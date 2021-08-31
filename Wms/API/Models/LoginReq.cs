using System;
using System.Text.Json.Serialization;

namespace Wms.API.Models
{
    public class LoginReq
    {
        [JsonPropertyName("email")]
        public string Email { get; }

        [JsonPropertyName("password")]
        public string Password { get; }

        public LoginReq(string email, string password)
        {
            if (string.IsNullOrEmpty(email))
                throw new Exception($"{nameof(email)} is null or empty");

            if (string.IsNullOrEmpty(password))
                throw new Exception($"{nameof(password)} is null or empty");

            Email = email;
            Password = password;
        }
    }
}
