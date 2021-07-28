using System.ComponentModel.DataAnnotations;

namespace AuthenticationMicroservice.Domain.ViewModels
{
    public class LoginUserViewModel
    {
        [Required]
        [EmailAddress]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
