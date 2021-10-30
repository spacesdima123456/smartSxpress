using Wms.ViewModel;
using System.Text.Json.Serialization;

namespace Wms.API.Models
{
    public class Sender: ValidateViewModel
    {
        private int _docTypeId;
        [JsonPropertyName("docTypeId")]
        public int DocTypeId
        {
            get => _docTypeId;
            set => Set(nameof(DocTypeId), ref _docTypeId, value);
        }

        private string _docId;
        [JsonPropertyName("docId")]
        public string DocId
        {
            get => _docId;
            set => Set(nameof(DocId), ref _docId, value);
        }

        private string _name;
        [JsonPropertyName("name")]
        public string Name
        {
            get => _name;
            set => Set(nameof(Name), ref _name, value);
        }

        private string _address;
        [JsonPropertyName("address")]
        public string Address
        {
            get => _address;
            set => Set(nameof(Address), ref _address, value);
        }

        private string _city;
        [JsonPropertyName("city")]
        public string City
        {
            get => _city;
            set => Set(nameof(City), ref _city, value);
        }

        private string _state;
        [JsonPropertyName("state")]
        public string State
        {
            get => _state;
            set => Set(nameof(State), ref _state, value);
        }

        private int? _zip;
        [JsonPropertyName("zip")]
        public int? Zip
        {
            get => _zip;
            set => Set(nameof(Zip), ref _zip, value);
        }

        private string _phone;
        [JsonPropertyName("phone")]
        public string Phone
        {
            get => _phone;
            set => Set(nameof(Phone), ref _phone, value);
        }

        private string _countryCode;
        [JsonPropertyName("countryCode")]
        public string CountryCode
        {
            get => _countryCode;
            set => Set(nameof(CountryCode), ref _countryCode, value);
        }

        public void Validate()
        {
            ClearErrors();

            if (string.IsNullOrWhiteSpace(Address))
                AddError(nameof(Address), "Address is null or white space ");
        }


        public  void Clear()
        {
            Zip = null;
            Name = Address = City = State = Phone = "";
        }
    }
}
