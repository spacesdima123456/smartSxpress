using System.Text.Json.Serialization;

namespace Wms.API.Models
{
    public class Customer: BranchBase
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("role")]
        public string Role { get; set; }

        [JsonPropertyName("logo")]
        public string Logo { get; set; }

        [JsonPropertyName("language")]
        public string Language { get; set; }

        [JsonPropertyName("countryName")]
        public string CountryName { get; set; }
    }
}
