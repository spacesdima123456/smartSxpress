using Wms.API.Models;
using System.Threading.Tasks;

namespace Wms.UnitOfWorkAPI.RepositoryAPI.Contract
{
    public interface IAccountRepository
    {
        Task<Error> ChangePasswordAsync(string password);
        Task<Error> ChangeAccountAsync(Account account);
    }
}
