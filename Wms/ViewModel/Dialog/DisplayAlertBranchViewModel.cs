using System.Linq;
using Wms.API.Models;
using System.Windows;
using DevExpress.Mvvm;
using static Wms.Helpers.Translator;
using System.Collections.ObjectModel;

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

        private int? _zip;
        public int? Zip
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

        public ObservableCollection<Countries> Countries { get; }

        private Countries _country;
        public Countries Country
        {
            get => _country;
            set => Set(nameof(Country), ref _country, value);
        }

        private Visibility _visibility;
        public Visibility Visibility
        {
            get => _visibility;
            private set => Set(nameof(Visibility), ref _visibility, value);
        }

        private double _height;
        public double Height
        {
            get => _height;
            set => Set(nameof(Height), ref _height, value);
        }

        private string _content;
        public  string Content
        {
            get => _content;
            private set => Set(nameof(Content), ref _content, value);
        }

        public DisplayAlertBranchViewModel()
        {
            Countries = new ObservableCollection<Countries>(App.Data.Data.Countries);
            
            Messenger.Default.Register<Customer>(this, (customer) =>
            {
                Height = 440;
                Company = customer.Company;
                Visibility = Visibility.Visible;
                Content = Translate("Create");
            });

            Messenger.Default.Register<Branches>(this, (branches) =>
            {
                Height = 355;
                Name = branches.Name;
                City = branches.City;
                Phone = branches.Phone;
                State = branches.State;
                Email = branches.Email;
                Company = branches.Company;
                Address = branches.Address;
                Zip =  branches.Zip;
                Visibility = Visibility.Collapsed;
                Content = Translate("Done");
                Country = Countries.FirstOrDefault(f => f.CountryCode == branches.Code);
            });

        }
    }
}
