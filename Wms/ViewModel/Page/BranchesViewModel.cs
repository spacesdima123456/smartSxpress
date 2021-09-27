using Refit;
using AutoMapper;
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
        private readonly IMapper _mapper;
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
                    b.Validate();
                    if (!b.HasErrors)
                    {
                        await _unitOfWork.BranchRepository.CreateBranchAsync(_mapper.Map<BranchCreate>(b));
                        RefreshBranchAndCloseWindow();
                    }
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
            _windowBranch.Edit(b,async e =>
                {
                    try
                    {
                        e.ValidateForm();
                        if (!e.HasErrors)
                        {
                            await _unitOfWork.BranchRepository.EditBranchAsync(b.Id, _mapper.Map<BranchBase>(e));
                            RefreshBranchAndCloseWindow();
                        }
                    }
                    catch (ApiException ex)
                    {
                       await HandleErrorsAsync(ex, e);
                    }
                });
            Messenger.Default.Send(b);
        });

        public BranchesViewModel(IUnitOfWork unitOfWork, IWindowBranch windowBranch, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _windowBranch = windowBranch;
        }

        private void RefreshBranchAndCloseWindow()
        {
            Branches = new ObservableCollection<Branches>(LoadBranches());
            _windowBranch.Close();
        }

        private static async Task HandleErrorsAsync(ApiException e, DisplayAlertBranchBaseViewModel vm)
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
