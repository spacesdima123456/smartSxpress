namespace Wms.API.Contract
{
    public interface IRest
    {
        T ExecuteRequest<T>();
    }
}
