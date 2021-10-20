using System.Text.Json.Serialization;

namespace Wms.API.Models
{
    public class ScaleType
    {
        [JsonPropertyName("type")]
        public  string Type { get; set; }

        [JsonPropertyName("regexp")]
        public string Regexp { get; set; }
    }
}
