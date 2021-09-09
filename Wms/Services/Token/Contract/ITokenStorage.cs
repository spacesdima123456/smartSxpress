using System;

namespace Wms.Services.Token.Contract
{
    public interface ITokenStorage: IDisposable
    {
        void SetToken(string key, string path);
        string GetToken(string path);
    }
}
