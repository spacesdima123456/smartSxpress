using System;
using Wms.View.WindowDialog;
using Wms.Services.Window.Contract;

namespace Wms.Services.Window.WindowDialogs
{
    public class WindowLogOut : IWindowLogOut
    {
        public void LogOut(Action<object> action)
        {
            var window = new DisplayAlert("Exit", "Yes", "No", "MsgLogOut", action);
            window.ShowDialog();
        }
    }
}
