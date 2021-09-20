using Wms.API.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using Wms.API.Models.Wms.API.Models;

namespace Wms.UnitOfWorkAPI.RepositoryAPI.Contract
{
    public interface IBranchRepository 
    {
        Task<IEnumerable<Branches>> GetAllBranchesAsync();
        Task DeleteBranchAsync(int id);
        Task<Error> EditBranchAsync(int id, BranchBase branch);
    }
}
