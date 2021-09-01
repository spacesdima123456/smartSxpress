using System.Text.Json.Serialization;

namespace Wms.API.Models
{
    public class Password
    {
        [JsonPropertyName("min")]
        public string Min { get; set; }
    }
}
