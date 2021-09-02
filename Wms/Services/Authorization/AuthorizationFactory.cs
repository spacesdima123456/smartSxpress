using Wms.Services.Authorization.Contract;

namespace Wms.Services.Authorization
{
    public class AuthorizationFactory : IAuthorizationFactory
    {
        public IAuthorization Make()
        {
            return new Authorization();
        }
    }
}
