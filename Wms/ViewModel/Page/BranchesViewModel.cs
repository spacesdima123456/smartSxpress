using Refit;
using System.Linq;
using Nito.AsyncEx;
using System.Windows;
using Wms.API.Models;
using DevExpress.Mvvm;
using System.Windows.Input;
using Wms.ViewModel.Dialog;
using System.Threading.Tasks;
using Wms.UnitOfWorkAPI.Contract;
using System.Collections.Generic;
using Wms.Services.Window.Contract;
using System.Collections.ObjectModel;

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
                    var branch = CreateBranch(b.Zip, b.Name, b.City, b.Email, b.Phone, b.State, b.Address, b.Company, b.Country.CountryCode, b.Password);
                    await _unitOfWork.BranchRepository.CreateBranchAsync(branch);
                    RefreshBranchAndCloseWindow();
                }
                catch (ApiException e)
                {
                    await HandleErrorsAsync(e, b);
                }
            });
            Messenger.Default.Send(App.Data.Data.Customer.Company);
        });

        private ICommand _editCommand;
        public ICommand EditCommand => _editCommand ??= new DelegateCommand<Branches>( (b) =>
        {
            _windowBranch.Edit(async e =>
                {
                    try
                    {
                        var branch = MakeBranch(e.Zip, e.Name, e.City, e.Email, e.Phone, e.State, e.Address, e.Company, e.Country.CountryCode);
                        await _unitOfWork.BranchRepository.EditBranchAsync(b.Id, branch);
                        RefreshBranchAndCloseWindow();
                    }
                    catch (ApiException ex)
                    {
                       await HandleErrorsAsync(ex, e);
                    }
                });
            Messenger.Default.Send(b);
        });

        public BranchesViewModel(IUnitOfWork unitOfWork, IWindowBranch windowBranch)
        {
            _unitOfWork = unitOfWork;
            _windowBranch = windowBranch;
        }

        private void RefreshBranchAndCloseWindow()
        {
            Branches = new ObservableCollection<Branches>(LoadBranches());
            _windowBranch.Close();
        }

        private static BranchBase MakeBranch(int? zip, string name, string city, string email, string phone, string state,
            string address, string company, string code)
        {
            return new BranchBase { Zip = zip, Name = name, City = city, Email = email, Phone = phone, State = state, Address = address, Company = company, Code = code };
        }

        private static BranchCreate CreateBranch(int? zip, string name, string city, string email, string phone, string state,
            string address, string company, string code, string password)
        {
            return new BranchCreate { Zip = zip, Name = name, City = city, Email = email, Phone = phone, State = state, Address = address, Company = company, Code = code, Password = password };
        }

        private static async Task HandleErrorsAsync(ApiException e, DisplayAlertBranchViewModel vm)
        {
            var content = await e.GetContentAsAsync<Error>();
            if (content.Errors != null)
                vm.HandleErrors(content);
            else
                MessageBox.Show(content.Text);
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
