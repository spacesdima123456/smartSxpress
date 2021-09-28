using Wms.API.Models;
using System.Threading.Tasks;

namespace Wms.UnitOfWorkAPI.RepositoryAPI.Contract
{
    public interface IAccountRepository
    {
        Task<Error> ChangeAccountAsync(Account account);
        Task<Error> ChangePasswordAsync(Password password);
    }
}
