using System.Windows;
using Wms.API.Models;
using Wms.Properties;
using DevExpress.Mvvm;
using Wms.Localization;
using DevExpress.Xpf.Core;
using Wms.Services.Updater;
using System.Globalization;
using Wms.DependencyInjection;

namespace Wms
{
    public partial class App : Application
    {
        public static Window Window { get; set; }
        public static Response Data { get; private set; }

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
        public static void SetDataKeyCheck(Response data)
        {
            Data = data;
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            IocKernel.Initialize(new IocConfiguration());

            base.OnStartup(e);
        }
    }
}
