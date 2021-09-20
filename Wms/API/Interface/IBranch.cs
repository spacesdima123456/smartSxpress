using Refit;
using Wms.API.Models;
using System.Threading.Tasks;
using Wms.API.Models.Wms.API.Models;

namespace Wms.API.Interface
{
    public interface IBranch
    {
        [Get("/branches/all")]
        Task<Branch> GetAllBranchAsync();

        [Delete("/branches/remove/{id}")]
        Task DeleteBranchAsync(int id);

        [Put("/branches/edit/{id}")]
        [Headers("Content-Type: application/json")]
        Task<Error> EditBranchAsync(int id, [Body] BranchBase branch);

        //[Post("/branches/add")]
        //[Headers("Content-Type: application/json")]
        //Task CreateBranchAsync(BranchCreate branch);
    }
}
