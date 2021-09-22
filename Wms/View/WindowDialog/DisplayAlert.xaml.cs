using System;
using System.Windows;
using System.Windows.Input;
using Wms.ViewModel.Dialog;

namespace Wms.View.WindowDialog
{
    public sealed partial class DisplayAlert : DisplayAlertBase
    {
        private readonly Action<object> _okAction;
        private readonly Action<object> _cancelAction;

        public DisplayAlert()
        {
            InitializeComponent();
            //SetWindowStartupLocation();
        }

        public DisplayAlert(string tittle, string okText, string cancelText, string message):this()
        {
            DataContext = new DisplayAlertViewModel(tittle, okText, cancelText, message);
        }

        public DisplayAlert(string tittle, string okText, string cancelText, string message, ICommand okCommand) : this(
            tittle, okText, cancelText, message)
        {
            DataContext = new DisplayAlertViewModel(tittle, okText, cancelText, message, okCommand, okCommand);
        }

        public DisplayAlert(string tittle, string okText, string cancelText, string message, ICommand okCommand,
            ICommand cancelCommand) : this(tittle, okText, cancelText, message, okCommand)
        {
            DataContext = new DisplayAlertViewModel(tittle, okText, cancelText, message, okCommand, cancelCommand);
        }

        public DisplayAlert(string tittle, string okText, string cancelText, string message,
            Action<object> okAction) : this(tittle, okText, cancelText, message)
        {
            _okAction = okAction;
        }

        public DisplayAlert(string tittle, string okText, string cancelText, string message,
            Action<object> okAction, Action<object> cancelAction) : this(tittle,
            okText, cancelText, message, okAction)
        {
            _cancelAction = cancelAction;
        }

        private void ClickOk(object sender, RoutedEventArgs e)
        {
            _okAction?.Invoke(sender);
            Close();
        }

        private void ClickCancel(object sender, RoutedEventArgs e)
        {
            _cancelAction?.Invoke(sender);
            Close();
        }
    }
}
