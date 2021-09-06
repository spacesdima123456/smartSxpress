using Refit;
using System;
using Nito.AsyncEx;
using Wms.Services.TokenVerify.Contract;
using Wms.Services.Authorization.Contract;

namespace Wms.Services.TokenVerify
{
    public class TokenVerify : ITokenVerify
    {
        public event EventHandler VerifyError;
        public event EventHandler VerifySuccess;
        private readonly IAuthorization _authorization;

        public TokenVerify(IAuthorization authorization)
        {
            _authorization = authorization;
        }

        public void VerifyApiToken()
        {
            if (_authorization.IsAuth)
            {
                try
                {
                    var data = AsyncContext.Run(async () => await _authorization.ValidKeyAsync());
                    if (data != null)
                        VerifySuccess?.Invoke(data, EventArgs.Empty);
                }
                catch (ApiException ex)
                {
                    VerifyError?.Invoke(ex, EventArgs.Empty);
                }
            }
        }
    }
}
