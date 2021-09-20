using System.Text.Json.Serialization;
using Wms.API.Models.Wms.API.Models;

namespace Wms.API.Models
{
  public  class Branches: BranchBase
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("role")]
        public string Role { get; set; }

        [JsonPropertyName("logo")]
        public string Logo { get; set; }

        [JsonPropertyName("language")]
        public string Language { get; set; }
    }
}
