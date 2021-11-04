using Refit;
using Wms.API.Models;
using System.Dynamic;
using System.Threading.Tasks;

namespace Wms.API.Interface
{
    public interface IPackage
    {
        [Get("/find/{typeCustomer}/{typeDoc}/{docNum}")]
        Task<CustomerDoc<T>> FindCustomerInfoAsync<T>(string typeCustomer, int typeDoc, string docNum);

        [Post("/accept/parcel")]
        [Headers("Content-Type: application/json")]
        Task<string> SendAsync(ExpandoObject package);
    }
}
