namespace Wms.Token.Contract
{
    public interface ITokenStorage
    {
        void SaveToken(string key, string path);
        string GetToken(string path);
    }
}
