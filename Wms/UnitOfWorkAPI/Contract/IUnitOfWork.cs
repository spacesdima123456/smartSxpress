using Wms.API.Interface;

namespace Wms.UnitOfWorkAPI.Contract
{
    public interface IUnitOfWork
    {
        IBranch BranchRepository { get; }
        IPackage PackageRepository { get; }
        IAccount AccountRepository { get; }
        IAuth AuthorizationRepository { get; }
    }
}
