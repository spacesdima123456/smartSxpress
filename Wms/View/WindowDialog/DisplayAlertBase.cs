using System.Windows;

namespace Wms.View.WindowDialog
{
    public  partial class DisplayAlertBase: Window
    {
        protected  virtual void SetWindowStartupLocation()
        {
            WindowStartupLocation = WindowStartupLocation.CenterOwner;
            Owner = Application.Current.MainWindow;
        }
    }
}
