using Refit;
using Wms.API.Contract;

namespace Wms.API
{
    public class Rest : IRest
    {
        public T ExecuteRequest<T>()
        {
            return RestService.For<T>(Properties.Settings.Default.HostUrl);
        }
    }
}
