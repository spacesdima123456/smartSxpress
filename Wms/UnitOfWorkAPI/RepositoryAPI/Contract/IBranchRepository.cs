using Wms.API.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Wms.UnitOfWorkAPI.RepositoryAPI.Contract
{
    public interface IBranchRepository 
    {
        Task DeleteBranchAsync(int id);
        Task CreateBranchAsync(BranchCreate branch);
        Task<IEnumerable<Branches>> GetAllBranchesAsync();
        Task<Error> EditBranchAsync(int id, BranchBase branch);
    }
}
