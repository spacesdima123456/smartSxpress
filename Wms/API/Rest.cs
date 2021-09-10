using Refit;
using System;
using System.Net.Http;
using Wms.API.Contract;
using System.Net.Http.Headers;

namespace Wms.API
{
    public class Rest : IRest
    {
        public T ExecuteRequest<T>()
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(Properties.Settings.Default.HostUrl),
                Timeout = TimeSpan.FromSeconds(Properties.Settings.Default.Timeout)
            };
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("apiKey", Properties.Settings.Default.Token);

            client.DefaultRequestHeaders.Add("apiKey", Properties.Settings.Default.Token);


            return RestService.For<T>(client);
        }
    }
}
