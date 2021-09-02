namespace Wms.Services.Authorization.Contract
{
    public interface IAuthorizationFactory
    {
        IAuthorization Make();
    }
}
