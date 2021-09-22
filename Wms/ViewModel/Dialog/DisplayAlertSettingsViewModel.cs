using  System.Linq;
using System.Drawing.Printing;
using System.Collections.ObjectModel;
using System.Windows.Input;
using DevExpress.Mvvm;
using  static  Wms.Helpers.RegistryWin;

namespace Wms.ViewModel.Dialog
{
    public class DisplayAlertSettingsViewModel : BaseViewModel
    {

        private string _printer;
        public  string Printer
        {
            get => _printer;
            set
            {
                Set(nameof(Printer), ref _printer, value);
                if (!string.IsNullOrEmpty(value))
                    SetValue("Printer", value);
            }
        }


        private string _printerDocx;
        public string PrinterDocx
        {
            get => _printerDocx;
            set
            {
                Set(nameof(PrinterDocx), ref _printerDocx, value);
                if (!string.IsNullOrEmpty(value))
                    SetValue("PrinterDocx", value);
            }
        }

        public ObservableCollection<string>  Printers { get; }

        //public  ICommand SaveSettingsCommand => new DelegateCommand(() =>
        //{
        //    if (!string.IsNullOrEmpty(Printer) && !string.IsNullOrEmpty(PrinterDocx))
        //    {
        //        SetValue("Printer", Printer);
        //        SetValue("PrinterDocx", PrinterDocx);
        //    }
        //});

        public DisplayAlertSettingsViewModel()
        {
            Printers = new ObservableCollection<string>(PrinterSettings.InstalledPrinters.OfType<string>());
            PrinterDocx = GetValue("PrinterDocx");
            Printer = GetValue("Printer");
        }
    }
}
