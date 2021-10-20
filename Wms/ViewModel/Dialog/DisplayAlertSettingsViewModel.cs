using System.Linq;
using DevExpress.Mvvm;
using System.IO.Ports;
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
            set => Set(nameof(Printer), ref _printer, value);
        }

        private string _printerDocx;
        public string PrinterDocx
        {
            get => _printerDocx;
            set=> Set(nameof(PrinterDocx), ref _printerDocx, value);
        }

        private string _comPort;
        public string ComPort
        {
            get => _comPort;
            set => Set(nameof(ComPort),ref _comPort, value);
        }

        private string _typeScale;
        public string TypeScale
        {
            get => _typeScale;
            set => Set(nameof(TypeScale), ref _typeScale, value);
        }

        public ObservableCollection<string> Printers { get; }
        public ObservableCollection<string> ComPorts { get; }
        public ObservableCollection<string> TypeOfScales { get; }

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
            TypeOfScales = new ObservableCollection<string>(App.Data.Data.ScaleTypes.Select(s=>s.Type));
            ComPorts = new ObservableCollection<string>(SerialPort.GetPortNames());
            PrinterDocx = GetValue(nameof(PrinterDocx));
            TypeScale = GetValue(nameof(TypeScale));
            Printer = GetValue(nameof(Printer));
            ComPort = GetValue(nameof(ComPort));
        }

        private void Set(string property, ref string field, string value)
        {
            base.Set(property, ref field, value);
            if (!string.IsNullOrEmpty(value))
                SetValue(property, value);
        }
    }
}
