using Wms.Helpers;
using Wms.Services.Token.Contract;

namespace Wms.Services.Token.Providers
{
    public class StorageRegistry: ITokenStorage
    {
       // private bool _disposed;

        public string GetToken(string path)
        {
            //using var registry = GetRegistryKey();
            //return (string)registry.GetValue(path);
            return RegistryWin.GetValue(path);
        }

        public void SetToken(string apiKey, string path)
        {
            //using var registry = GetRegistryKey(true);
            //registry.SetValue(path, apiKey);

            RegistryWin.SetValue(path, apiKey);
        }

        //private static RegistryKey GetRegistryKey(bool writable = false)
        //{
        //    var reg = Registry.CurrentUser.OpenSubKey(@"Software\WMS", writable) ?? Registry.CurrentUser.CreateSubKey(@"Software\WMS", writable);
        //    return reg;
        //}

        //public void Dispose()
        //{
        //    Dispose(true);
        //    GC.SuppressFinalize(this);
        //}

        //protected virtual void Dispose(bool disposing)
        //{
        //    if (!_disposed)
        //    {
        //        if (disposing)
        //            GetRegistryKey().Dispose();
        //        _disposed = true;
        //    }
        //}

        //~StorageRegistry()
        //{
        //    Dispose(false);
        //}
    }
}
