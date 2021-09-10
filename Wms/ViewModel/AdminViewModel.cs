using Wms.API.Models;
using DevExpress.Mvvm;
using System.Collections.Generic;
using Nito.AsyncEx;
using Wms.UnitOfWorkAPI;
using Wms.UnitOfWorkAPI.Contract;


namespace Wms.ViewModel
{
    public class AdminViewModel : BaseViewModel
    {
        private readonly IUnitOfWork _unitOfWork;

        private IEnumerable<Branches> _branches;
        public IEnumerable<Branches> Branches
        {
            get => _branches;
            private set => Set(nameof(Branches), ref _branches, value);
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

        public AdminViewModel()
        {
            _unitOfWork = new UnitOfWork();
            Branches = AsyncContext.Run(async ()=> await _unitOfWork.BranchRepository.GetAllBranchesAsync());

            Messenger.Default.Register<LoginRes>(this, (data) =>
            {
                UserName = data.Data.Customer.Name;
                Company = data.Data.Customer.Company;
            });
        }
    }
}
