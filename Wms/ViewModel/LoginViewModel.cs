using System.Collections.Generic;
using System.Windows.Forms;
using System.Windows.Input;
using DevExpress.Mvvm;
using Wms.API;
using Wms.API.Contract;
using Wms.API.Interface;
using Wms.API.Models;

namespace Wms.ViewModel
{
    public class LoginViewModel: BaseViewModel
    {
        private readonly IRest _rest;

        private string _email;
        public string Email
        {
            get=> _email;
            set=> Set(nameof(Email), ref _email, value);
        }

        private string _password;
        public string Password
        {
            get => _password;
            set => Set(nameof(Password), ref _password, value);
        }

        public Dictionary<string, string> Languages =>
            new Dictionary<string, string>
            {
                {"en-US", "English"},
                {"ru-RU", "Русский"},
                {"zh-CN", "中文"},
                {"tr-TR", "Türk"},
            };

        private string _language;
        public string Language
        {
            get => _language;
            set => Set(nameof(Language), ref _language, value);
        }

        private ICommand _loginCommand;
        public ICommand LoginCommand
        {
            get
            {
                return _loginCommand??= new AsyncCommand(async () =>
                {
                    if (Email.Length>6 && Password.Length>6 && !string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password))
                    {
                        var login = await _rest.ExecuteRequest<IAuth>().LogInAsync(new LoginReq(Email, Password, Language));
                    }
                    else
                    {
                        MessageBox.Show("Error","Invalid username or password!");
                    }
                });
            }
        }

        public LoginViewModel()
        {
            _rest = new RestFactory().CreateRest();
        }
    }
}
