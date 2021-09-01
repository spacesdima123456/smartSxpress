using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using DevExpress.Mvvm;
using Refit;
using Wms.API;
using Wms.API.Contract;
using Wms.API.Interface;
using Wms.API.Models;
using Wms.Localization;

namespace Wms.ViewModel
{
    public class LoginViewModel: BaseViewModel, INotifyDataErrorInfo
    {
        private readonly IRest _rest;
        private readonly Dictionary<string, List<string>> _errorsByPropertyName = new Dictionary<string, List<string>>();

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

        public static Dictionary<string, string> Languages =>
            new Dictionary<string, string>
            {
                {"en-US", "English"},
                {"ru-RU", "Русский"},
                {"zh-CN", "中文"},
                {"tr-TR", "Türk"},
            };

        public string Language
        {
            get =>  Properties.Settings.Default.DefaultLanguage;
            set
            {
                SetCulture(value);
                OnPropertyChanged(nameof(Language));
            }
        }

        private string _error;
        public string Error
        {
            get => _error;
            private set => Set(nameof(Error), ref _error, value);
        }

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        private ICommand _loginCommand;
        public ICommand LoginCommand => _loginCommand ??= new AsyncCommand(LoginAsync);

        private async Task LoginAsync()
        {
            try
            {
                var login = await _rest.ExecuteRequest<IAuth>().LogInAsync(new LoginReq(Email, Password, Language));
            }
            catch (ApiException ex)
            {
                var error = await ex.GetContentAsAsync<ValidationLogin>();

                ClearErrors(nameof(Email));
                ClearErrors(nameof(Password));

                if (error.Errors != null)
                {
                    if (error.Errors.Email != null)
                        AddError(nameof(Email), error.Errors.Email.Emails);

                    if (error.Errors.Password != null)
                        AddError(nameof(Password), error.Errors.Password.Min);
                }

                Error = error.Text ?? "";
            }
        }

        public bool HasErrors => _errorsByPropertyName.Any();

        public LoginViewModel()
        {
            _rest = new RestFactory().CreateRest();
        }

        public IEnumerable GetErrors(string propertyName)
        {
            return _errorsByPropertyName.ContainsKey(propertyName) ? _errorsByPropertyName[propertyName] : null;
        }

        //private void Validate(string value, string name)
        //{
        //    ClearErrors(name);
        //    if (string.IsNullOrWhiteSpace(value))
        //        AddError(name, $"{name} cannot be empty.");

        //    if (value == null || value?.Length <= 6)
        //        AddError(name, $"{name} must be at least 6 characters long.");
        //}

        //private void ValidateEmail(string value, string name)
        //{
        //    Validate(value, name);

        //    if (!value.ToLower().Contains("@"))
        //        AddError(name, $"{name} the email must contain a domain @.");
        //}

        private void AddError(string propertyName, string error)
        {
            if (!_errorsByPropertyName.ContainsKey(propertyName))
                _errorsByPropertyName[propertyName] = new List<string>();

            if (!_errorsByPropertyName[propertyName].Contains(error))
            {
                _errorsByPropertyName[propertyName].Add(error);
                OnErrorsChanged(propertyName);
            }
        }

        private void ClearErrors(string property)
        {
            if (_errorsByPropertyName.ContainsKey(property))
            {
                _errorsByPropertyName.Remove(property);
                OnErrorsChanged(property);
            }
        }

        private static void SetCulture(string culture)
        {
            Properties.Settings.Default.DefaultLanguage = culture;
            TranslationSource.Instance.CurrentCulture = new CultureInfo(culture);
            Properties.Settings.Default.Save();
        }

        private void OnErrorsChanged(string property)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(property));
        }
    }
}
