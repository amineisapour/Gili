using System;
using AuthenticationMicroservice.Domain.Models;

namespace AuthenticationMicroservice.Domain.ViewModels
{
    public class AuthenticateResponse
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }
        //[System.Text.Json.Serialization.JsonIgnore] // refresh token is returned in http only cookie
        public string RefreshToken { get; set; }

        public AuthenticateResponse()
        {

        }

        public AuthenticateResponse(User user, string jwtToken, string refreshToken)
        {
            Id = user.Id;
            Username = user.Username;
            Token = jwtToken;
            RefreshToken = refreshToken;
        }
    }
}
