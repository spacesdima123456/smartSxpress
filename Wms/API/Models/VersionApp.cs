using System.Text.Json.Serialization;

namespace Wms.API.Models
{
    public class VersionApp
    {
        [JsonPropertyName("version")]
        public string Version { get; set; }
        [JsonPropertyName("url")]
        public string Url { get; set; }
    }
}
