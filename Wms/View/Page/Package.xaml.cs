using Wms.ViewModel.Page;
using System.Windows.Input;

namespace Wms.View.Page
{
    public partial class Package : System.Windows.Controls.Page
    {
        public Package()
        {
            InitializeComponent();
        }

        //private void DtgLoaded(object sender, System.Windows.RoutedEventArgs e)
        //{
        //    ((DataGrid)sender).Columns.AsParallel().ForEach(column =>
        //   {
        //       column.MinWidth = column.ActualWidth;
        //       column.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
        //   });
        //}

        private void OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
           ((PackageViewModel)DataContext).AddContentCommand.Execute(e);
        }
    }
}
