using Nito.AsyncEx;
using System.Windows;
using Wms.API.Models;
using DevExpress.Mvvm;
using Wms.UnitOfWorkAPI;
using System.Windows.Input;
using Wms.UnitOfWorkAPI.Contract;
using Wms.Services.Window.Contract;
using System.Collections.ObjectModel;
using Wms.Services.Window.WindowDialogs;

namespace Wms.ViewModel
{
    public class BranchesViewModel : BaseViewModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWindowBranch _windowBranch;
        private ObservableCollection<Branches> _branches;

        private VerticalAlignment _verticalAlignmentAdd;
        public VerticalAlignment VerticalAlignmentAdd
        {
            get => _verticalAlignmentAdd;
            private set => Set(nameof(VerticalAlignmentAdd), ref _verticalAlignmentAdd, value);
        }

        private HorizontalAlignment _horizontalAlignmentAdd;
        public HorizontalAlignment HorizontalAlignmentAdd
        {
            get => _horizontalAlignmentAdd;
            private set => Set(nameof(HorizontalAlignmentAdd), ref _horizontalAlignmentAdd, value);
        }


        public ObservableCollection<Branches> Branches
        {
            get => _branches;
            private set => Set(nameof(Branches), ref _branches, value);
        }

        private ICommand _openWindowDeleteBranchCommand;
        public ICommand OpenWindowDeleteBranchCommand => _openWindowDeleteBranchCommand ??= new DelegateCommand<Branches>((branches) => _windowBranch.Delete(
            o =>
            {
                Branches.Remove(branches);
                SetAlignmentAdd();
            }));

        public BranchesViewModel()
        {
            _unitOfWork = new UnitOfWork();
            _windowBranch = new WindowBranch();
            Branches = new ObservableCollection<Branches>(AsyncContext.Run(async () => await _unitOfWork.BranchRepository.GetAllBranchesAsync()));
            SetAlignmentAdd();
        }

        private void SetAlignmentAdd()
        {
            if (Branches.Count == 0)
            {
                VerticalAlignmentAdd = VerticalAlignment.Center;
                HorizontalAlignmentAdd = HorizontalAlignment.Center;
            }
            else
            {
                VerticalAlignmentAdd = VerticalAlignment.Stretch;
                HorizontalAlignmentAdd = HorizontalAlignment.Stretch;
            }
        }
    }
}
