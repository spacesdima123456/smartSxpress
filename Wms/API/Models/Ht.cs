using System.Text.Json.Serialization;

namespace Wms.API.Models
{
    public class Ht
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("hts")]
        public string Hts { get; set; }

        [JsonPropertyName("desc")]
        public string Desc { get; set; }
    }
}
