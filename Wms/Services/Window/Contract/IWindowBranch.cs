using System;

namespace Wms.Services.Window.Contract
{
    public interface IWindowBranch
    {
        void Create(Action<object> action);
        void Delete(Action<object> action);
        void Edit(Action<object> action);
    }
}
