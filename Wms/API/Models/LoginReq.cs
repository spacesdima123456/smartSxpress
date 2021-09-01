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


    public class ValidationLogin
    {
        private string _text;
        [JsonPropertyName("text")]
        public string Text
        {
            get => _text == "validationError" ? "" : _text;
            set => _text = value;
        }

        [JsonPropertyName("errors")]
        public Errors Errors { get; set; }
    }

    public class Errors
    {
        [JsonPropertyName("email")]
        public Email Email { get; set; }
        [JsonPropertyName("password")]
        public Password Password { get; set; }
    }

    public class Email
    {
        [JsonPropertyName("email")]
        public string Emails { get; set; }
    }

    public class Password
    {
        [JsonPropertyName("min")]
        public string Min { get; set; }
    }
}
