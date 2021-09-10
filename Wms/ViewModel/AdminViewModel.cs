using Wms.API;
using Wms.API.Models;
using DevExpress.Mvvm;
using Wms.API.Contract;
using System.Collections.Generic;

namespace Wms.ViewModel
{
    public class AdminViewModel : BaseViewModel
    {
        private readonly IRest _rest;

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
            _rest = new RestFactory().CreateRest();

            Messenger.Default.Register<LoginRes>(this, (data) =>
            {
                UserName = data.Data.Customer.Name;
                Company = data.Data.Customer.Company;
            });
        }
    }
}
