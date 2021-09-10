using System.Threading.Tasks;
using Wms.API.Models;

namespace Wms.UnitOfWorkAPI.RepositoryAPI.Contract
{
    public interface IAuthorizationRepository
    {
        Task LogOutAsync(string token);
        Task<LoginRes> LogInAsync(LoginReq login);
        Task<LoginRes> ValidKeyAsync(string token);
    }
}
