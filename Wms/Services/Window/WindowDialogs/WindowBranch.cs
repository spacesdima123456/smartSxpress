using System;
using Wms.View.WindowDialog;
using Wms.Services.Window.Contract;

namespace Wms.Services.Window.WindowDialogs
{
    public class WindowBranch : IWindowBranch
    {
        public void Create()
        {
            CreateWindow(new DisplayAlertBranch()).Show();
        }

        public void Delete(Action<object> action)
        {
            CreateWindow(new DisplayAlert("DeleteBranchTittle", "Yes", "No", "MsgDeleteBranch", action)).CreateWindow();
        }

        public void Edit(Action<object> action)
        {
            CreateWindow(new DisplayAlertBranch()).Show();
        }

        private static WindowDialog CreateWindow(System.Windows.Window window)=> new WindowDialog(window);
    }
}
