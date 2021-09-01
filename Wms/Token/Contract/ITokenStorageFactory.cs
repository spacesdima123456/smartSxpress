using Wms.Token.Contract;
using Wms.Token.Enum;

namespace Wms.Token
{
    public interface ITokenStorageFactory
   {
       ITokenStorage MakeStorage(TypeStorage type);
   }
}
