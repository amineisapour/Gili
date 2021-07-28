using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace AuthenticationMicroservice.Domain.ViewModels
{
    public class RegisterUserViewModel
    {
        [Required]
        [EmailAddress]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

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
