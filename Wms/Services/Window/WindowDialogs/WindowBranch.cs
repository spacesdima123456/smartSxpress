using System;
using Wms.ViewModel.Dialog;
using Wms.View.WindowDialog;
using Wms.Services.Window.Contract;

namespace Wms.Services.Window.WindowDialogs
{
    public class WindowBranch : IWindowBranch
    {
        private  System.Windows.Window _window;
        public void Create(Action<object> action)
        {
            CreateWindow(new DisplayAlertBranch(action)).Show();
        }

        public void Delete(Action<object> action)
        {
            CreateWindow(new DisplayAlert("DeleteBranchTittle", "Yes", "No", "MsgDeleteBranch", action)).CreateWindow();
        }

        public void Edit(Action<DisplayAlertBranchViewModel> action)
        {
            CreateWindow(new DisplayAlertBranch(action)).Show();
        }

        private WindowDialog CreateWindow(System.Windows.Window window)
        {
            _window = window;
            return new WindowDialog(window);
        }

        public void Close()
        {
            _window.Close();
        }
    }
}
