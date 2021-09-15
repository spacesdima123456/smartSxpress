using System.Windows;
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
    }
}
