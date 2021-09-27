using System;
using System.Linq;
using Wms.API.Models;
using System.Windows;
using DevExpress.Mvvm;
using System.Windows.Input;
using static Wms.Helpers.Translator;
using System.Collections.ObjectModel;

namespace Wms.ViewModel.Dialog
{
    public class DisplayAlertBranchBaseViewModel : ValidateViewModel
    {
        protected Action<DisplayAlertBranchBaseViewModel> Action;

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

        private double _height;

        public double Height
        {
            get => _height;
            set => Set(nameof(Height), ref _height, value);
        }

        private string _content;

        public string Content
        {
            get => _content;
            protected set => Set(nameof(Content), ref _content, value);
        }

        private string _tittle;

        public string Title
        {
            get => _tittle;
            protected set => Set(nameof(Title), ref _tittle, value);
        }

        private Visibility _visibility;

        public Visibility Visibility
        {
            get => _visibility;
            protected set => Set(nameof(Visibility), ref _visibility, value);
        }

        private bool _isEnabled;
        public bool IsEnabled
        {
            get => _isEnabled;
            set => Set(nameof(IsEnabled), ref _isEnabled, value);
        }

        private ICommand _doneCommand;

        public virtual ICommand DoneCommand => _doneCommand ??= new DelegateCommand(() =>
        {
            ClearErrors();
            Action.Invoke(this);
        });

        public ObservableCollection<Countries> Countries { get; }

        private Countries _country;

        public Countries Country
        {
            get => _country;
            set
            {
                Set(nameof(Country), ref _country, value);
                if (_country != null && _country.CountryCode != "US")
                    State = string.Empty;
            }
        }

        public DisplayAlertBranchBaseViewModel(Action<DisplayAlertBranchBaseViewModel> action)
        {
            Action = action;
            Countries = new ObservableCollection<Countries>(App.Data.Data.Countries);
        }

        public  virtual  void Validate()
        {
            ValidateForm();
            ValidatePassword();
        }

        public virtual void ValidateForm()
        {
            var error = "Property cannot be empty.";

            if (string.IsNullOrEmpty(Company))
                AddError(nameof(Company), error);

            if (string.IsNullOrEmpty(Name))
                AddError(nameof(Name), error);

            if (string.IsNullOrEmpty(Address))
                AddError(nameof(Address), error);

            if (string.IsNullOrEmpty(City))
                AddError(nameof(City), error);

            if (Zip == null)
                AddError(nameof(Zip), error);

            if (Country == null)
                AddError(nameof(Country), error);

            if (string.IsNullOrEmpty(Email))
                AddError(nameof(Email), error);

            if (Country != null && Country.CountryCode == "US" && string.IsNullOrEmpty(State))
                AddError(nameof(State), error);
        }

        public virtual void ValidatePassword()
        {
            var error = "Property cannot be empty.";

            if (string.IsNullOrEmpty(Password))
                AddError(nameof(Password), error);

            if (string.IsNullOrEmpty(ConfirmPassword))
                AddError(nameof(ConfirmPassword), error);

            if (ConfirmPassword != Password)
            {
                AddError(nameof(Password), $"Passwords don't match");
                AddError(nameof(ConfirmPassword), $"Passwords don't match");
            }

        }

        public new virtual void HandleErrors(Error error)
        {
            base.HandleErrors(error);
        }

        protected void InitProperty(
            int height, string title, bool isEnabled,
            int? zip, string name, string city, string phone,
            string state, string email, string company, string address, Visibility visibility, string content,
            string country)
        {
            Zip = zip;
            Name = name;
            City = city;
            Phone = phone;
            Title = title;
            State = state;
            Email = email;
            Height = height;
            Company = company;
            Address = address;
            IsEnabled = isEnabled;
            Visibility = visibility;
            Content = Translate(content);
            Country = Countries.FirstOrDefault(f => f.Name == country);
        }

    }
}
