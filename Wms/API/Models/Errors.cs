using System.Text.Json.Serialization;

namespace Wms.API.Models
{
    public class Errors
    {
        [JsonPropertyName("email")]
        public Email Email { get; set; }
        [JsonPropertyName("password")]
        public Password Password { get; set; }
    }
}
