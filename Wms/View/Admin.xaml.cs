using System.Globalization;
using System.Windows;
using System.Windows.Input;
using Wms.Localization;
using Wms.Properties;
using Wms.Services.Window;
using Wms.Services.Window.Contract;
using Wms.Services.Window.WindowDialogs;
using Wms.View.WindowDialog;
using Wms.ViewModel;

namespace Wms.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Admin : Window
    {
        private IWindowFactory _window;
        private IWindowLogOut _windowLogOut;
        private DisplayAlertSettings _displayAlertSettings;

        public Admin()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            TranslationSource.Instance.CurrentCulture = new CultureInfo("ru-RU");
            Settings.Default.DefaultLanguage = "ru-RU";
            Settings.Default.Save();

        }

        private void ButtonBase_OnClick1(object sender, RoutedEventArgs e)
        {
            TranslationSource.Instance.CurrentCulture = new CultureInfo("en-US");
            Settings.Default.DefaultLanguage = "en-US";
            Settings.Default.Save();
        }

        private void ButtonBase_OnClick3(object sender, RoutedEventArgs e)
        {
            TranslationSource.Instance.CurrentCulture = new CultureInfo("zh-CN");
            Settings.Default.DefaultLanguage = "zh-CN";
            Settings.Default.Save();
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



