using System.Collections.Generic;
using System.Threading.Tasks;
using Wms.API.Models;

namespace Wms.UnitOfWorkAPI.RepositoryAPI.Contract
{
    public interface IBranchRepository 
    {
        Task<IEnumerable<Branches>> GetAllBranchesAsync();
        Task DeleteBranchAsync(int id);
    }
}
