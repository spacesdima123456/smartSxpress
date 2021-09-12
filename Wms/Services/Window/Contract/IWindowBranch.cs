using System;

namespace Wms.Services.Window.Contract
{
    public interface IWindowBranch
    {
        void Delete(Action<object> action);
        void Edit(Action<object> action);
    }
}
