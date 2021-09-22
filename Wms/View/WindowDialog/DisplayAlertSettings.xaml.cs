using System.ComponentModel;
using System.Windows;

namespace Wms.View.WindowDialog
{
    public partial class DisplayAlertSettings : DisplayAlertBase
    {
        public DisplayAlertSettings()
        {
            InitializeComponent();
        }

        private void OnClosed(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            Visibility = Visibility.Collapsed;
        }
    }
}