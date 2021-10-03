using Refit;
using System;
using AutoMapper;
using Wms.API.Models;
using DevExpress.Mvvm;
using System.Windows.Forms;
using System.Windows.Input;
using Wms.ViewModel.Dialog;
using System.Threading.Tasks;
using Wms.UnitOfWorkAPI.Contract;
using Wms.Services.Window.Contract;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;

namespace Wms.ViewModel
{
    public class AdminViewModel : BaseViewModel
    {
        public EventHandler<KeyEventArgs> OnKeyDownEven;

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
                        var result = await _unitOfWork.AccountRepository.ChangeAccountAsync(_mapper.Map<Account>(c));
                        if (result.Code == 1)
                        {
                            UserName = account.Name;
                            Company = account.Company;
                            App.Data.Data.Customer = _mapper.Map(account, App.Data.Data.Customer);
                        }
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
                    var password = await _unitOfWork.AccountRepository.ChangePasswordAsync(new Password { UserPassword = p.Password });
                    if (password.Code == 1)
                    {
                        await _unitOfWork.AuthorizationRepository.LogOutAsync(Properties.Settings.Default.Token);
                        _windowSettings.HideProfileWindow();
                        _windowSettings.ShowLoginPage();
                    }

                }
                catch (ApiException ex)
                {
                    await HandleErrorsAsync(ex, p);
                }
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

        public AdminViewModel(IUnitOfWork unitOfWork, IWindowSettings windowSettings, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _windowSettings = windowSettings;
            OnKeyDownEven += OnKeyDown;

            Messenger.Default.Register<Response>(this, (data) =>
            {
                UserName = data.Data.Customer.Name;
                Company = data.Data.Customer.Company;
            });
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            Messenger.Default.Send(e);
        }
    }
}
