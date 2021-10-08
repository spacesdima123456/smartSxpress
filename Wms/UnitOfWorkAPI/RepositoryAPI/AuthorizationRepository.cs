using Wms.API.Models;
using Wms.API.Contract;
using Wms.API.Interface;
using System.Threading.Tasks;
using Wms.UnitOfWorkAPI.RepositoryAPI.Contract;

namespace Wms.UnitOfWorkAPI.RepositoryAPI
{
    public class AuthorizationRepository : IAuthorizationRepository
    {
        private readonly IAuth _auth;
        public AuthorizationRepository(IRest rest) => _auth = rest.ExecuteRequest<IAuth>();
        public async Task<Response> LogInAsync(Login login) => await _auth.LogInAsync(login);
        public async Task LogOutAsync(string token) => await _auth.LogOutAsync(token);
        public async Task<Response> ValidKeyAsync(string token) => await _auth.ValidTokenAsync(token);
    }
}
