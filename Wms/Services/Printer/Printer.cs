using System.Linq;
using System.Drawing.Printing;
using static Wms.Helpers.RegistryWin;

namespace Wms.Services.Printer
{
    public class Printer : IPrinter
    {
        private readonly string _printer = GetValue("Printer");
        private readonly string _printerDocx = GetValue("PrinterDocx");

        public bool HasPrinterPhysical()
        {
            var printers = PrinterSettings.InstalledPrinters.OfType<string>();
            return printers.Any(a=>a == _printer || a == _printerDocx);
        }

        public bool HasPrinterRegistry()
        {
            return string.IsNullOrEmpty(_printer) && string.IsNullOrEmpty(_printerDocx);
        }
    }
}
