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

        private void OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
           ((PackageViewModel)DataContext).AddContentCommand.Execute(e);
        }
    }
}
