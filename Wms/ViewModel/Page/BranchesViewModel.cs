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
using Wms.API.Models.Wms.API.Models;
using Wms.Services.Window.WindowDialogs;

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
            _windowBranch.Create(b =>
            {
                // servic api
                Branches = new ObservableCollection<Branches>(LoadBranches());
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
                        var branchBase = new BranchBase
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
                        };
                        await _unitOfWork.BranchRepository.EditBranchAsync(branch.Id, branchBase);
                        Branches = new ObservableCollection<Branches>(LoadBranches());
                        _windowBranch.Close();
                    }
                    catch (ApiException e)
                    {
                        var content = await e.GetContentAsAsync<Error>();
                        b.HandleErrors(content);
                    }
                });
            Messenger.Default.Send(branch);
        });

        public BranchesViewModel()
        {
            _unitOfWork = new UnitOfWork();
            _windowBranch = new WindowBranch();
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
