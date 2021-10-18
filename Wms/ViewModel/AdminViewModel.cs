using Refit;
using AutoMapper;
using Wms.API.Models;
using DevExpress.Mvvm;
using System.Windows.Input;
using System.Threading.Tasks;
using Wms.UnitOfWorkAPI.Contract;
using Wms.Services.Window.Contract;
using static Wms.Helpers.ErrorValidation;

namespace Wms.ViewModel
{
    public class AdminViewModel : BaseViewModel
    {
        private readonly IMapper _mapper;
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
                        var account = _mapper.Map<Account>(c);
                        var success = await _unitOfWork.AccountRepository.ChangeAccountAsync(_mapper.Map<Account>(c));
                        if (success.Code == 1)
                        {
                            UserName = account.Name;
                            Company = account.Company;
                            App.Data.Data.Customer = _mapper.Map(account, App.Data.Data.Customer);
                        }
                        else
                            HandleGeneralErrors(success);
                    }
                    catch (ApiException ex)
                    {
                        await HandleErrorsAsync(ex, c);
                    }
                }

            }, async p=>
            {
                try
                {
                    var success = await _unitOfWork.AccountRepository.ChangePasswordAsync(new Password { UserPassword = p.Password });
                    if (success.Code == 1)
                    {
                        await _unitOfWork.AuthorizationRepository.LogOutAsync(Properties.Settings.Default.Token);
                        _windowSettings.HideProfileWindow();
                        _windowSettings.ShowLoginPage();
                    }
                    else
                        HandleGeneralErrors(success);
                }
                catch (ApiException ex)
                {
                    await HandleErrorsAsync(ex, p);
                }
            });
        });

        public AdminViewModel(IUnitOfWork unitOfWork, IWindowSettings windowSettings, IMapper mapper)
        {
            _mapper = mapper;
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
