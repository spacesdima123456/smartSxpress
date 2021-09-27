using System;
using System.Windows;
using Wms.API.Models;
using Wms.ViewModel.Dialog;
using DevExpress.Xpf.Editors;

namespace Wms.View.WindowDialog
{
    public sealed partial class DisplayAlertBranch : DisplayAlertBase
    {
        public DisplayAlertBranch()
        {
            InitializeComponent();
        }

        public DisplayAlertBranch(Action<DisplayAlertBranchViewModel> action): this()
        {
            DataContext = new DisplayAlertBranchViewModel(action);
        }
        public DisplayAlertBranch(Branches branches, Action<DisplayAlertBranchViewModel> action) : this()
        {
            DataContext = new DisplayAlertBranchViewModel(branches, action);
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
