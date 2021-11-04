using System;
using System.Windows;
using Wms.View.WindowDialog;
using Wms.Services.Window.Contract;
using static Wms.Helpers.Translator;

namespace Wms.Services.Window.WindowDialogs
{
    public class WindowPackage : IWindowPackage
    {
        public void ShowDisplayAlertSend(Action<object> ok)
        {
            CreateWindow(new DisplayAlert("TittleSendPackage", "Yes", "No", "MsgSendPackage", ok)).CreateWindow();
        }

        public void Warning()
        {
            var message = Translate("WrgPrnPackage");
            MessageBox.Show(message);
        }

        private static WindowDialog CreateWindow(System.Windows.Window window)
        {
            return new WindowDialog(window);
        }
    }
}
