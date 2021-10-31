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

        [JsonPropertyName("scaleTypes")]
        public List<ScaleType> ScaleTypes { get; set; }

        [JsonPropertyName("forwarders")]
        public  List<Cargo> Forwarders { get; set; }

        [JsonPropertyName("consignees")]
        public List<Cargo> Consignees { get; set; }
    }
}
