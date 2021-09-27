using Wms.API.Models;
using DevExpress.Mvvm;
using System.Windows.Input;
using System.Threading.Tasks;
using Wms.Services.Window.Contract;
using Wms.UnitOfWorkAPI.Contract;

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
            _windowSettings.ShowProfile(c =>
            {

                _windowSettings.HideProfileWindow();
            }, p =>
            {

                _windowSettings.HideProfileWindow();
            });
        });

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
