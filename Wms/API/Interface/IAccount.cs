using Refit;
using Wms.API.Models;
using System.Threading.Tasks;

namespace Wms.API.Interface
{
    public interface IAccount
    {
        [Put("/profile/changeInfo")]
        [Headers("Content-Type: application/json")]
        Task<Error> ChangeAccountAsync([Body] Account account);

        [Put("/profile/changePassword")]
        [Headers("Content-Type: application/json")]
        Task<Error> ChangePasswordAsync([Body] Password password);
    }
}
