using System;
using Wms.API.Models;
using System.Windows;

namespace Wms.ViewModel.Dialog
{
    public class DisplayAlertBranchViewModel : DisplayAlertBranchBaseViewModel
    {
        public DisplayAlertBranchViewModel(Action<DisplayAlertBranchBaseViewModel> action) : base(action)
        {
            InitProperty(480, "Create", true, null, string.Empty, string.Empty, string.Empty,
                string.Empty, string.Empty, App.Data.Data.Customer.Company, string.Empty, Visibility.Visible, "Create",
                App.Data.Data.Customer.CountryName);
        }

        public DisplayAlertBranchViewModel(Branches branches, Action<DisplayAlertBranchBaseViewModel> action) :
            base(action)
        {
            InitProperty(395, "Edit", false, branches.Zip, branches.Name, branches.City, branches.Phone,
                branches.State, branches.Email, branches.Company, branches.Address, Visibility.Collapsed, "Done",
                branches.Code);
        }
    }
}
