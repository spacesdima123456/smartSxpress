using System;
using Wms.ViewModel.Dialog;
using Wms.View.WindowDialog;
using Wms.Services.Window.Contract;
using Wms.API.Models;

namespace Wms.Services.Window.WindowDialogs
{
    public class WindowBranch : IWindowBranch
    {
        private  System.Windows.Window _window;
        public void Create(Action<DisplayAlertBranchBaseViewModel> action)
        {
            CreateWindow(new DisplayAlertBranch(action)).CreateWindow();
        }

        public void Delete(Action<object> action)
        {
            CreateWindow(new DisplayAlert("DeleteBranchTittle", "Yes", "No", "MsgDeleteBranch", action)).CreateWindow();
        }

        public void Edit(Branches branches, Action<DisplayAlertBranchBaseViewModel> action)
        {
            CreateWindow(new DisplayAlertBranch(branches, action)).CreateWindow();
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
