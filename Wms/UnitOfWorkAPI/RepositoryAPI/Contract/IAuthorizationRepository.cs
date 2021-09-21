using System.Threading.Tasks;
using Wms.API.Models;

namespace Wms.UnitOfWorkAPI.RepositoryAPI.Contract
{
    public interface IAuthorizationRepository
    {
        Task LogOutAsync(string token);
        Task<Response> LogInAsync(Login login);
        Task<Response> ValidKeyAsync(string token);
    }
}
