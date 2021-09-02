using Wms.Services.Token.Enum;

namespace Wms.Services.Token.Contract
{
    public interface ITokenStorageFactory
   {
       ITokenStorage MakeStorage(TypeStorage type);
   }
}
