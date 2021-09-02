using Microsoft.Win32;
using Wms.Services.Token.Contract;

namespace Wms.Services.Token.Providers
{
    public class StorageRegistry: ITokenStorage
    {
        public string GetToken(string path)
        {
            using var registry = GetRegistryKey();
            return (string)registry.GetValue(path);
        }

        public void SetToken(string apiKey, string path)
        {
            using var registry = GetRegistryKey(true);
            registry.SetValue(path, apiKey);
        }

        private static RegistryKey GetRegistryKey(bool writable = false)
        {
            var reg = Registry.CurrentUser.OpenSubKey(@"Software\WMS", writable) ?? Registry.CurrentUser.CreateSubKey(@"Software\WMS", writable);
            return reg;
        }
    }
}
