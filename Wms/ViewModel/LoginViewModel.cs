using Refit;
using Wms.API.Models;
using DevExpress.Mvvm;
using Wms.Localization;
using System.Windows.Input;
using System.Globalization;
using Wms.Services.TokenVerify;
using Wms.Services.Authorization;
using Wms.Services.Window.Contract;
using Wms.Services.TokenVerify.Contract;
using Wms.Services.Authorization.Contract;
using AsyncCommand = DevExpress.Mvvm.AsyncCommand;

namespace Wms.ViewModel
{
    public class LoginViewModel : ValidateViewModel
    {
        private readonly ITokenVerify _tokenVerify;
        private readonly IWindowFactory _windowFactory;
        private readonly IAuthorization _authorization;

        private string _email;

        public string Email
        {
            get => _email;
            set
            {
                ClearError(nameof(Email));
                Set(nameof(Email), ref _email, value);
            }
        }

        private string _password;

        public string Password
        {
            get => _password;
            set
            {
                ClearError(nameof(Password));
                Set(nameof(Password), ref _password, value);
            }
        }

        private string _text;

        public string Text
        {
            get => _text;
            private set => Set(nameof(Text), ref _text, value);
        }

        private ICommand _languageCommand;
        public ICommand LanguageCommand => _languageCommand ??= new DelegateCommand<string>(SetCulture);

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
                await _authorization.LogInAsync(new Login(Email, Password,
                    Properties.Settings.Default.DefaultLanguage));
                _tokenVerify.VerifyApiToken();
            }
            catch (ApiException ex)
            {
                var content = await ex.GetContentAsAsync<Error>();
                HandleErrors(content);
            }
        });

        private ICommand _closeCommand;

        public ICommand CloseCommand =>
            _closeCommand ??= new DelegateCommand(() => System.Windows.Application.Current.Shutdown());

        public LoginViewModel(IWindowFactory windowFactory)
        {
            _windowFactory = windowFactory;
            _authorization = new AuthorizationFactory().Make();
            _tokenVerify = new TokenVerifyFactory(_authorization).Make();
            _tokenVerify.VerifySuccess += CompletedVerify;
            _tokenVerify.VerifyApiToken();
        }

        private void CompletedVerify(object sender, Response e)
        {
            _windowFactory.CreateWindow();
            Messenger.Default.Send(e);
            App.SetDataKeyCheck(e);
            SetToken(e.ApiKey);
        }

        private static void SetCulture(string culture)
        {
            Properties.Settings.Default.DefaultLanguage = culture;
            TranslationSource.Instance.CurrentCulture = new CultureInfo(culture);
            Properties.Settings.Default.Save();
        }

        private static void SetToken(string token)
        {
            Properties.Settings.Default.Token = token;
            Properties.Settings.Default.Save();
        }

        protected override void HandleErrors(Error error)
        {
            base.HandleErrors(error);
            if (error.Code!=2)
                Text = error.Text ?? "";
        }
    }
}
