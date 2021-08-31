using Wms.API.Contract;

namespace Wms.API
{
    public class RestFactory : IRestFactory
    {
        public IRest CreateRest()
        {
            return new Rest();
        }
    }
}
