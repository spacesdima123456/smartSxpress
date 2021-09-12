using System.Windows.Input;
using Wms.API.Models;
using DevExpress.Mvvm;

namespace Wms.ViewModel
{
    public class AdminViewModel : BaseViewModel
    {
        private string _source;
        public string Source
        {
            get => _source;
            private set => Set(nameof(Source), ref _source, value);
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

        private ICommand _pageCommand;
        public ICommand PageCommand => _pageCommand ??= new DelegateCommand<string>(source => Source = source);

        public AdminViewModel()
        {
            Messenger.Default.Register<LoginRes>(this, (data) =>
            {
                //Source = "../Page/Branches.xaml";
                UserName = data.Data.Customer.Name;
                Company = data.Data.Customer.Company;
            });
        }
    }
}
