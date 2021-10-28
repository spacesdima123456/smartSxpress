using Refit;
using Wms.API.Models;
using System.Threading.Tasks;

namespace Wms.API.Interface
{
    public interface IPackage
    {
        [Get("/find/{typeCustomer}/{typeDoc}/{docNum}")]
        Task<CustomerDoc<T>> FindCustomerInfoAsync<T>(string typeCustomer, int typeDoc, string docNum);
    }
}
