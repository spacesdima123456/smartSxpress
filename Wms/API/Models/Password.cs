using System.Text.Json.Serialization;

namespace Wms.API.Models
{
    public class Password
    {
        [JsonPropertyName("password")]
        public string UserPassword { get; set; }
    }
}
