﻿using System;
using System.Diagnostics;
using System.IO.Ports;
using System.Windows.Forms;

namespace SerialPortTester
{
    public partial class MainForm : Form
    {
        private SerialPort _port;
        private ConfigPortForm _configForm;

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
            AppendReceivedData(data);
        }

        private void _port_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            Trace.TraceInformation("ErrorReceived: {0}", e.EventType);
        }

        private void _port_PinChanged(object sender, SerialPinChangedEventArgs e)
        {
            Trace.TraceInformation("PinChanged: {0}", e.EventType);
        }

        private void AppendReceivedData(string data)
        {
            if (InvokeRequired)
            {
                Invoke((Action)(() => { AppendReceivedData(data); }));
            }
            else
            {
                ReceivedTextBox.Text += data;
            }
        }

        private void UpdateButtons()
        {
            bool isOpen = _port.IsOpen;
            ConfigButton.Enabled = !isOpen;
            OpenButton.Enabled = !isOpen;
            CloseButton.Enabled = isOpen;
            SendButton.Enabled = isOpen;
            SendTextBox.ReadOnly = !isOpen;
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

        private void ConfigButton_Click(object sender, EventArgs e)
        {
            if (_configForm == null || _configForm.IsDisposed)
            {
                _configForm = new ConfigPortForm();
                _configForm.Owner = this;
            }

            var config = new SerialPortConfig();
            config.GetPortConfiguration(_port);
            var handle = _configForm.Handle; // Force the form to load
            _configForm.SetSerialPortConfig(config);

            var result = _configForm.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                config = _configForm.GetSerialPortConfig();
                config.SetPortConfiguration(_port);
            }
        }
    }
}
