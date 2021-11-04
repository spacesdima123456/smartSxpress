namespace Wms.Services.Printer
{
    public interface IPrinter
    {
        bool HasPrinterRegistry();
        bool HasPrinterPhysical();
    }
}
