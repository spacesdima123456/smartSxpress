using System;
using System.Windows.Input;
using DevExpress.Mvvm;

namespace Wms.ViewModel
{
    public class BranchDeleteViewModel: BaseViewModel
    {
        private readonly Action<object> _action;

        private ICommand _deleteCommand;
        public  ICommand DeleteCommand=> _deleteCommand ??= new DelegateCommand(()=>_action(this));
        public BranchDeleteViewModel(Action<object> action)
        {
            _action = action;
        }
    }
}
