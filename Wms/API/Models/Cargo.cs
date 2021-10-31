using System.Text.Json.Serialization;

namespace Wms.API.Models
{
   public class Cargo
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public  string Name { get; set; }
    }
}
