using System.Text.Json.Serialization;

namespace Wms.API.Models
{
    public class Email
    {
        [JsonPropertyName("email")]
        public string Emails { get; set; }
    }
}
