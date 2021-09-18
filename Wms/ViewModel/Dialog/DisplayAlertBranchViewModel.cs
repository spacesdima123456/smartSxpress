using System;
using System.Linq;
using Wms.API.Models;
using System.Windows;
using DevExpress.Mvvm;
using static Wms.Helpers.Translator;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Wms.ViewModel.Dialog
{
    public class DisplayAlertBranchViewModel: ValidateViewModel
    {
        private readonly Action<object> _action;

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

        private bool _isEnabled;
        public  bool IsEnabled
        {
            get => _isEnabled;
            set => Set(nameof(IsEnabled), ref _isEnabled, value);
        }


        private ICommand _doneCommand;
        public  ICommand DoneCommand => _doneCommand??= new DelegateCommand(() =>
        {
            ClearErrors();
            ValidProperty();
            if (Errors.Count==0)
            {
                _action.Invoke(this);
            }
        });

        private void ValidProperty()
        {
            if (string.IsNullOrEmpty(Company))
                AddError(nameof(Company), $"{nameof(Company)} property cannot be empty.");

            if (string.IsNullOrEmpty(Name))
                AddError(nameof(Name), $"{nameof(Name)} property cannot be empty.");

            if (string.IsNullOrEmpty(Address))
                AddError(nameof(Address), $"{nameof(Address)} property cannot be empty.");

            if (string.IsNullOrEmpty(City))
                AddError(nameof(City), $"{nameof(City)} property cannot be empty.");

            if (Zip == null)
                AddError(nameof(Zip), $"{nameof(Zip)} property cannot be null.");

            if (Country == null)
                AddError(nameof(Country), $"{nameof(Country)} property cannot be null.");

            if (string.IsNullOrEmpty(Email))
                AddError(nameof(Email), $"{nameof(Email)}  property cannot be empty.");

            if (Password == null)
                AddError(nameof(Password), $"{nameof(Password)}  property cannot be empty.");

            if (ConfirmPassword == null)
                AddError(nameof(ConfirmPassword), $"{nameof(ConfirmPassword)}  property cannot be empty.");

            if (ConfirmPassword != Password)
            {
                AddError(nameof(Password), $"Passwords don't match");
                AddError(nameof(ConfirmPassword), $"Passwords don't match");
            }

        }

        public DisplayAlertBranchViewModel(Action<object> action)
        {
            _action = action;
            Countries = new ObservableCollection<Countries>(App.Data.Data.Countries);
            
            Messenger.Default.Register<Customer>(this, (customer) =>
            {
                Height = 440;
                IsEnabled = true;
                Company = customer.Company;
                Visibility = Visibility.Visible;
                Content = Translate("Create");
            });

            Messenger.Default.Register<Branches>(this, (branches) =>
            {
                Height = 355;
                IsEnabled = false;
                Zip =  branches.Zip;
                Name = branches.Name;
                City = branches.City;
                Phone = branches.Phone;
                State = branches.State;
                Email = branches.Email;
                Company = branches.Company;
                Address = branches.Address;
                Visibility = Visibility.Collapsed;
                Content = Translate("Done");
                Country = Countries.FirstOrDefault(f => f.CountryCode == branches.Code);
            });
        }
    }
}
