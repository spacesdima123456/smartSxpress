using System.Linq;
using Wms.API.Models;
using Wms.API.Contract;
using Wms.API.Interface;
using System.Threading.Tasks;
using System.Collections.Generic;
using Wms.UnitOfWorkAPI.RepositoryAPI.Contract;

namespace Wms.UnitOfWorkAPI.RepositoryAPI
{
    public class BranchRepository : IBranchRepository
    {
        private readonly IBranch _branch;

        public BranchRepository(IRest rest) => _branch = rest.ExecuteRequest<IBranch>();

        public async Task<IEnumerable<Branches>> GetAllBranchesAsync()
        {
            var branches = await _branch.GetAllBranchAsync();
            if (branches.Code == 1)
                return branches.Branches;
            return Enumerable.Empty<Branches>();
        }

        public async Task DeleteBranchAsync(int id)=> await _branch.DeleteBranchAsync(id);
        public Task CreateBranchAsync(BranchCreate branch)=> _branch.CreateBranchAsync(branch);

        public Task<Error> EditBranchAsync(int id, BranchBase branch)=> _branch.EditBranchAsync(id, branch);

    }
}
