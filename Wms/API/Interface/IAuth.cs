using Refit;
using Wms.API.Models;
using System.Threading.Tasks;

namespace Wms.API.Interface
{
    [Headers("Content-Type: application/json")]
    public interface IAuth
    {
        [Post("/auth/login")]
        Task<LoginRes> LogInAsync([Body] LoginReq login);

        [Get("/auth/keyCheck")]
        Task<LoginRes> ValidTokenAsync([Header("apikey")] string token);

        [Get("/auth/logout")]
        Task LogOutAsync([Header("apikey")] string token);
    }
}
