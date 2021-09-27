using Refit;
using Wms.API.Models;
using DevExpress.Mvvm;
using System.Windows.Forms;
using System.Windows.Input;
using Wms.ViewModel.Dialog;
using System.Threading.Tasks;
using Wms.UnitOfWorkAPI.Contract;
using Wms.Services.Window.Contract;

namespace Wms.ViewModel
{
    public class AdminViewModel : BaseViewModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWindowSettings _windowSettings;

        private string _source;
        public string Source
        {
            get => _source;
            private set => Set(nameof(Source), ref _source, value);
        }

        private string _userName;
        public  string UserName
        {
            get => _userName;
            private set => Set(nameof(UserName), ref _userName, value);
        }

        private string _company;
        public string Company
        {
            get => _company;
            private set => Set(nameof(Company), ref _company, value);
        }

        private ICommand _pageCommand;
        public ICommand PageCommand => _pageCommand ??= new DelegateCommand<string>(source => Source = source);

        public async  Task LogOutAsync()
        {
            await _unitOfWork.AuthorizationRepository.LogOutAsync(Properties.Settings.Default.Token);
        }

        private ICommand _profileCommand;
        public ICommand ProfileCommand => _profileCommand ??= new DelegateCommand(() =>
        {
            _windowSettings.ShowProfile(async c=>
            {
                c.ValidateForm();
                if (!c.HasErrors)
                {
                    try
                    {
                        var account = new Account { Company = c.Company, Name = c.Name, Address = c.Address, City = c.City, State = c.State, Zip = c.Zip, Phone = c.Phone, Email = c.Email };
                        var result = await _unitOfWork.AccountRepository.ChangeAccountAsync(account);
                        if (result.Code == 1)
                        {
                            UserName = account.Name;
                            Company = account.Company;

                            App.Data.Data.Customer.Zip = account.Zip;
                            App.Data.Data.Customer.Name = account.Name;
                            App.Data.Data.Customer.City = account.City;
                            App.Data.Data.Customer.Phone = account.Phone;
                            App.Data.Data.Customer.State = account.State;
                            App.Data.Data.Customer.Email = account.Email;
                            App.Data.Data.Customer.Company = account.Company;
                            App.Data.Data.Customer.Address = account.Address;
                        }
                    }
                    catch (ApiException ex)
                    {
                        await HandleErrorsAsync(ex, c);
                    }
                }

            }, p =>
            {
                _windowSettings.HideProfileWindow();
            });
        });


        private static async Task HandleErrorsAsync(ApiException e, DisplayAlertBranchBaseViewModel vm)
        {
            var content = await e.GetContentAsAsync<Error>();
            if (content.Errors != null)
                vm.HandleErrors(content);
            else
                MessageBox.Show(content.Text);
        }

        public AdminViewModel(IUnitOfWork unitOfWork, IWindowSettings windowSettings)
        {
            _unitOfWork = unitOfWork;
            _windowSettings = windowSettings;

            Messenger.Default.Register<Response>(this, (data) =>
            {
                UserName = data.Data.Customer.Name;
                Company = data.Data.Customer.Company;
            });
        }
    }
}
