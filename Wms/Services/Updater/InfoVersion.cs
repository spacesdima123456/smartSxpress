using System;
using Wms.API;
using Nito.AsyncEx;
using System.Windows;
using Wms.API.Interface;
using System.Reflection;

namespace Wms.Services.Updater
{
    public class InfoVersion
    {
        public string Url { get; private set; }
        public Version NewVersion { get; private set; }
        public Version CurVersion { get; private set; }
        private static InfoVersion _instance;

        private InfoVersion()
        {
            CheckVerifyApp();
        }

        public static InfoVersion Instance()=> _instance ??= new InfoVersion();

        private void CheckVerifyApp()
        {
            try
            {
                var rest = new RestFactory().CreateRest();
                var version = AsyncContext.Run(async ()=> await rest.ExecuteRequest<IAppUpdate>().GetActualVersionAppAsync());
                if (version != null)
                {
                    Url = version.Url;
                    NewVersion = new Version(version.Version);
                }
                CurVersion = Assembly.GetExecutingAssembly().GetName().Version;

            }
            catch (Exception ex)
            {
                MessageBox.Show( ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
