using System;
using AuthenticationMicroservice.Domain.Models;

namespace AuthenticationMicroservice.Domain.ViewModels
{
    public class AuthenticateResponse
    {
        public AuthenticateResponse() { }

        public AuthenticateResponse(User user, UserInformation userInfo, string jwtToken, string refreshToken)
        {
            Id = user.Id;
            Username = user.Username;
            Gender = userInfo.Gender.ToString();
            FullName = userInfo.FirstName + " " + userInfo.LastName;
            Token = jwtToken;
            RefreshToken = refreshToken;
        }

        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Gender { get; set; }
        public string FullName { get; set; }
        public string Token { get; set; }
        //[System.Text.Json.Serialization.JsonIgnore] // refresh token is returned in http only cookie
        public string RefreshToken { get; set; }
    }
}
