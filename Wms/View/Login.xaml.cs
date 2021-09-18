using DevExpress.Xpf.Core;
using DevExpress.Xpf.Editors;
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

        private void OnValueChanged(object sender, EditValueChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(TxeLogin.Text) && !string.IsNullOrEmpty(PbePassword.Text))
                BtnLogIn.IsEnabled = true;
            else
                BtnLogIn.IsEnabled = false;
        }
    }
}
