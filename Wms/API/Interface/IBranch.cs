using Refit;
using Wms.API.Models;
using System.Threading.Tasks;

namespace Wms.API.Interface
{
    public interface IBranch
    {
        [Get("/branches/all")]
        Task<Branch> GetAllBranchAsync();

        [Delete("/branches/remove/{id}")]
        Task DeleteBranchAsync(int id);
    }
}
