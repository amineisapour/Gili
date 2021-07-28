using System.ComponentModel.DataAnnotations;

namespace AuthenticationMicroservice.Domain.ViewModels
{
    public class AuthenticateRequest
    {
        public AuthenticateRequest()
        {
        }

        [Required]
        [EmailAddress]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
