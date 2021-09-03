using Refit;
using Nito.AsyncEx;
using Wms.API.Models;
using Wms.Localization;
using System.Globalization;
using System.Windows.Input;
using System.Threading.Tasks;
using System.Collections.Generic;
using DevExpress.Mvvm;
using Wms.Services.Authorization;
using Wms.Services.Authorization.Contract;
using AsyncCommand = DevExpress.Mvvm.AsyncCommand;

namespace Wms.ViewModel
{
    public class LoginViewModel: ValidateViewModel 
    {
        private readonly Login _login;
        private readonly IAuthorization _authorization;

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

        private ICommand _closeCommand;
        public ICommand CloseCommand => _closeCommand ??=new DelegateCommand(() =>
        {
            System.Windows.Application.Current.Shutdown();
        });

        public LoginViewModel(Login login)
        {
            _authorization = new AuthorizationFactory().Make();
            _login = login;
            VerifyApiKey();
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

        private static void SetCulture(string culture)
        {
            Properties.Settings.Default.DefaultLanguage = culture;
            TranslationSource.Instance.CurrentCulture = new CultureInfo(culture);
            Properties.Settings.Default.Save();
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
