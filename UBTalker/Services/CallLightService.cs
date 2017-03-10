using System;
using System.IO.Ports;
using System.Threading;

namespace UBTalker.Services
{
    class CallLightService : ICallLightService
    {
        private static readonly int UK1104_DEV_BAUD_RATE = 115200;
        private static readonly Parity UK1104_DEV_PARITY = Parity.None;
        private static readonly int UK1104_DEV_DATA_BITS = 8;
        private static readonly StopBits UK1104_DEV_STOP_BITS = StopBits.One;

        private SerialPort _serialPort;

        public CallLightService()
        {
            Connect();
        }

        private void Connect()
        {
            foreach (var port in SerialPort.GetPortNames())
            {
                System.Console.WriteLine("Connecting to " + port + " ...");
                if (TryConnect(port))
                {
                    break;
                }
            }
        }

        private bool TryConnect(string port)
        {
            _serialPort = new SerialPort();
            _serialPort.PortName = port;
            _serialPort.BaudRate = UK1104_DEV_BAUD_RATE;
            _serialPort.Parity = UK1104_DEV_PARITY;
            _serialPort.DataBits = UK1104_DEV_DATA_BITS;
            _serialPort.StopBits = UK1104_DEV_STOP_BITS;

            try
            {
                // Open the port and request device information
                // Command "ABOUT" per UK1104 Documentation
                _serialPort.Open();
                _serialPort.Write("ABOUT\r\n");
                _serialPort.ReadTimeout = 100;

                // Gather response
                string response = "";
                for (int i = 0; i < 3; i++)
                    response += _serialPort.ReadLine();

                // Verify response
                if (response.Contains("UK1104"))
                    return true;
                else
                    _serialPort.Close();
            }
            catch
            {
                if (_serialPort.IsOpen)
                    _serialPort.Close();
            }

            return false;
        }

        public void ActivateCallLight()
        {
            try
            {
                // If not connected to device, attempt to connect
                if (_serialPort == null || !_serialPort.IsOpen)
                    Connect();

                // If still not connected, assume it failed
                if (_serialPort == null || !_serialPort.IsOpen)
                    return;

                // Simulate button press
                _serialPort.Write("RELS.ON\r\n");
                Thread.Sleep(500);
                _serialPort.Write("RELS.OFF\r\n");
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.StackTrace);
            }
        }
    }
}
