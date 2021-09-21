using System.Threading.Tasks;
using Wms.API.Contract;
using Wms.API.Interface;
using Wms.API.Models;
using Wms.UnitOfWorkAPI.RepositoryAPI.Contract;

namespace Wms.UnitOfWorkAPI.RepositoryAPI
{
    public class AuthorizationRepository : RepositoryBase, IAuthorizationRepository
    {
        public AuthorizationRepository(IRest rest) : base(rest) { }
        public async Task<Response> LogInAsync(Login login) => await Rest.ExecuteRequest<IAuth>().LogInAsync(login);
        public async Task LogOutAsync(string token) => await Rest.ExecuteRequest<IAuth>().LogOutAsync(token);
        public async Task<Response> ValidKeyAsync(string token) => await Rest.ExecuteRequest<IAuth>().ValidTokenAsync(token);
    }
}
