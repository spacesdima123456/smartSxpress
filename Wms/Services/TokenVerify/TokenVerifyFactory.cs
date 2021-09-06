using Wms.Services.Authorization.Contract;
using Wms.Services.TokenVerify.Contract;

namespace Wms.Services.TokenVerify
{
    public class TokenVerifyFactory : ITokenVerifyFactory
    {
        private readonly IAuthorization _authorization;

        public TokenVerifyFactory(IAuthorization authorization)
        {
            _authorization = authorization;
        }

        public ITokenVerify Make()
        {
            return new TokenVerify(_authorization);
        }
    }
}
