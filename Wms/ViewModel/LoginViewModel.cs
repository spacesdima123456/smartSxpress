using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Nito.AsyncEx;
using Refit;
using Wms.API.Models;
using Wms.Localization;
using Wms.Services.Authorization;
using Wms.Services.Authorization.Contract;
using AsyncCommand = DevExpress.Mvvm.AsyncCommand;

namespace Wms.ViewModel
{
    public class LoginViewModel: BaseViewModel, INotifyDataErrorInfo
    {
        private readonly Login _login;
        private readonly IAuthorization _authorization;
        private readonly Dictionary<string, List<string>> _errorsByPropertyName = new Dictionary<string, List<string>>();

        private string _email;
        public string Email
        {
            get => _email;
            set
            {
                ClearErrors(nameof(Email));
                Set(nameof(Email), ref _email, value);
            }
        }
            
        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                ClearErrors(nameof(Password));
                Set(nameof(Password), ref _password, value);
            }
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
        public ICommand LoginCommand => _loginCommand ??= new AsyncCommand(async () =>
        {
            try
            {
                await _authorization.LogInAsync(new LoginReq(Email, Password, Language));
                VerifyApiKey();
            }
            catch (ApiException ex)
            {
                await HandleErrorsAsync(ex);
            }
        });

        public bool HasErrors => _errorsByPropertyName.Any();

        public LoginViewModel(Login login)
        {
            _authorization = new AuthorizationFactory().Make();
            _login = login;
            VerifyApiKey();
        }

        public IEnumerable GetErrors(string propertyName)
        {
            return _errorsByPropertyName.ContainsKey(propertyName) ? _errorsByPropertyName[propertyName] : null;
        }

        private async Task HandleErrorsAsync(ApiException ex)
        {
            var error = await ex.GetContentAsAsync<ValidationLogin>();

            if (error.Errors != null)
            {
                if (error.Errors.Email != null)
                    AddError(nameof(Email), error.Errors.Email.Emails);

                if (error.Errors.Password != null)
                    AddError(nameof(Password), error.Errors.Password.Min);
            }

            Error = error.Text ?? "";
        }

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

        private void VerifyApiKey()
        {
            if (_authorization.IsAuth)
            {
                try
                {
                    var data = AsyncContext.Run(async () => await _authorization.ValidKeyAsync());
                    if (data != null)
                    {
                        OpenAdmin();
                    }
                }
                catch
                {
                    // ignored
                }
            }
        }

        private void OpenAdmin()
        {
            var admin = new Admin { Owner = _login.Owner };
            admin.Show();
            _login.Close();
        }
    }
}
