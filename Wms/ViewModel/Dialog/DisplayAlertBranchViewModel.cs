using System.Collections.ObjectModel;
using DevExpress.Mvvm;
using Wms.API.Models;

namespace Wms.ViewModel.Dialog
{
    public class DisplayAlertBranchViewModel: BaseViewModel
    {
        private string _company;
        public string Company
        {
            get => _company;
            set => Set(nameof(Company), ref _company, value);
        }

        private string _name;
        public string Name
        {
            get => _name;
            set => Set(nameof(Name), ref _name, value);
        }

        private string _address;
        public string Address
        {
            get => _address;
            set => Set(nameof(Address), ref _address, value);
        }

        private string _city;
        public string City
        {
            get => _city;
            set => Set(nameof(City), ref _city, value);
        }

        private string _zip;
        public string Zip
        {
            get => _zip;
            set => Set(nameof(Zip), ref _zip, value);
        }

        private string _state;
        public string State
        {
            get => _state;
            set => Set(nameof(State), ref _state, value);
        }

        private string _phone;
        public string Phone
        {
            get => _phone;
            set => Set(nameof(Phone), ref _phone, value);
        }

        private string _email;
        public string Email
        {
            get => _email;
            set => Set(nameof(Email), ref _email, value);
        }

        private string _password;
        public string Password
        {
            get => _password;
            set => Set(nameof(Password), ref _password, value);
        }

        private string _confirmPassword;
        public string ConfirmPassword
        {
            get => _confirmPassword;
            set => Set(nameof(ConfirmPassword), ref _confirmPassword, value);
        }

        private ObservableCollection<Countries> _countries;

        public ObservableCollection<Countries> Countries
        {
            get => _countries;
            set => Set(nameof(Countries), ref _countries, value);
        }

        private Countries _country;
        public Countries Country
        {
            get => _country;
            set
            {
                Set(nameof(Country), ref _country, value);
                TelMask = $"({value.PhoneCode}) 000-00-00-00";
            }
        }

        private string _telMask;
        public  string TelMask
        {
            get => _telMask;
            private set => Set(nameof(TelMask), ref _telMask, value);
        }

        public DisplayAlertBranchViewModel()
        {
            Messenger.Default.Register<Customer>(this, (data) => Company = data.Company);
            Countries = new ObservableCollection<Countries>(App.Data.Data.Countries);
        }
    }
}
