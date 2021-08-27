using System.Globalization;
using System.Windows;
using WpfApp3.Localization;
using WpfApp3.Properties;

namespace WpfApp3
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            TranslationSource.Instance.CurrentCulture = new CultureInfo(Settings.Default.DefaultLanguage);
            UpdatingApp.RunUpdate();
        }
    }
}
