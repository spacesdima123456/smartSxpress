using Wms.UnitOfWorkAPI.RepositoryAPI.Contract;

namespace Wms.UnitOfWorkAPI.Contract
{
    public interface IUnitOfWork
    {
        IBranchRepository BranchRepository { get; }
        IAuthorizationRepository AuthorizationRepository { get; }
    }
}
