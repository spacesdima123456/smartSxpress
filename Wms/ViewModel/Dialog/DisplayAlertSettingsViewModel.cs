using System.Linq;
using DevExpress.Mvvm;
using System.Diagnostics;
using System.Windows.Input;
using System.Drawing.Printing;
using System.Collections.ObjectModel;
using static  Wms.Helpers.RegistryWin;

namespace Wms.ViewModel.Dialog
{
    public class DisplayAlertSettingsViewModel : BaseViewModel
    {
        private string _printer;
        public string Printer
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

        public ObservableCollection<string> Printers { get; }

        public static ICommand PrinterDialogCommand => new DelegateCommand<string>((printer) =>
        {
            using var process = new Process();
            var startInfo = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                CreateNoWindow = true,
                WindowStyle = ProcessWindowStyle.Hidden,
                Arguments = "/C rundll32 printui.dll,PrintUIEntry /p /n \"" + printer + "\""
            };

            process.StartInfo = startInfo;
            process.Start();
        });

        public DisplayAlertSettingsViewModel()
        {
            Printers = new ObservableCollection<string>(PrinterSettings.InstalledPrinters.OfType<string>());
            PrinterDocx = GetValue("PrinterDocx");
            Printer = GetValue("Printer");
        }
    }
}
