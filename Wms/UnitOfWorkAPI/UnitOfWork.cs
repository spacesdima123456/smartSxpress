using Wms.API;
using Wms.API.Contract;
using Wms.UnitOfWorkAPI.Contract;
using Wms.UnitOfWorkAPI.RepositoryAPI;
using Wms.UnitOfWorkAPI.RepositoryAPI.Contract;

namespace Wms.UnitOfWorkAPI
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IRest _rest;

        public UnitOfWork()
        {
            _rest = new RestFactory().CreateRest();
        }

        private BranchRepository _branch;
        public IBranchRepository BranchRepository => _branch ??= new BranchRepository(_rest);

        private AuthorizationRepository _authorization;
        public IAuthorizationRepository AuthorizationRepository => _authorization??=new AuthorizationRepository(_rest);

        private AccountRepository _accountRepository;
        public IAccountRepository AccountRepository => _accountRepository??=new AccountRepository(_rest);
    }
}
