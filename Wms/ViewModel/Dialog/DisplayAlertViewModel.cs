using System.Windows.Input;
using static Wms.Helpers.Translator;

namespace Wms.ViewModel.Dialog
{
    public class DisplayAlertViewModel : BaseViewModel
    {
        private ICommand _okCommand;
        public ICommand OkCommand
        {
            get => _okCommand;
            private set => Set(nameof(OkCommand), ref _okCommand, value);
        }

        private ICommand _cancelCommand;

        public ICommand CancelCommand
        {
            get => _cancelCommand;
            private set => Set(nameof(CancelCommand), ref _cancelCommand, value);
        }

        private string _message;

        public string Message
        {
            get => _message;
            private set => Set(nameof(Message), ref _message, value);
        }

        private string _okText;

        public string OkText
        {
            get => _okText;
            private set => Set(nameof(OkText), ref _okText, value);
        }

        private string _cancelText;

        public string CancelText
        {
            get => _cancelText;
            private set => Set(nameof(CancelText), ref _cancelText, value);
        }

        private string _tittle;

        public string Tittle
        {
            get => _tittle;
            private set => Set(nameof(Tittle), ref _tittle, value);
        }

        public DisplayAlertViewModel(string tittle, string okText, string cancelText, string message)
        {
            Tittle = Translate(tittle);
            OkText = Translate(okText);
            Message = Translate(message);
            CancelText = Translate(cancelText);
        }

        public DisplayAlertViewModel(string tittle, string okText, string cancelText, string message, ICommand okCommand) : this(tittle, okText, cancelText, message)
        {
            OkCommand = okCommand;
        }

        public DisplayAlertViewModel(string tittle, string okText, string cancelText, string message,
            ICommand okCommand, ICommand cancelCommand) : this(tittle, okText, cancelText, message, okCommand)
        {
            CancelCommand = cancelCommand;
        }
    }
}
