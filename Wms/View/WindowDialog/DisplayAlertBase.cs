using System.Windows;

namespace Wms.View.WindowDialog
{
    public  partial class DisplayAlertBase: Window
    {
        public  virtual void SetWindowStartupLocation()
        {
            WindowStartupLocation = WindowStartupLocation.CenterOwner;
            Owner = Application.Current.MainWindow;
        }
    }
}
