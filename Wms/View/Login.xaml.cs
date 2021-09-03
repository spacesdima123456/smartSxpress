using DevExpress.Xpf.Core;
using Wms.ViewModel;

namespace Wms.View
{
    public partial class Login : ThemedWindow
    {
        public Login()
        {
            InitializeComponent();
            DataContext = new LoginViewModel(this);
        }
    }
}
