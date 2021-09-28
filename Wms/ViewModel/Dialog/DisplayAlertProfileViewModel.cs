using System;
using System.Windows;
using DevExpress.Mvvm;
using System.Windows.Input;

namespace Wms.ViewModel.Dialog
{
    public class DisplayAlertProfileViewModel : DisplayAlertBranchBaseViewModel
    {

        private readonly Action<DisplayAlertProfileViewModel> _passwordAction;

        private ICommand _changePasswordCommand;

        public virtual ICommand ChangePasswordCommand => _changePasswordCommand ??= new DelegateCommand(() =>
        {
            ClearErrors();
            ValidatePassword();
            if (Errors.Count == 0)
            {
                _passwordAction.Invoke(this);
            }
        });

        private ICommand _doneCommand;
        public override ICommand DoneCommand => _doneCommand ??= new DelegateCommand(() =>
        {
            ClearErrors();
            ValidateForm();
            if (Errors.Count == 0)
            {
                Action.Invoke(this);
            }
        });

        public DisplayAlertProfileViewModel(Action<DisplayAlertBranchBaseViewModel> action,
            Action<DisplayAlertProfileViewModel> passAction) : base(action)
        {
            _passwordAction = passAction;

            InitProperty(530,
                "Profile",
                true,
                App.Data.Data.Customer.Zip,
                App.Data.Data.Customer.Name,
                App.Data.Data.Customer.City,
                App.Data.Data.Customer.Phone,
                App.Data.Data.Customer.State,
                App.Data.Data.Customer.Email,
                App.Data.Data.Customer.Company,
                App.Data.Data.Customer.Address,
                Visibility.Visible,
                "Done",
                App.Data.Data.Customer.CountryName);
        }
    }
}
