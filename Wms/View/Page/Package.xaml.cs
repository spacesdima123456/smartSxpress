using System.Linq;
using DevExpress.Mvvm.Native;
using System.Windows.Controls;

namespace Wms.View.Page
{
    public partial class Package : System.Windows.Controls.Page
    {
        public Package()
        {
            InitializeComponent();
        }

        private void DtgLoaded(object sender, System.Windows.RoutedEventArgs e)
        {
            ((DataGrid)sender).Columns.AsParallel().ForEach(column =>
           {
               column.MinWidth = column.ActualWidth;
               column.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
           });
        }
    }
}
