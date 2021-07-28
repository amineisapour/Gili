namespace AuthenticationMicroservice.Domain.ViewModels
{
    public class UserViewModel : object
    {
        public UserViewModel() : base()
        {
        }

        public System.Guid Id { get; set; }
        public string Username { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]
        public byte[] PasswordHash { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]
        public byte[] PasswordSalt { get; set; }
        public System.DateTime RegisterDateTime { get; set; }
        public bool IsActive { get; set; }
    }
}
