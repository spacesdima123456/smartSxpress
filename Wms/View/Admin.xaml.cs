using System.Globalization;
using System.Windows;
using System.Windows.Input;
using Wms.Localization;
using Wms.Properties;

namespace Wms.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Admin : Window
    {

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
            TopPanel.Visibility = Visibility.Collapsed;
        }
    }
}



