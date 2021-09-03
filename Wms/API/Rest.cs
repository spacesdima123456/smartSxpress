using System;
using System.Net.Http;
using Refit;
using Wms.API.Contract;

namespace Wms.API
{
    public class Rest : IRest
    {
        public T ExecuteRequest<T>()
        {
            return RestService.For<T>(new HttpClient
            {
                BaseAddress = new  Uri(Properties.Settings.Default.HostUrl),
                Timeout = TimeSpan.FromSeconds(15)
            });
        }
    }
}
