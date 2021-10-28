using System.Threading.Tasks;
using Wms.API.Models;
using Wms.Services.Authorization.Contract;
using Wms.Services.Token.Contract;
using Wms.Services.Token.Providers;
using Wms.UnitOfWorkAPI;
using Wms.UnitOfWorkAPI.Contract;

namespace Wms.Services.Authorization
{
    public class Authorization : IAuthorization
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenStorage _tokenStorage;

        public Authorization()
        {
            _unitOfWork = new UnitOfWork();
            _tokenStorage = new StorageRegistry();
        }

        public bool IsAuth => !string.IsNullOrEmpty(GetToken());

        public async Task LogInAsync(Login login)
        {
            var auth = await _unitOfWork.AuthorizationRepository.LogInAsync(login);
            if (auth != null)
                _tokenStorage.SetToken(auth.ApiKey, "ApiKey");
        }

        public async Task LogOutAsync()
        {
            if (!string.IsNullOrEmpty(GetToken()))
                await _unitOfWork.AuthorizationRepository.LogOutAsync(GetToken());
            _tokenStorage.SetToken("", "ApiKey");
        }

        public async Task<Response> ValidKeyAsync()=> await _unitOfWork.AuthorizationRepository.ValidTokenAsync(GetToken());

        private string GetToken() => _tokenStorage.GetToken("ApiKey");
    }
}
