using System.Text.Json.Serialization;

namespace Wms.API.Models
{
    public class CustomerDoc<T> : Error
    {
        [JsonPropertyName("data")]
        public T Data { get; set; }
    }
}
