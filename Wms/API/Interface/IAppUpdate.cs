using System.Threading.Tasks;
using Refit;
using Wms.API.Models;

namespace Wms.API.Interface
{
    public interface IAppUpdate
    {
        [Get("/app/checkUpdate")]
        Task<VersionApp> GetActualVersionAppAsync();
    }
}
