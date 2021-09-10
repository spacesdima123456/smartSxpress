using System.Linq;
using Wms.API.Models;
using Wms.API.Interface;
using System.Threading.Tasks;
using System.Collections.Generic;
using Wms.API.Contract;
using Wms.UnitOfWorkAPI.RepositoryAPI.Contract;

namespace Wms.UnitOfWorkAPI.RepositoryAPI
{
    public class BranchRepository : RepositoryBase, IBranchRepository
    {
        public BranchRepository(IRest rest) : base(rest) { }

        public async Task<IEnumerable<Branches>> GetAllBranchesAsync()
        {
            var branches = await Rest.ExecuteRequest<IReference>().GetAllBranchAsync();
            if (branches.Code == 1)
                return branches.Branches;
            return Enumerable.Empty<Branches>();
        }
    }
}
