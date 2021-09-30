using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Wms.API.Models
{
    public class Data
    {
        [JsonPropertyName("customer")]
        public Customer Customer { get; set; }

        [JsonPropertyName("hts")]
        public Ht[] Hts { get; set; }

        [JsonPropertyName("countries")]
        public List<Countries> Countries { get; set; }

        [JsonPropertyName("docTypes")]
        public List<DocType> DocType { get; set; }
    }

}
