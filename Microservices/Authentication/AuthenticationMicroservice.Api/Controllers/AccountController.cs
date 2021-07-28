using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AuthenticationMicroservice.Core.Attributes;
using AuthenticationMicroservice.Core.Exceptions;
using AuthenticationMicroservice.Domain.Enums;
using AuthenticationMicroservice.Domain.Models;
using AuthenticationMicroservice.Domain.ViewModels;
using Castle.Components.DictionaryAdapter;
using GiliX.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace AuthenticationMicroservice.Api.Controllers
{
    public class AccountController : Infrastructure.ApiControllerBase
    {
        #region Fields

        private readonly Core.Interfaces.IAccountService _accountService;

        #endregion

        #region Constructor

        public AccountController(Core.Interfaces.IAccountService accountService,
            Persistence.IUnitOfWork unitOfWork,
            Persistence.IQueryUnitOfWork queryUnitOfWork) : base(unitOfWork, queryUnitOfWork)
        {
            _accountService = accountService;
        }

        #endregion

        #region Methods

        [AllowAnonymous]
        [HttpPost("login")]
        [ProducesResponseType(type: typeof(Result<AuthenticateResponse>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(type: typeof(Result), statusCode: StatusCodes.Status400BadRequest)]
        public async Task<Result<AuthenticateResponse>> Login([FromBody] LoginUserViewModel loginUser)
        {
            var result = new FluentResults.Result<AuthenticateResponse>();
            try
            {
                var user = await GetUserAsync(loginUser.Username);
                if (user == null)
                {
                    result.WithError(errorMessage: Resources.Messages.Errors.LoginFailed);
                    return result.ConvertToDtxResult();
                }

                var isPasswordValid = _accountService.IsPasswordValid(new PasswordViewModel
                {
                    Password = loginUser.Password,
                    PasswordSalt = user.PasswordSalt,
                    PasswordHash = user.PasswordHash

                });
                if (!isPasswordValid)
                {
                    result.WithError(errorMessage: Resources.Messages.Errors.LoginFailed);
                    return result.ConvertToDtxResult();
                }

                var jwtToken = _accountService.GenerateJwtToken(user);
                var refreshToken = _accountService.GenerateRefreshToken(IpAddress());

                await UnitOfWork.RefreshTokens.InsertAsync(refreshToken);
                user.RefreshTokens.Add(refreshToken);

                // remove old refresh tokens from user
                //_accountService.RemoveOldRefreshTokens(user);
                RemoveOldRefreshTokens(user);

                // save changes to db
                await UnitOfWork.Users.UpdateAsync(user);

                await UnitOfWork.SaveAsync();

                AuthenticateResponse authenticateResponse = new Domain.ViewModels.AuthenticateResponse
                {
                    Id = user.Id,
                    Username = user.Username,
                    Token = jwtToken,
                    RefreshToken = refreshToken.Token
                };

                result.WithSuccess(successMessage: Resources.Messages.Successes.SuccessLogin);
                result.WithValue(value: authenticateResponse);

                //SetTokenCookie(refreshToken.Token);
            }
            catch (Exception ex)
            {
                result.WithError(errorMessage: ex.Message);
                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    result.WithError(ex.InnerException.Message);
                }
            }
            return result.ConvertToDtxResult();
        }

        [AllowAnonymous]
        [HttpPost("refresh-token")]
        [ProducesResponseType(type: typeof(Result<AuthenticateResponse>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(type: typeof(Result), statusCode: StatusCodes.Status400BadRequest)]
        public async Task<Result<AuthenticateResponse>> RefreshToken([FromBody] AuthenticateResponse tokenElemnt)
        {
            var result = new FluentResults.Result<AuthenticateResponse>();
            try
            {
                //Request.Headers["dd"].ToString();
                //var token = Request.Cookies["refreshToken"];
                var token = tokenElemnt.RefreshToken ?? Request.Cookies["refreshToken"];
                if (string.IsNullOrEmpty(token))
                {
                    result.WithError("Token is required");
                    return result.ConvertToDtxResult();
                }

                //var response = _userService.RefreshToken(token, IpAddress());

                var user = await QueryUnitOfWork.Users.GetUserByRefreshTokenAsync(token);
                if (user == null)
                {
                    result.WithError("Invalid token");
                    //throw new AppException("Invalid token");
                    return result.ConvertToDtxResult();
                }
                var refreshToken = user.RefreshTokens.Single(x => x.Token == token);
                if (refreshToken.IsRevoked)
                {
                    // revoke all descendant tokens in case this token has been compromised
                    _accountService.RevokeDescendantRefreshTokens(refreshToken, user, IpAddress(), $"Attempted reuse of revoked ancestor token: {token}");

                    await UnitOfWork.Users.UpdateAsync(user);
                    await UnitOfWork.SaveAsync();
                }

                if (!refreshToken.IsActive)
                {
                    result.WithError("Invalid token");
                    //throw new AppException("Invalid token");
                    return result.ConvertToDtxResult();
                }

                // replace old refresh token with a new one (rotate token)
                var newRefreshToken = _accountService.RotateRefreshToken(refreshToken, IpAddress());
                await UnitOfWork.RefreshTokens.UpdateAsync(refreshToken);
                await UnitOfWork.RefreshTokens.InsertAsync(newRefreshToken);

                user.RefreshTokens.Add(newRefreshToken);

                // remove old refresh tokens from user
                //_accountService.RemoveOldRefreshTokens(user);
                RemoveOldRefreshTokens(user);

                // save changes to db
                await UnitOfWork.Users.UpdateAsync(user);
                await UnitOfWork.SaveAsync();

                var jwtToken = _accountService.GenerateJwtToken(user);

                AuthenticateResponse authenticateResponse = new Domain.ViewModels.AuthenticateResponse
                {
                    Id = user.Id,
                    Username = user.Username,
                    Token = jwtToken,
                    RefreshToken = newRefreshToken.Token
                };

                result.WithSuccess("Ok");
                result.WithValue(value: authenticateResponse);

                //SetTokenCookie(newRefreshToken.Token);
            }
            catch (Exception ex)
            {
                result.WithError(errorMessage: ex.Message);
                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    result.WithError(ex.InnerException.Message);
                }
            }
            return result.ConvertToDtxResult();
        }

        [AllowAnonymous]
        [HttpPost("revoke-token")]
        [ProducesResponseType(type: typeof(Result), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(type: typeof(Result), statusCode: StatusCodes.Status400BadRequest)]
        public async Task<Result> RevokeToken(RevokeTokenRequest model)
        {
            var result = new FluentResults.Result();
            try
            {
                // accept refresh token in request body or cookie
                var token = model.Token ?? Request.Cookies["refreshToken"];
                if (string.IsNullOrEmpty(token))
                {
                    result.WithError("Token is required");
                    return result.ConvertToDtxResult();
                }

                var user = await QueryUnitOfWork.Users.GetUserByRefreshTokenAsync(token);
                if (user == null)
                {
                    result.WithError("Invalid token");
                    //throw new AppException("Invalid token");
                    return result.ConvertToDtxResult();
                }
                var refreshToken = user.RefreshTokens.Single(x => x.Token == token);

                if (!refreshToken.IsActive)
                {
                    result.WithError("Invalid token");
                    //throw new AppException("Invalid token");
                    return result.ConvertToDtxResult();
                }

                // revoke token and save
                _accountService.RevokeRefreshToken(refreshToken, IpAddress(), "Revoked without replacement");

                await UnitOfWork.Users.UpdateAsync(user);
                await UnitOfWork.SaveAsync();

                result.WithSuccess("Token revoked");
            }
            catch (Exception ex)
            {
                result.WithError(errorMessage: ex.Message);
                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    result.WithError(ex.InnerException.Message);
                }
            }
            return result.ConvertToDtxResult();
        }

        [AllowAnonymous]
        [HttpPost("register")]
        [ProducesResponseType(type: typeof(Result<AuthenticateResponse>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(type: typeof(Result), statusCode: StatusCodes.Status400BadRequest)]
        public async Task<Result<AuthenticateResponse>> Register([FromBody] RegisterUserViewModel registerUser)
        {
            var result = new FluentResults.Result<AuthenticateResponse>();
            try
            {
                var getUser = await GetUserAsync(registerUser.Username);
                if (getUser != null)
                {
                    result.WithError(string.Format(Resources.Messages.Errors.ExistEntity, "User"));
                    return result.ConvertToDtxResult();
                }

                var passwordDate = _accountService.CreatePassword(registerUser.Password);

                var newUser = new User
                {
                    Username = registerUser.Username.ToLower(),
                    PasswordHash = passwordDate.PasswordHash,
                    PasswordSalt = passwordDate.PasswordSalt
                };
                await UnitOfWork.Users.InsertAsync(newUser);

                var newUserInfo = new UserInformation
                {
                    Birthdate = registerUser.Birthdate,
                    FirstName = registerUser.FirstName,
                    LastName = registerUser.LastName,
                    Gender = registerUser.Gender,
                    NationalId = registerUser.NationalId,
                    User = newUser
                };
                await UnitOfWork.UserInformations.InsertAsync(newUserInfo);
                await UnitOfWork.SaveAsync();

                var jwtToken = _accountService.GenerateJwtToken(newUser);
                var refreshToken = _accountService.GenerateRefreshToken(IpAddress());

                await UnitOfWork.RefreshTokens.InsertAsync(refreshToken);
                newUser.RefreshTokens.Add(refreshToken);

                // remove old refresh tokens from user
                //RemoveOldRefreshTokens(newUser);

                await UnitOfWork.Users.UpdateAsync(newUser);
                await UnitOfWork.SaveAsync();

                AuthenticateResponse authenticateResponse = new Domain.ViewModels.AuthenticateResponse
                {
                    Id = newUser.Id,
                    Username = newUser.Username,
                    Token = jwtToken,
                    RefreshToken = refreshToken.Token
                };

                result.WithSuccess(successMessage: string.Format(Resources.Messages.Successes.SuccessInsert, "User"));
                result.WithValue(value: authenticateResponse);

                //SetTokenCookie(refreshToken.Token);
            }
            catch (Exception ex)
            {
                result.WithError(errorMessage: ex.Message);
                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    result.WithError(ex.InnerException.Message);
                }
            }
            return result.ConvertToDtxResult();
        }

        [PermissionAuthorize(Core.Config.PermissionsConfig.Account.CanRead)]
        [HttpGet("get-all-user")]
        [ProducesResponseType(type: typeof(Result<IList<Domain.ViewModels.UserListViewModel>>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(type: typeof(Result), statusCode: StatusCodes.Status400BadRequest)]
        public async Task<Result<IList<Domain.ViewModels.UserListViewModel>>> GetAllUser()
        {
            var result = new FluentResults.Result<IList<Domain.ViewModels.UserListViewModel>>();
            try
            {
                var userList = await QueryUnitOfWork.Users.GetAllAsync();
                List<Domain.ViewModels.UserListViewModel> returnList =
                    new List<Domain.ViewModels.UserListViewModel>();
                foreach (var user in userList)
                {
                    var userInfo =
                        await QueryUnitOfWork.UserInformations.GetUserInfoByUserIdAsync(user.Id);
                    returnList.Add(new Domain.ViewModels.UserListViewModel()
                    {
                        RegisterDateTime = user.RegisterDateTime,
                        Username = user.Username,
                        IsActive = user.IsActive,
                        Id = user.Id,
                        Birthdate = userInfo?.Birthdate,
                        Gender = userInfo?.Gender,
                        FirstName = userInfo != null ? userInfo.FirstName : "",
                        LastName = userInfo != null ? userInfo.LastName : "",
                        NationalId = userInfo != null ? userInfo.NationalId : ""
                    });
                }

                result.WithValue(value: returnList);
            }
            catch (Exception ex)
            {
                result.WithError(errorMessage: ex.Message);
            }
            return result.ConvertToDtxResult();
        }

        [PermissionAuthorize(Core.Config.PermissionsConfig.Account.CanRead)]
        [HttpGet("{id}/refresh-token")]
        [ProducesResponseType(type: typeof(Result<IList<Domain.ViewModels.UserListViewModel>>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(type: typeof(Result), statusCode: StatusCodes.Status400BadRequest)]
        public async Task<Result<IList<Domain.Models.RefreshToken>>> GetRefreshTokens(Guid id)
        {
            var result = new FluentResults.Result<IList<Domain.Models.RefreshToken>>();
            try
            {
                var user =
                    await QueryUnitOfWork.Users.GetUserByUserIdAsync(id);

                result.WithSuccess("Ok");
                result.WithValue(value: user.RefreshTokens);
            }
            catch (Exception ex)
            {
                result.WithError(errorMessage: ex.Message);
            }
            return result.ConvertToDtxResult();
        }

        [PermissionAuthorize(Core.Config.PermissionsConfig.Account.CanRead)]
        [HttpGet("get-user/{id}")]
        [ProducesResponseType(type: typeof(Result<Domain.ViewModels.UserListViewModel>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(type: typeof(Result), statusCode: StatusCodes.Status400BadRequest)]
        public async Task<Result<Domain.ViewModels.UserListViewModel>> GetById(Guid id)
        {
            var result = new FluentResults.Result<Domain.ViewModels.UserListViewModel>();
            try
            {
                var user =
                    await QueryUnitOfWork.Users.GetUserByUserIdAsync(id);
                var userInfo =
                    await QueryUnitOfWork.UserInformations.GetUserInfoByUserIdAsync(user.Id);

                var userListViewModel = new Domain.ViewModels.UserListViewModel()
                {
                    RegisterDateTime = user.RegisterDateTime,
                    Username = user.Username,
                    IsActive = user.IsActive,
                    Id = user.Id,
                    Birthdate = userInfo?.Birthdate,
                    Gender = userInfo?.Gender,
                    FirstName = userInfo != null ? userInfo.FirstName : "",
                    LastName = userInfo != null ? userInfo.LastName : "",
                    NationalId = userInfo != null ? userInfo.NationalId : ""
                };

                result.WithSuccess("Ok");
                result.WithValue(value: userListViewModel);
            }
            catch (Exception ex)
            {
                result.WithError(errorMessage: ex.Message);
            }
            return result.ConvertToDtxResult();
        }

        [PermissionAuthorize]
        [HttpGet("test")]
        public string Test()
        {
            return "Hello You have the right access to the Test method.";
        }

        #endregion

        #region Helper Methods

        private async Task<User> GetUserAsync(string username)
        {
            return await UnitOfWork.Users.GetByUsernameAsync(username.ToLower());
        }

        private void SetTokenCookie(string token)
        {
            // append cookie with refresh token to the http response
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.Now.AddDays(7)
            };
            Response.Cookies.Append("refreshToken", token, cookieOptions);
        }

        private string IpAddress()
        {
            // get source ip address for the current request

            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];

            return HttpContext.Connection.RemoteIpAddress != null ?
                HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString() :
                "localhost";
        }

        private void RemoveOldRefreshTokens(User user)
        {
            var listOldRefreshToken = _accountService.RemoveOldRefreshTokens(user);
            foreach (var oldRefreshToken in listOldRefreshToken)
            {
                UnitOfWork.RefreshTokens.DeleteAsync(oldRefreshToken);
                user.RefreshTokens.Remove(oldRefreshToken);
            }
        }

        #endregion

        #region FakeData

        [AllowAnonymous]
        [HttpGet("generate-fake-data")]
        [ProducesResponseType(type: typeof(Result), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(type: typeof(Result), statusCode: StatusCodes.Status400BadRequest)]
        public async Task<Result> GenerateFakeData()
        {
            var result = new FluentResults.Result();
            try
            {
                //*************************  Permission

                Permission p1 = new Permission { Name = "Permission.Account.CanEdit" };
                await UnitOfWork.Permissions.InsertAsync(p1);

                Permission p2 = new Permission { Name = "Permission.Account.CanDelete" };
                await UnitOfWork.Permissions.InsertAsync(p2);

                Permission p3 = new Permission { Name = "Permission.Action.FullAccess" };
                await UnitOfWork.Permissions.InsertAsync(p3);

                Permission p4 = new Permission { Name = "Permission.Account.CanRead" };
                await UnitOfWork.Permissions.InsertAsync(p4);

                Permission p5 = new Permission { Name = "Permission.Account.CanAdd" };
                await UnitOfWork.Permissions.InsertAsync(p5);

                //*************************  Role

                Role r1 = new Role { Name = "Admin", };
                await UnitOfWork.Roles.InsertAsync(r1);

                Role r2 = new Role { Name = "User", };
                await UnitOfWork.Roles.InsertAsync(r2);

                //*************************  RolePermission

                RolePermission rp1 = new RolePermission { Role = r2, Permission = p4 };
                await UnitOfWork.RolePermissions.InsertAsync(rp1);

                RolePermission rp2 = new RolePermission { Role = r1, Permission = p3 };
                await UnitOfWork.RolePermissions.InsertAsync(rp2);

                //*************************  User & UserInformation

                var pd1 = _accountService.CreatePassword("1234");
                var u1 = new User
                {
                    Username = "admin@gili.com",
                    PasswordHash = pd1.PasswordHash,
                    PasswordSalt = pd1.PasswordSalt
                };
                await UnitOfWork.Users.InsertAsync(u1);

                var uif1 = new UserInformation
                {
                    Birthdate = new DateTime(year: 1980, month: 01, day: 01),
                    FirstName = "Admin",
                    LastName = "Admin",
                    Gender = Gender.Man,
                    NationalId = "111111111",
                    User = u1
                };
                await UnitOfWork.UserInformations.InsertAsync(uif1);

                var pd2 = _accountService.CreatePassword("1234");
                var u2 = new User
                {
                    Username = "amin.eisapour@gmail.com",
                    PasswordHash = pd2.PasswordHash,
                    PasswordSalt = pd2.PasswordSalt
                };
                await UnitOfWork.Users.InsertAsync(u2);

                var uif2 = new UserInformation
                {
                    Birthdate = new DateTime(year: 1982, month: 07, day: 21),
                    FirstName = "Amin",
                    LastName = "Eisapour",
                    Gender = Gender.Man,
                    NationalId = "0068272618",
                    User = u2
                };
                await UnitOfWork.UserInformations.InsertAsync(uif2);

                //*************************  UserRole

                var ur1 = new UserRole { User = u1, Role = r1 };
                await UnitOfWork.UserRoles.InsertAsync(ur1);

                var ur2 = new UserRole { User = u2, Role = r2 };
                await UnitOfWork.UserRoles.InsertAsync(ur2);


                //*************************  Save Data
                await UnitOfWork.SaveAsync();

                result.WithSuccess("All data has been saved successfully.");
            }
            catch (Exception ex)
            {
                result.WithError(errorMessage: ex.Message);
            }
            return result.ConvertToDtxResult();
        }

        #endregion
    }
}
