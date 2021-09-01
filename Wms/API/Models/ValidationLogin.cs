using System.Text.Json.Serialization;

namespace Wms.API.Models
{
    public class ValidationLogin
    {
        private string _text;
        [JsonPropertyName("text")]
        public string Text
        {
            get => Code != 2 ? _text : "";
            set => _text = value;
        }

        [JsonPropertyName("code")]
        public  int Code { get; set; }

        [JsonPropertyName("errors")]
        public Errors Errors { get; set; }
    }
}
