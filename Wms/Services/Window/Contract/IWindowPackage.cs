using System;

namespace Wms.Services.Window.Contract
{
    public interface IWindowPackage
    {
        void Warning();
        void ShowDisplayAlertSend(Action<object> ok);
    }
}
