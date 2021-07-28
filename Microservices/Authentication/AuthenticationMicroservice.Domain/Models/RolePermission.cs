using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace AuthenticationMicroservice.Domain.Models
{
    public class RolePermission : Base.Entity
    {
        public RolePermission() : base()
        {
        }

        private readonly ILazyLoader _loader;
        public RolePermission(ILazyLoader loader)
        {
            _loader = loader;
        }

        //public Role Role { get; set; }
        private Role _role;
        [Required]
        public virtual Role Role
        {
            get => _loader.Load(this, ref _role);
            set => _role = value;
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
