using Wms.API.Models;
using Wms.API.Contract;
using Wms.API.Interface;
using System.Threading.Tasks;
using Wms.UnitOfWorkAPI.RepositoryAPI.Contract;

namespace Wms.UnitOfWorkAPI.RepositoryAPI
{
    public class BranchRepository : IBranchRepository
    {
        private readonly IBranch _branch;
        public BranchRepository(IRest rest) => _branch = rest.ExecuteRequest<IBranch>();
        public async Task<Branch> GetAllBranchesAsync()=> await _branch.GetAllBranchAsync();
        public async Task<Error> DeleteBranchAsync(int id)=> await _branch.DeleteBranchAsync(id);
        public Task<Error> CreateBranchAsync(BranchCreate branch)=> _branch.CreateBranchAsync(branch);
        public Task<Error> EditBranchAsync(int id, BranchBase branch)=> _branch.EditBranchAsync(id, branch);
    }
}
