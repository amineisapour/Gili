using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AuthenticationMicroservice.Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace AuthenticationMicroservice.Core.Services
{
    public class AccountService : Interfaces.IAccountService
    {
        #region Fields

        private readonly SymmetricSecurityKey _key;
        private readonly byte[] _secretKey;
        private readonly int _refreshTokenInDaysTTL;
        private readonly int _refreshTokenInDaysForDbTTL;
        private readonly int _TokenInMinutesTTL;

        #endregion

        #region Constructor

        public AccountService(IConfiguration config)
        {
            //_key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetSection("AppSettings")["SecretKey"]));
            _secretKey = Encoding.UTF8.GetBytes(config["MySecretKey"]);
            _key = new SymmetricSecurityKey(_secretKey);

            int.TryParse(config.GetSection("AppSettings")["RefreshTokenInDaysTTL"], out _refreshTokenInDaysTTL);
            if (_refreshTokenInDaysTTL <= 0)
                _refreshTokenInDaysTTL = 7;

            int.TryParse(config.GetSection("AppSettings")["RefreshTokenInDaysForDbTTL"], out _refreshTokenInDaysForDbTTL);
            if (_refreshTokenInDaysForDbTTL <= 0)
                _refreshTokenInDaysForDbTTL = 2; 

            int.TryParse(config.GetSection("AppSettings")["TokenInMinutesTTL"], out _TokenInMinutesTTL);
            if (_TokenInMinutesTTL <= 0)
                _TokenInMinutesTTL = 15;

        }

        #endregion

        #region Token

        public Guid? ValidateJwtToken(string token)
        {
            if (token == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(_secretKey),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = Guid.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);

                // return user id from JWT token if validation successful
                return userId;
            }
            catch
            {
                // return null if validation fails
                return null;
            }
        }

        public string GenerateJwtToken(Domain.Models.User user)
        {
            if (user == null)
            {
                throw new ApplicationException("user is null!");
            }

            var claims = new ClaimsIdentity();

            claims.AddClaims(new[]
            {
                new Claim("id", user.Id.ToString())
            });

            if (user.UserPermissions != null)
            {
                foreach (var permission in user.UserPermissions)
                {
                    claims.AddClaims(new[]
                    {
                        new Claim(Core.Config.PermissionsConfig.Permission, permission.Permission.Name)
                    });
                }
            }

            if (user.UserRoles != null)
            {
                foreach (var userRole in user.UserRoles)
                {
                    foreach (var rolePermission in userRole.Role.RolePermissions)
                    {
                        var pName = rolePermission.Permission.Name;
                        if (user.UserPermissions == null || user.UserPermissions.All(x => x.Permission.Name != pName))
                        {
                            claims.AddClaims(new[]
                            {
                                new Claim(Core.Config.PermissionsConfig.Permission, pName)
                            });
                        }
                    }
                }
            }

            var cerds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.Now.AddMinutes(_TokenInMinutesTTL),
                SigningCredentials = cerds,
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        #endregion

        #region Password

        public Domain.ViewModels.PasswordViewModel CreatePassword(string password)
        {
            using var hmac = new HMACSHA512();
            var result = new Domain.ViewModels.PasswordViewModel
            {
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password)),
                PasswordSalt = hmac.Key,
                Password = password
            };
            return result;
        }

        public bool IsPasswordValid(Domain.ViewModels.PasswordViewModel passwordData)
        {
            var hmac = new HMACSHA512(passwordData.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(passwordData.Password));
            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != passwordData.PasswordHash[i])
                {
                    return false;
                }
            }
            return true;
        }

        #endregion

        #region RefreshToken

        public RefreshToken GenerateRefreshToken(string ipAddress)
        {
            // generate refresh token that is valid for 7 days
            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            var randomBytes = new byte[64];
            rngCryptoServiceProvider.GetBytes(randomBytes);
            var refreshToken = new RefreshToken
            {
                Token = Convert.ToBase64String(randomBytes),
                Expires = DateTime.Now.AddDays(_refreshTokenInDaysTTL),
                Created = DateTime.Now,
                CreatedByIp = ipAddress
            };

            return refreshToken;
        }

        public RefreshToken RotateRefreshToken(RefreshToken refreshToken, string ipAddress)
        {
            var newRefreshToken = GenerateRefreshToken(ipAddress);
            RevokeRefreshToken(refreshToken, ipAddress, "Replaced by new token", newRefreshToken.Token);
            return newRefreshToken;
        }

        public void RevokeDescendantRefreshTokens(RefreshToken refreshToken, User user, string ipAddress, string reason)
        {
            // recursively traverse the refresh token chain and ensure all descendants are revoked
            if (!string.IsNullOrEmpty(refreshToken.ReplacedByToken))
            {
                var childToken = user.RefreshTokens.SingleOrDefault(x => x.Token == refreshToken.ReplacedByToken);
                if (childToken is { IsActive: true })
                    RevokeRefreshToken(childToken, ipAddress, reason);
                else
                    RevokeDescendantRefreshTokens(childToken, user, ipAddress, reason);
            }
        }

        public void RevokeRefreshToken(RefreshToken token, string ipAddress, string reason = null, string replacedByToken = null)
        {
            token.Revoked = DateTime.Now;
            token.RevokedByIp = ipAddress;
            token.ReasonRevoked = reason;
            token.ReplacedByToken = replacedByToken;
        }

        public List<RefreshToken> RemoveOldRefreshTokens(User user)
        {
            // remove old inactive refresh tokens from user based on TTL in app settings
            //user.RefreshTokens.RemoveAll(x =>
            //    !x.IsActive &&
            //    x.Created.AddDays(_refreshTokenInDaysForDbTTL) <= DateTime.Now);
            return user.RefreshTokens.Where(x =>
                  !x.IsActive &&
                  x.Created.AddDays(_refreshTokenInDaysForDbTTL) <= DateTime.Now).ToList();
        }

        #endregion
    }
}
