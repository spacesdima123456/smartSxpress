using System.Threading.Tasks;
using Wms.API.Models;

namespace Wms.Services.Authorization.Contract
{
    public interface IAuthorization
    {
        bool IsAuth { get; }
        Task LogOutAsync();
        Task LogInAsync(Login login);
        Task<Response> ValidKeyAsync();
    }
}
