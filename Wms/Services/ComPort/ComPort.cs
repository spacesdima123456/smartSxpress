using System;
using System.IO.Ports;
using System.Linq;
using static Wms.Helpers.RegistryWin;

namespace Wms.Services.ComPort
{
    public class ComPort: IComPort
    {
        private readonly SerialPort _serialPort;

        public event SerialDataReceivedEventHandler DataReceived;

        public ComPort()
        {
            var port = GetValue("ComPort");
            var speed = GetValue("SpeedComPort");
            var comPorts = SerialPort.GetPortNames();

            if (string.IsNullOrEmpty(port) || string.IsNullOrEmpty(speed) || !comPorts.Any(a=>a.Contains(port)))
                return;

            _serialPort = new SerialPort(port, Convert.ToInt32(speed), Parity.None, 8, StopBits.One);
            _serialPort.DataReceived += Received;
        }

        private void Received(object sender, SerialDataReceivedEventArgs e)
        {
            DataReceived?.Invoke(sender, e);
        }

        public void Open()
        {
            if (_serialPort!=null && !_serialPort.IsOpen)
                _serialPort.Open();
        }

        public void Close()
        {
            if (_serialPort != null && _serialPort.IsOpen)
                _serialPort.Close();
        }
    }
}
