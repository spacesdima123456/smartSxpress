
using Refit;
using System.Linq;
using Nito.AsyncEx;
using Wms.API.Models;
using DevExpress.Mvvm;
using Wms.UnitOfWorkAPI;
using System.Windows.Input;
using Wms.UnitOfWorkAPI.Contract;
using System.Collections.Generic;
using Wms.Services.Window.Contract;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Wms.Services.Window.WindowDialogs;
using Wms.ViewModel.Dialog;

namespace Wms.ViewModel.Page
{
    public class BranchesViewModel : BaseViewModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWindowBranch _windowBranch;

        private ObservableCollection<Branches> _branches;
        public ObservableCollection<Branches> Branches
        {
            get => _branches??= new ObservableCollection<Branches>(LoadBranches());
            private set => Set(nameof(Branches), ref _branches, value);
        }

        private ICommand _openWindowDeleteBranchCommand;
        public ICommand OpenWindowDeleteBranchCommand => _openWindowDeleteBranchCommand ??= new DelegateCommand<Branches>((branches) => _windowBranch.Delete(
            async o =>
            {
                await _unitOfWork.BranchRepository.DeleteBranchAsync(branches.Id);
                Branches.Remove(branches);
            }));

        private ICommand _addBranchCommand;
        public ICommand AddCommand => _addBranchCommand??=new DelegateCommand(() =>
        {
            _windowBranch.Create(async b =>
            {
                try
                {
                    await _unitOfWork.BranchRepository.CreateBranchAsync(new BranchCreate
                    {
                        Zip = b.Zip,
                        Name = b.Name,
                        City = b.City,
                        Email = b.Email,
                        Phone = b.Phone,
                        State = b.State,
                        Address = b.Address,
                        Company = b.Company,
                        Code = b.Country.CountryCode,
                        Password = b.Password
                    });
                    Branches = new ObservableCollection<Branches>(LoadBranches());
                    _windowBranch.Close();
                }
                catch (ApiException e)
                {
                    await HandleErrorsAsync(e, b);
                }
            });
            Messenger.Default.Send(App.Data.Data.Customer);
        });

        private ICommand _editCommand;
        public ICommand EditCommand => _editCommand ??= new DelegateCommand<Branches>( (branch) =>
        {
            _windowBranch.Edit(async b =>
                {
                    try
                    {
                        await _unitOfWork.BranchRepository.EditBranchAsync(branch.Id, MakeBranch(b.Zip, b.Name, b.City, b.Email, b.Phone, b.State, b.Address, b.Company, b.Country.CountryCode));
                        Branches = new ObservableCollection<Branches>(LoadBranches());
                        _windowBranch.Close();
                    }
                    catch (ApiException e)
                    {
                       await HandleErrorsAsync(e, b);
                    }
                });
            Messenger.Default.Send(branch);
        });

        public BranchesViewModel()
        {
            _unitOfWork = new UnitOfWork();
            _windowBranch = new WindowBranch();
        }

        private BranchBase MakeBranch(int? zip, string name, string city, string email, string phone, string state,
            string address, string company, string code)
        {
            return new BranchBase
            {
                Zip = zip,
                Name = name,
                City = city,
                Email = email,
                Phone = phone,
                State = state,
                Address = address,
                Company = company,
                Code = code,
            };
        }

        private async Task HandleErrorsAsync(ApiException e, DisplayAlertBranchViewModel vm)
        {
            var content = await e.GetContentAsAsync<Error>();
            vm.HandleErrors(content);
        }

        private IEnumerable<Branches> LoadBranches()
        {
            return AsyncContext.Run(async () =>
            {
                var branches = await _unitOfWork.BranchRepository.GetAllBranchesAsync();
                return branches ?? Enumerable.Empty<Branches>();
            });
        }
    }
}
