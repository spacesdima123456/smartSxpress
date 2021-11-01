using System;
using System.IO.Ports;
using static Wms.Helpers.RegistryWin;

namespace Wms.Services.ComPort
{
    public class ComPort : IComPort
    {

        private readonly SerialPort _serialPort;

        public event SerialDataReceivedEventHandler DataReceived;

        public ComPort()
        {
            var port = GetValue("ComPort");
            var speed = Convert.ToInt32(GetValue("SpeedComPort"));
            _serialPort = new SerialPort(port, speed, Parity.None, 8, StopBits.One) { Handshake = Handshake.None };
            _serialPort.DataReceived += Received;
            _serialPort.Open();
        }

        private void Received(object sender, SerialDataReceivedEventArgs e)
        {
            DataReceived?.Invoke(sender, e);
        }

        public void Close()
        {
            if (_serialPort.IsOpen)
                _serialPort.Close();
        }

        public void Open()
        {
            if (!_serialPort.IsOpen)
                _serialPort.Open();
        }
    }
}
