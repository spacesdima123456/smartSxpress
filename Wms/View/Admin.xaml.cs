using Wms.ViewModel;
using System.Windows;
using Wms.Services.Window;
using System.Windows.Input;
using Wms.View.WindowDialog;
using Wms.Services.Window.Contract;
using Wms.Services.Window.WindowDialogs;

namespace Wms.View
{
    public partial class Admin : Window
    {
        private IWindowFactory _window;
        private IWindowLogOut _windowLogOut;
        private DisplayAlertSettings _displayAlertSettings;

        public Admin()
        {
            InitializeComponent();
            App.Window = this;
        }

        private void OnSettings(object sender, MouseButtonEventArgs e)
        {
            TopPanel.Visibility = Visibility.Visible;
        }

        private void OnPreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!TopPanel.IsMouseOver)
                TopPanel.Visibility = Visibility.Collapsed;
        }

        private void LogOut(object sender, RoutedEventArgs e)
        {
            if (_windowLogOut==null) 
                _windowLogOut = new WindowLogOut();

            _windowLogOut.LogOut(async o =>
            {
                await ((AdminViewModel) DataContext).LogOutAsync();
                _window = new WindowFactory( this, new Login());
                _window.CreateWindow();
            });
        }

        private void ShowSettings(object sender, RoutedEventArgs e)
        {
            if (_displayAlertSettings == null)
                _displayAlertSettings = new DisplayAlertSettings();

            _displayAlertSettings.ShowDialog();
        }
    }
}



