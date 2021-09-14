using System.Collections.ObjectModel;
using System.Windows.Input;
using DevExpress.Mvvm;
using Nito.AsyncEx;
using Wms.API.Models;
using Wms.Services.Window.Contract;
using Wms.Services.Window.WindowDialogs;
using Wms.UnitOfWorkAPI;
using Wms.UnitOfWorkAPI.Contract;

namespace Wms.ViewModel.Page
{
    public class BranchesViewModel : BaseViewModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWindowBranch _windowBranch;
        private ObservableCollection<Branches> _branches;


        public ObservableCollection<Branches> Branches
        {
            get => _branches;
            private set => Set(nameof(Branches), ref _branches, value);
        }

        private ICommand _openWindowDeleteBranchCommand;
        public ICommand OpenWindowDeleteBranchCommand => _openWindowDeleteBranchCommand ??= new DelegateCommand<Branches>((branches) => _windowBranch.Delete(
            async o =>
            {
                await _unitOfWork.BranchRepository.DeleteBranchAsync(branches.Id);
                Branches.Remove(branches);
            }));

        public BranchesViewModel()
        {
            _unitOfWork = new UnitOfWork();
            _windowBranch = new WindowBranch();
            Branches = new ObservableCollection<Branches>(AsyncContext.Run(async () => await _unitOfWork.BranchRepository.GetAllBranchesAsync()));
        }
    }
}
