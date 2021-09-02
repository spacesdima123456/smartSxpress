using System.Globalization;
using System.Windows;
using Wms.Localization;
using Wms.Properties;
using Wms.Services.Updater;


namespace Wms
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
