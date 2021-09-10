using Wms.API.Contract;

namespace Wms.UnitOfWorkAPI.RepositoryAPI
{
    public class RepositoryBase
    {
        protected IRest Rest;

        public RepositoryBase(IRest rest)
        {
            Rest = rest;
        }
    }
}
