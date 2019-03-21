using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SerialPortTester
{
    public partial class MainForm : Form
    {
        private SerialPort _port;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            _port = new SerialPort();
            _port.DataReceived += _port_DataReceived;
            _port.ErrorReceived += _port_ErrorReceived;
            _port.PinChanged += _port_PinChanged;

            var names = SerialPort.GetPortNames();
            Trace.TraceInformation("PortNames: {0}", string.Join(", ", names));
            _port.PortName = "COM1";
            _port.BaudRate = 9600;
            _port.DataBits = 8;
            _port.Handshake = Handshake.None;
            _port.Parity = Parity.None;
            _port.StopBits = StopBits.One;
            _port.DtrEnable = false;
            _port.RtsEnable = false;
            _port.ReadTimeout = 1000;
            _port.WriteTimeout = 250;

            UpdateButtons();
        }

        private void _port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            Trace.TraceInformation("DataReceived: {0}", e.EventType);
            string data = ((SerialPort)sender).ReadExisting();
            ReceivedTextBox.Text += data;
        }

        private void _port_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            Trace.TraceInformation("ErrorReceived: {0}", e.EventType);
        }

        private void _port_PinChanged(object sender, SerialPinChangedEventArgs e)
        {
            Trace.TraceInformation("PinChanged: {0}", e.EventType);
        }

        private void UpdateButtons()
        {
            bool isOpen = _port.IsOpen;
            ConfigButton.Enabled = !isOpen;
            OpenButton.Enabled = !isOpen;
            CloseButton.Enabled = isOpen;
            SendButton.Enabled = isOpen;
        }

        private void OpenButton_Click(object sender, EventArgs e)
        {
            if (!_port.IsOpen)
                _port.Open();
            UpdateButtons();
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            if (_port.IsOpen)
                _port.Close();
            UpdateButtons();
        }

        private void SendButton_Click(object sender, EventArgs e)
        {
            _port.Write(SendTextBox.Text);
            UpdateButtons();
        }
    }
}
