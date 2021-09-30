using Wms.API.JsonConverter;
using System.Text.Json.Serialization;

namespace Wms.API.Models
{
    public class DocType
    {
        [JsonPropertyName("id")]
        [JsonConverter(typeof(IntToStringConvert))]
        public int Id { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }
    }
}
