using Microsoft.Win32;

namespace Wms.Helpers
{
    public static class RegistryWin
    {
        public static void SetValue(string value, string key)
        {
            using var registry = GetRegistryKey(true);
            registry.SetValue(value, key);
        }

        public static string GetValue(string value)
        {
            using var registry = GetRegistryKey();
            return (string)registry.GetValue(value);
        }

        private static RegistryKey GetRegistryKey(bool writable = false)
        {
            var reg = Registry.CurrentUser.OpenSubKey(@"Software\WMS", writable) ?? Registry.CurrentUser.CreateSubKey(@"Software\WMS", writable);
            return reg;
        }
    }
}
