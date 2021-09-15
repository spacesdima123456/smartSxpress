using System.Windows;
using Wms.API.Models;
using Wms.Properties;
using DevExpress.Mvvm;
using Wms.Localization;
using DevExpress.Xpf.Core;
using System.Globalization;
using Wms.Services.Updater;

namespace Wms
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public  static LoginRes Data { get; private set; }

        public App()
        {
            TranslationSource.Instance.CurrentCulture = new CultureInfo(Settings.Default.DefaultLanguage);
            UpdatingApp.RunUpdate();

            SplashScreenManager.CreateThemed(new DXSplashScreenViewModel
                {
                    Copyright = "All rights reserved",
                    IsIndeterminate = true,
                    Status = "Starting...",
                    Title = "",
                    Subtitle = "Powered by DevExpress",
                }
            ).ShowOnStartup();
        }
        public static void SetDataKeyCheck(LoginRes data)
        {
            Data = data;
        }
    }
}
