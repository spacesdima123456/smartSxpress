using System;
using System.Windows;
using DevExpress.Xpf.Editors;
using Wms.ViewModel.Dialog;

namespace Wms.View.WindowDialog
{
    public sealed partial class DisplayAlertBranch : DisplayAlertBase
    {
        public DisplayAlertBranch(Action<DisplayAlertBranchViewModel> action)
        {
            InitializeComponent();
            SetWindowStartupLocation();
            DataContext = new DisplayAlertBranchViewModel(action);
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void OnValueChanged(object sender, EditValueChangedEventArgs e)
        {
            var vm = (DisplayAlertBranchViewModel) DataContext;
            if (e.NewValue!=null && e.OldValue!=null)
            {
                if (!vm.IsEnabled && e.NewValue != e.OldValue)
                    vm.IsEnabled = true;
                if (sender is ComboBoxEdit && e.NewValue != e.OldValue)
                    TxePhone.Clear();
            }
        }
    }
}
