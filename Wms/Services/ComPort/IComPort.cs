using System.IO.Ports;

namespace Wms.Services.ComPort
{
    public interface IComPort
    {
        void Open();
        void Close();
        event SerialDataReceivedEventHandler DataReceived;
    }
}
