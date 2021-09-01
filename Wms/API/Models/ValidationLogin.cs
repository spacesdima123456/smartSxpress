using System.Text.Json.Serialization;

namespace Wms.API.Models
{
    public class ValidationLogin
    {
        private string _text;
        [JsonPropertyName("text")]
        public string Text
        {
            get => _text == "validationError" ? "" : _text;
            set => _text = value;
        }

        [JsonPropertyName("errors")]
        public Errors Errors { get; set; }
    }
}
