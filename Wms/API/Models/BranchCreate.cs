using System.Text.Json.Serialization;

namespace Wms.API.Models
{
    public class BranchCreate : BranchBase
    {
        [JsonPropertyName("password")]
        public string Password { get; set; }
    }
}
