using DevExpress.Xpf.Core;
using Wms.Services.Window;
using Wms.ViewModel;

namespace Wms.View
{
    public partial class Login : ThemedWindow
    {
        public Login()
        {
            InitializeComponent();
            DataContext = new LoginViewModel(new WindowFactory(this, new Admin()));
        }
    }
}
