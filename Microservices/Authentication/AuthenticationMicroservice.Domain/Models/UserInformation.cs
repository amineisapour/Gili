using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace AuthenticationMicroservice.Domain.Models
{
    public class UserInformation : Base.Entity
    {
        public UserInformation(): base()
        {
        }

        private readonly ILazyLoader _loader;
        public UserInformation(ILazyLoader loader)
        {
            _loader = loader;
        }

        private User _user;
        [Required]
        public virtual User User
        {
            get => _loader.Load(this, ref _user);
            set => _user = value;
        }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [AllowNull]
        public string NationalId { get; set; }

        [Required]
        public Enums.Gender Gender { get; set; }

        [Required]
        public System.DateTime Birthdate { get; set; }
    }
}
