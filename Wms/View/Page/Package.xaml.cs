using Wms.ViewModel.Page;
using System.Windows.Input;
using DevExpress.Xpf.Editors;

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

        private async void OnQuerySubmittedSender(object sender, AutoSuggestEditQuerySubmittedEventArgs e)
        {
            var vm = (PackageViewModel) DataContext;
            var autoSuggestEdit = (AutoSuggestEdit) sender;

            await vm.SearchVariantDocSendersAsync(e.Text);

            if (vm.DocNumSenders != null && vm.DocNumSenders.Count > 0)
                autoSuggestEdit.ShowPopup();
            else
                autoSuggestEdit.ClosePopup();
        }
    }
}
