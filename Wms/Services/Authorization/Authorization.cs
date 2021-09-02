using System.Threading.Tasks;
using Wms.API;
using Wms.API.Contract;
using Wms.API.Interface;
using Wms.API.Models;
using Wms.Services.Authorization.Contract;
using Wms.Services.Token.Contract;
using Wms.Services.Token.Providers;

namespace Wms.Services.Authorization
{
    public class Authorization : IAuthorization
    {
        private readonly IRest _rest;
        private readonly ITokenStorage _tokenStorage;

        public Authorization()
        {
            _rest = new Rest();
            _tokenStorage = new StorageRegistry();
        }

        public bool IsAuth => !string.IsNullOrEmpty(_tokenStorage.GetToken("ApiKey"));

        public async Task LogInAsync(LoginReq login)
        {
            var auth = await _rest.ExecuteRequest<IAuth>().LogInAsync(login);
            if (auth != null)
                _tokenStorage.SetToken(auth.ApiKey, "ApiKey");
        }

        public void LogOut()
        {
            _tokenStorage.SetToken("", "ApiKey");
        }

        public async Task<LoginRes> ValidKeyAsync()
        {
            return await _rest.ExecuteRequest<IAuth>().ValidTokenAsync(_tokenStorage.GetToken("ApiKey"));
        }
    }
}
