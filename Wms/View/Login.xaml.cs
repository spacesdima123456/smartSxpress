using DevExpress.Xpf.Core;
using Wms.ViewModel;

namespace Wms
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
