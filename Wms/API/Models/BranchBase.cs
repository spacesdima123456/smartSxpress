using System.Text.Json.Serialization;

namespace Wms.API.Models
{
    public class BranchBase: Account
    {
        [JsonPropertyName("countryCode")]
        public string Code { get; set; }
    }
}