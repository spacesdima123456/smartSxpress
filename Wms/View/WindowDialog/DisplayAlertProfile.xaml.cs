using System;
using DevExpress.Xpf.Editors;
using Wms.ViewModel.Dialog;

namespace Wms.View.WindowDialog
{
    public partial class DisplayAlertProfile : DisplayAlertBase
    {
        public DisplayAlertProfile(Action<DisplayAlertBranchBaseViewModel> action, Action<DisplayAlertProfileViewModel> passwordAction)
        {
            InitializeComponent();
            DataContext = new DisplayAlertProfileViewModel(action, passwordAction);
        }

        private void OnValueChanged(object sender, EditValueChangedEventArgs e)
        {
            var vm = (DisplayAlertBranchBaseViewModel)DataContext;
            if (e.NewValue != null && e.OldValue != null)
            {
                if (!vm.IsEnabled && e.NewValue != e.OldValue)
                    vm.IsEnabled = true;
                if (sender is ComboBoxEdit && e.NewValue != e.OldValue)
                    TxePhone.Clear();
            }
        }
    }
}
