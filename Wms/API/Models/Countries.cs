using Wms.API.JsonConverter;
using System.Text.Json.Serialization;

namespace Wms.API.Models
{
    public class Countries
    {
        [JsonPropertyName("countryCode")]
        public  string CountryCode { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("phoneCode")]
        [JsonConverter(typeof(IntToStringConvert))]
        public int PhoneCode { get; set; }

        [JsonPropertyName("phoneMask")]
        public string PhoneMask { get; set; }
    }

}
