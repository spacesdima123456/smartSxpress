using System;
using Wms.ViewModel.Dialog;

namespace Wms.Services.Window.Contract
{
    public interface IWindowSettings
    {
        void ShowLoginPage();
        void HideProfileWindow();
        void ShowProfile(Action<DisplayAlertBranchBaseViewModel> action, Action<DisplayAlertProfileViewModel> passwordAction);
    }
}
