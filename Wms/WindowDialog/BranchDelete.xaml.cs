using System;
using System.Windows;
using Wms.ViewModel;

namespace Wms.WindowDialog
{
    public partial class BranchDelete : Window
    {
        public BranchDelete(Action<object> action)
        {
            InitializeComponent();
            SetWindowStartupLocation();
            DataContext = new BranchDeleteViewModel(action);
        }

        private void Close(object sender, RoutedEventArgs e) => Close();

        private void SetWindowStartupLocation()
        {
            var curApp = Application.Current;
            var mainWindow = curApp.MainWindow;
            if (mainWindow!=null)
            {
                Left = mainWindow.Left + (mainWindow.Width - ActualWidth) / 2;
                Top = mainWindow.Top + (mainWindow.Height - ActualHeight) / 2;
            }
        }
    }
}
