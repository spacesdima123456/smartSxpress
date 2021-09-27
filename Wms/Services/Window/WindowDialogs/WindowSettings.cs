using System;
using Wms.ViewModel.Dialog;
using Wms.View.WindowDialog;
using Wms.Services.Window.Contract;

namespace Wms.Services.Window.WindowDialogs
{
    public class WindowSettings : IWindowSettings
    {
        private  DisplayAlertProfile _displayAlertProfile;

        public void HideProfileWindow()
        {
            _displayAlertProfile.Hide();
        }

        public void ShowProfile(Action<DisplayAlertBranchBaseViewModel> action, Action<DisplayAlertProfileViewModel> passwordAction)
        {
            _displayAlertProfile = new DisplayAlertProfile(action, passwordAction);
            _displayAlertProfile.ShowDialog();
        }
    }
}
