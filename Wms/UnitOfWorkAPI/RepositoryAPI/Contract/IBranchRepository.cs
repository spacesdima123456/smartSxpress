using Wms.API.Models;
using System.Threading.Tasks;

namespace Wms.UnitOfWorkAPI.RepositoryAPI.Contract
{
    public interface IBranchRepository 
    {
        Task<Branch> GetAllBranchesAsync();
        Task<Error> DeleteBranchAsync(int id);
        Task<Error> CreateBranchAsync(BranchCreate branch);
        Task<Error> EditBranchAsync(int id, BranchBase branch);
    }
}
