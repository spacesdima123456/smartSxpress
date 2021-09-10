using Refit;
using Wms.API.Models;
using System.Threading.Tasks;

namespace Wms.API.Interface
{
    public interface IReference
    {
        [Get("/branches/all")]
        Task<Branch> GetAllBranchAsync();
    }
}
