using System.Text.Json.Serialization;

namespace Wms.API.Models
{
    public class LoginRes
    {
        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("apiKey")]
        public string ApiKey { get; set; }

        [JsonPropertyName("role")]
        public string Role { get; set; }
    }
}
