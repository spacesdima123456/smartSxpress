using Refit;
using Wms.API.Models;
using System.Threading.Tasks;

namespace Wms.API.Interface
{
    [Headers("Content-Type: application/json")]
    public interface IAuth
    {
        [Post("/auth/login")]
        Task<Response> LogInAsync([Body] Login login);

        [Get("/auth/keyCheck")]
        Task<Response> ValidTokenAsync([Header("apikey")] string token);

        [Get("/auth/logout")]
        Task LogOutAsync([Header("apikey")] string token);
    }
}

