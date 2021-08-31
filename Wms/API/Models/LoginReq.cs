using System;
using System.Text.Json.Serialization;

namespace Wms.API.Models
{
    public class LoginReq
    {
        [JsonPropertyName("email")]
        public string Email { get; private set; }

        [JsonPropertyName("password")]
        public string Password { get; private set; }

        [JsonPropertyName("lng")]
        public string Language { get; private set; }

        public LoginReq(string email, string password, string language)
        {
            if (string.IsNullOrEmpty(email))
                throw new Exception($"{nameof(email)} is null or empty");

            if (string.IsNullOrEmpty(password))
                throw new Exception($"{nameof(password)} is null or empty");

            if (string.IsNullOrEmpty(language))
                throw new Exception($"{nameof(language)} is null or empty");

            Email = email;
            Password = password;
            Language = language;
        }
    }
}
