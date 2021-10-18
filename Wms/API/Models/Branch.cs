using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Wms.API.Models
{
    public class Branch: Error
    {
        [JsonPropertyName("data")]
        public List<Branches> Branches { get; set; }
    }
}
