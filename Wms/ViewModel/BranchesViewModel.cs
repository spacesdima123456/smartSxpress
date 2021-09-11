using Nito.AsyncEx;
using Wms.API.Models;
using Wms.UnitOfWorkAPI;
using System.Collections.ObjectModel;

namespace Wms.ViewModel
{
    public class BranchesViewModel : BaseViewModel
    {
        private ObservableCollection<Branches> _branches;
        public ObservableCollection<Branches> Branches
        {
            get => _branches;
            private set => Set(nameof(Branches), ref _branches, value);
        }

        public BranchesViewModel()
        {
            var unitOfWork = new UnitOfWork();
            var branches=  AsyncContext.Run(async () => await unitOfWork.BranchRepository.GetAllBranchesAsync());
            Branches = new ObservableCollection<Branches>(branches); 
        }
    }
}
