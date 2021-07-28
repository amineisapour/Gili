using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace AuthenticationMicroservice.Domain.Models
{
    public class User : Base.Entity
    {
        public User() : base()
        {
            //UserRoles = new List<UserRole>();
            //UserPermissions = new List<UserPermission>();
            //RefreshTokens = new List<RefreshToken>();
        }

        private readonly ILazyLoader _loader;
        public User(ILazyLoader loader)
        {
            _loader = loader;
        }

        [Required]
        [EmailAddress]
        [MaxLength(254)]
        public string Username { get; set; }

        [Required]
        [System.Text.Json.Serialization.JsonIgnore]
        public byte[] PasswordHash { get; set; }

        [Required]
        [System.Text.Json.Serialization.JsonIgnore]
        public byte[] PasswordSalt { get; set; }

        private List<RefreshToken> _refreshTokens;
        [System.Text.Json.Serialization.JsonIgnore]
        public List<RefreshToken> RefreshTokens
        {
            get => _loader.Load(this, ref _refreshTokens);
            set => _refreshTokens = value;
        }

        private List<UserPermission> _userPermissions;
        public virtual List<UserPermission> UserPermissions
        {
            get => _loader.Load(this, ref _userPermissions);
            set => _userPermissions = value;
        }

        private List<UserRole> _userRoles;
        public virtual List<UserRole> UserRoles
        {
            get => _loader.Load(this, ref _userRoles);
            set => _userRoles = value;
        }

    }
}
