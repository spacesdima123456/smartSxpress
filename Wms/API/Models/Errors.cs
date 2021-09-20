using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Wms.API.Models
{
    public class Error
    {
        [JsonPropertyName("text")]
        public  string Text { get; set; }

        [JsonPropertyName("code")]
        public int Code { get; set; }

        [JsonPropertyName("Errors")]
        public  Dictionary<string, object> Errors { get; set; }
    }
}
