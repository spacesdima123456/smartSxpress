using System;
using System.Windows;
using Wms.ViewModel;

namespace Wms.WindowDialog
{
    /// <summary>
    /// Логика взаимодействия для BranchDelete.xaml
    /// </summary>
    public partial class BranchDelete : Window
    {
        public BranchDelete(Action<object> action)
        {
            InitializeComponent();
            DataContext = new BranchDeleteViewModel(action);
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
