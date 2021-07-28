using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace AuthenticationMicroservice.Domain.Models
{
    public class UserPermission : Base.Entity
    {
        public UserPermission(): base()
        {
        }

        private readonly ILazyLoader _loader;
        public UserPermission(ILazyLoader loader)
        {
            _loader = loader;
        }

        //public User User { get; set; }
        private User _user;
        [Required]
        public virtual User User
        {
            get => _loader.Load(this, ref _user);
            set => _user = value;
        }

        //public Permission Permission { get; set; }
        private Permission _permission;
        [Required]
        public virtual Permission Permission
        {
            get => _loader.Load(this, ref _permission);
            set => _permission = value;
        }
    }
}
