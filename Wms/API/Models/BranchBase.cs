namespace Wms.API.Models
{
    using System.Text.Json.Serialization;

    namespace Wms.API.Models
    {
        public class BranchBase
        {
            [JsonPropertyName("countryCode")]
            public string Code { get; set; }

            [JsonPropertyName("company")]
            public string Company { get; set; }

            [JsonPropertyName("name")]
            public string Name { get; set; }
            [JsonPropertyName("email")]
            public string Email { get; set; }
            [JsonPropertyName("address")]
            public string Address { get; set; }

            [JsonPropertyName("city")]
            public string City { get; set; }

            [JsonPropertyName("state")]
            public string State { get; set; }
            [JsonPropertyName("zip")]
            public int? Zip { get; set; }

            [JsonPropertyName("phone")]
            public string Phone { get; set; }
        }
    }

}
