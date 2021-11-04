using System.Linq;
using System.Drawing.Printing;
using static Wms.Helpers.RegistryWin;

namespace Wms.Services.Printer
{
    public class Printer : IPrinter
    {

        public bool HasPrinterPhysical()
        {
            var printers = PrinterSettings.InstalledPrinters.OfType<string>();
            return printers.Any(a=>a == GetValue("Printer") || a == GetValue("PrinterDocx"));
        }

        public bool HasPrinterRegistry()
        {
            return string.IsNullOrEmpty(GetValue("Printer")) && string.IsNullOrEmpty(GetValue("PrinterDocx"));
        }
    }
}
