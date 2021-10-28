using Wms.API;
using Wms.API.Contract;
using Wms.API.Interface;
using Wms.UnitOfWorkAPI.Contract;

namespace Wms.UnitOfWorkAPI
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IRest _rest;
        public UnitOfWork() => _rest = new RestFactory().CreateRest();

        private IBranch _branchRepository;
        public IBranch BranchRepository => _branchRepository??= _rest.ExecuteRequest<IBranch>();

        private IPackage _packageRepository;
        public IPackage PackageRepository=> _packageRepository??= _rest.ExecuteRequest<IPackage>();

        private IAccount _accountRepository;
        public IAccount AccountRepository => _accountRepository??= _rest.ExecuteRequest<IAccount>();

        private IAuth _authorizationRepository;
        public IAuth AuthorizationRepository => _authorizationRepository??= _rest.ExecuteRequest<IAuth>();
    }
}
