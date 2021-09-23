using System.Windows;

namespace Wms.View.WindowDialog
{
    public  partial class DisplayAlertBase: Window
    {

        public DisplayAlertBase()
        {
            SetWindowStartupLocation();
        }

        private void SetWindowStartupLocation()
        {
            WindowStartupLocation = WindowStartupLocation.CenterOwner;
            //Owner = Application.Current.MainWindow;
            Owner = App.Window;
        }
    }
}
