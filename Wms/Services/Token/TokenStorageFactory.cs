using Wms.Services.Token.Contract;
using Wms.Services.Token.Enum;
using Wms.Services.Token.Providers;

namespace Wms.Services.Token
{
    public class TokenStorageFactory : ITokenStorageFactory
    {
        public ITokenStorage MakeStorage(TypeStorage type)
        {
            return type switch
            {
                TypeStorage.Registry => new StorageRegistry(),
                _ => throw new System.NotImplementedException()
            };
        }
    }
}
