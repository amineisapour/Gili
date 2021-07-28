using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace AuthenticationMicroservice.Domain.Models
{
    public class UserRole : Base.Entity
    {
        public UserRole() : base()
        {
        }

        private readonly ILazyLoader _loader;
        public UserRole(ILazyLoader loader)
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

        //public Role Role { get; set; }
        private Role _role;
        [Required]
        public virtual Role Role
        {
            get => _loader.Load(this, ref _role);
            set => _role = value;
        }
    }
}
