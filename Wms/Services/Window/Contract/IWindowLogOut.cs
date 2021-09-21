using System;

namespace Wms.Services.Window.Contract
{
    public interface IWindowLogOut
    {
        void LogOut(Action<object> action);
    }
}
