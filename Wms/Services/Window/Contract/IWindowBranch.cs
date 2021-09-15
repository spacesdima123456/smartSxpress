using System;

namespace Wms.Services.Window.Contract
{
    public interface IWindowBranch
    {
        void Create();
        void Delete(Action<object> action);
        void Edit(Action<object> action);
    }
}
