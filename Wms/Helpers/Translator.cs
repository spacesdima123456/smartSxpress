using System.Globalization;
using System.Resources;

namespace Wms.Helpers
{
    public static class Translator
    {
        private static readonly  CultureInfo CultureInfo = new CultureInfo(Properties.Settings.Default.DefaultLanguage);
        private static readonly ResourceManager Manager = new ResourceManager(typeof(Resources.Resources));
        public static string Translate(string property)
        {
            return Manager.GetString(property, CultureInfo);
        }
    }
}
