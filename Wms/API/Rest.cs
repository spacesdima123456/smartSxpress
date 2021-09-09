using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Refit;
using Wms.API.Contract;
using Wms.Services.Token;
using Wms.Services.Token.Contract;
using Wms.Services.Token.Enum;

namespace Wms.API
{
    public class Rest : IRest
    {
        public T ExecuteRequest<T>()
        {
            using var tokenStorageFactory = new TokenStorageFactory().MakeStorage(TypeStorage.Registry);
            var client = new HttpClient
            {
                BaseAddress = new Uri(Properties.Settings.Default.HostUrl),
                Timeout = TimeSpan.FromSeconds(15)
            };
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("apiKey", tokenStorageFactory.GetToken("apiKey"));
            return RestService.For<T>(client);
        }
    }
}
