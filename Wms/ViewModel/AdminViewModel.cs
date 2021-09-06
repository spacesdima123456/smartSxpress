using DevExpress.Mvvm;
using Wms.API.Models;

namespace Wms.ViewModel
{
    public class AdminViewModel : BaseViewModel
    {
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
            Messenger.Default.Register<LoginRes>(this, (data) =>
            {
                UserName = data.Data.Customer.Name;
                Company = data.Data.Customer.Company;
            });
        }
    }
}
