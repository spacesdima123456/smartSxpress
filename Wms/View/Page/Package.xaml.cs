using System;
using Wms.ViewModel.Page;
using System.Windows.Input;
using DevExpress.Xpf.Editors;
using System.Windows.Controls;

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

            await vm.OnQuerySubmittedSenderAsync(e.Text);

            if (vm.DocNumSenders != null && vm.DocNumSenders.Count > 0)
                autoSuggestEdit.ShowPopup();
            else
                autoSuggestEdit.ClosePopup();
        }

        private async void OnQuerySubmittedRecipients(object sender, AutoSuggestEditQuerySubmittedEventArgs e)
        {
            var vm = (PackageViewModel)DataContext;
            var autoSuggestEdit = (AutoSuggestEdit)sender;

            await vm.OnQuerySubmittedRecipientAsync(e.Text);

            if (vm.DocNumRecipients != null && vm.DocNumRecipients.Count > 0)
                autoSuggestEdit.ShowPopup();
            else
                autoSuggestEdit.ClosePopup();
        }
    }
}
