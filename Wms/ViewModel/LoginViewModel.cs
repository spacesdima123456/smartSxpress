using System.Windows.Input;
using DevExpress.Mvvm;
using Wms.API;
using Wms.API.Interface;
using Wms.API.Models;

namespace Wms.ViewModel
{
    public class LoginViewModel: BaseViewModel
    {
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

        private ICommand _loginCommand;
        public ICommand LoginCommand
        {
            get
            {
                return _loginCommand??= new DelegateCommand(  () =>
                {
                    var rest = new RestFactory().CreateRest();
                    var login =  rest.ExecuteRequest<IAuth>().LogInAsync(new LoginReq(Email, Password)).GetAwaiter().GetResult();
                    if (login.Status == "Ok")
                    {
                        
                    }
                });
            }
        }
    }
}
