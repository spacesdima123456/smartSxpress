using System.Threading.Tasks;
using Wms.API.Models;

namespace Wms.Services.Authorization.Contract
{
    public interface IAuthorization
    {
        bool IsAuth { get; }
        void LogOut();
        Task LogInAsync(LoginReq login);
        Task<LoginRes> ValidKeyAsync();
    }
}
