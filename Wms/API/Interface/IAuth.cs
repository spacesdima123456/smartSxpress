using System.Threading.Tasks;
using Refit;
using Wms.API.Models;

namespace Wms.API.Interface
{
    [Headers("Content-Type: application/json")]
    public interface IAuth
    {
        [Post("/auth/login")]
        Task<LoginRes> LogInAsync([Body] LoginReq login);
    }
}
