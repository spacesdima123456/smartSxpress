using Wms.Token.Contract;
using Wms.Token.Enum;
using Wms.Token.Providers;

namespace Wms.Token
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
