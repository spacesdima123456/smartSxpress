using System.Windows;
using System.Windows.Input;
using DevExpress.Xpf.Editors;
using Wms.ViewModel.Dialog;

namespace Wms.View.WindowDialog
{
    public sealed partial class DisplayAlertBranch : DisplayAlertBase
    {
        public DisplayAlertBranch()
        {
            InitializeComponent();
            SetWindowStartupLocation();
            DataContext = new DisplayAlertBranchViewModel();
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
                if (e.NewValue != e.OldValue)
                    TxePhone.Clear();
            }
        }
    }
}
