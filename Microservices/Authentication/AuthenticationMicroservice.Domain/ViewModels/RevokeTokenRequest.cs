namespace AuthenticationMicroservice.Domain.ViewModels
{
    public class RevokeTokenRequest
    {
        public RevokeTokenRequest()
        {
        }

        public string Token { get; set; }
    }
}
