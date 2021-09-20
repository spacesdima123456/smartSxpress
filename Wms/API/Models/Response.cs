using System.Text.Json.Serialization;

namespace Wms.API.Models
{
    public class Response
    {
        [JsonPropertyName("apiKey")]
        public string ApiKey { get; set; }

        [JsonPropertyName("data")]
        public Data Data { get; set; }
    }
}
