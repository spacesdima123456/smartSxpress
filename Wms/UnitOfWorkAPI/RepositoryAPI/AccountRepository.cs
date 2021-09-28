using Wms.API.Models;
using Wms.API.Contract;
using Wms.API.Interface;
using System.Threading.Tasks;
using Wms.UnitOfWorkAPI.RepositoryAPI.Contract;

namespace Wms.UnitOfWorkAPI.RepositoryAPI
{
    public class AccountRepository : RepositoryBase, IAccountRepository
    {
        private readonly IAccount _account;
        public AccountRepository(IRest rest) : base(rest)=> _account = rest.ExecuteRequest<IAccount>();
        public  Task<Error> ChangeAccountAsync(Account account)=> _account.ChangeAccountAsync(account);
        public Task<Error> ChangePasswordAsync(Password password)=> _account.ChangePasswordAsync(password);
    }
}
