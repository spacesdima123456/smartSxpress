using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Wms.API.Models
{
    public class Branch
    {
        [JsonPropertyName("code")]
        public int Code { get; set; }
        [JsonPropertyName("text")]
        public string Text { get; set; }

        [JsonPropertyName("data")]
        public List<Branches> Branches { get; set; }
    }
}
