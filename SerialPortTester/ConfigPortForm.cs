using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO.Ports;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SerialPortTester
{
    public partial class ConfigPortForm : Form
    {
        private static int[] DefaultBaudRates()
        {
            return new int[] { 300, 600, 1200, 2400, 4800, 9600, 19200, 38400, 57600, 115200, 230400, 460800, 921600 };
        }

        private List<int> _baseBaudRates = new List<int>(DefaultBaudRates());

        private List<int> _configBaudRates;

        private Dictionary<string, IList<int>> _portBaudRates =
            new Dictionary<string, IList<int>>();

        private SerialPortConfig _delayConfigLoad;

        public bool IsLoaded { get; private set; }

        public ConfigPortForm()
        {
            InitializeComponent();
            Load += ConfigPortForm_Load;
        }

        private async void ConfigPortForm_Load(object sender, EventArgs e)
        {
            // Add the fixed options that cannot change
            AddFixedComboBoxItems();

            try
            {
                string[] portNames = await AddComPortComboBoxItemsAsync();
                PortNameComboBox.Items.Clear();
                PortNameComboBox.Items.AddRange(portNames);
            }
            catch (Exception ex)
            {
                //_log.Error(ex, "AddComPortComboBoxItemsAsync: GetPortNames failed - {0}", ex.Message);
                myErrorProvider.SetError(PortNameComboBox, string.Format("GetPortNames failed - {0}", ex.Message));
            }

            IsLoaded = true;

            if (_delayConfigLoad == null)
            {
                string portName = PortNameComboBox.Items.Count > 0 ? (string)PortNameComboBox.Items[0] : null;
                _delayConfigLoad = SerialPortConfig.Default(portName);
            }
            else
            {
                SetSerialPortConfig(_delayConfigLoad);
            }

            Validate();
            ValidateChildren();
        }

        private void ReadConfigBaudRates()
        {
            try
            {
                string value = ConfigurationManager.AppSettings["BaudRates"];
                if (!string.IsNullOrEmpty(value))
                {
                    var data = value.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                    .Select(x => Convert.ToInt32(x.Trim(), CultureInfo.InvariantCulture))
                                    .OrderBy(x => x);
                    _configBaudRates = new List<int>(data);

                    // Add values not already in default baud rates
                    var newRates = _configBaudRates.Except(_baseBaudRates);
                    if (newRates.Any())
                    {
                        _baseBaudRates.AddRange(newRates);
                        _baseBaudRates.Sort();
                    }
                }
            }
            catch (Exception ex)
            {
                //_log.Error(ex, "ReadConfigBaudRates: Failed - {0}", ex.Message);
            }
        }

        private Task<string[]> AddComPortComboBoxItemsAsync()
        {
            return Task.Run(() =>
            {
                ReadConfigBaudRates();

                try
                {
                    var comparer = new NaturalComparer();
                    return SerialPort.GetPortNames().OrderBy(x => x, comparer).ToArray();
                }
                catch (Exception ex)
                {
                    //_log.Error(ex, "AddComPortComboBoxItems: Failed - {0}", ex.Message);
                    throw;
                }
            });
        }

        private void AddFixedComboBoxItems()
        {
            DataBitsComboBox.Items.AddRange(new object[] { 7, 8 });
            ParityComboBox.Items.AddRange(new object[]
            {
                EnumComboItem(Parity.None),
                EnumComboItem(Parity.Even),
                EnumComboItem(Parity.Odd),
                EnumComboItem(Parity.Mark),
            });
            StopBitsComboBox.Items.AddRange(new object[]
            {
                //EnumComboItem("0", StopBits.None),
                EnumComboItem("1", StopBits.One),
                EnumComboItem("1.5", StopBits.OnePointFive),
                EnumComboItem("2", StopBits.Two)
            });
            HandshakeComboBox.Items.AddRange(new object[]
            {
                EnumComboItem("None", Handshake.None),
                EnumComboItem("Request To Send", Handshake.RequestToSend),
                EnumComboItem("Request To Send/XOn/XOff", Handshake.RequestToSendXOnXOff),
                EnumComboItem("XOn/XOff", Handshake.XOnXOff),
            });
        }

        /// <summary>
        /// Override to allow Visual Studio Designer to work without an exception:
        /// https://stackoverflow.com/a/39095558/29762
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBox_Validating_MustSelectIndex(object sender, CancelEventArgs e)
        {
            string message = string.Empty;
            ComboBox source = (ComboBox)sender;
            if (source.SelectedIndex < 0)
            {
                message = "Please choose from the available selections";
            }
            myErrorProvider.SetError(source, message);
        }

        private void PortNameComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string portName = string.Empty;

            try
            {
                var comboBox = sender as ComboBox;
                portName = (string)comboBox.SelectedItem;
                UpdatePortBaudRates(portName);
            }
            catch (Exception ex)
            {
                Trace.TraceError("PortNameComboBox_SelectedIndexChanged: Unable to open port '{0}' - {1}={2}",
                    portName, ex.GetType().Name, ex.Message);
            }

            RaiseValidating(sender as Control);
        }

        private void ComboBoxValidate_SelectedIndexChanged(object sender, EventArgs e)
        {
            RaiseValidating(sender as Control);
        }

        private void UpdatePortBaudRates(string portName)
        {
            Int32? settableBaud = null;
            Int32? maxBaud = null;
            bool found = false;
            IList<int> baudRates;
            var selectedBaudRate = (int?)BaudRateComboBox.SelectedItem;
            if (_portBaudRates.TryGetValue(portName, out baudRates))
            {
                found = true;
            }
            else
            {
                try
                {
                    Trace.TraceInformation("UpdatePortBaudRates: Opening '{0}'", portName);
                    using (var _port = new SerialPort(portName))
                    {
                        _port.Open();
                        try
                        {
                            object commProp = _port.BaseStream.GetType().GetField("commProp", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(_port.BaseStream);
                            //_log.Debug("UpdatePortBaudRates: CommProp = {0}", JsonConvert.SerializeObject(commProp, Formatting.None));
                            settableBaud = (Int32)commProp.GetType().GetField("dwSettableBaud", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public).GetValue(commProp);
                            maxBaud = (Int32)commProp.GetType().GetField("dwMaxBaud", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public).GetValue(commProp);
                            //_log.Debug("UpdatePortBaudRates: dwSettableBaud = {0:X4}, dwMaxBaud = {1:X4}", settableBaud, maxBaud);
                        }
                        finally
                        {
                            _port.Close();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Trace.TraceError("UpdatePortBaudRates: Unable to open port '{0}' - {1}={2}",
                        portName, ex.GetType().Name, ex.Message);
                }
            }

            if (baudRates == null)
            {
                if (settableBaud == null)
                {
                    // Use the default baud rates
                    baudRates = new List<int>(_baseBaudRates);
                }
                else
                {
                    // Use only the baud rates up to the settableBaud, then cache the result
                    var rates = NativeMethods.GetSettableBaudRates(settableBaud.Value);
                    if (rates.Length == 0)
                        baudRates = new List<int>(_baseBaudRates.Where(x => x <= settableBaud));
                    else
                        baudRates = new List<int>(rates);
                    if (!found)
                    {
                        _portBaudRates.Add(portName, baudRates);
                    }
                }
            }

            BaudRateComboBox.Items.Clear();
            if (baudRates != null)
            {
                var items = baudRates.Cast<object>().ToArray();
                BaudRateComboBox.Items.AddRange(items);
                if (selectedBaudRate != null)
                {
                    int newIndex = BaudRateComboBox.Items.IndexOf(selectedBaudRate);
                    if (newIndex >= 0)
                        BaudRateComboBox.SelectedIndex = newIndex;
                }
            }
        }

        //protected override void InternalUpdateConfiguration()
        //{
        //    var config = Configuration;
        //    if (config == null) { throw new InvalidOperationException("Configuration is null"); }
        //    if (!IsLoaded) { throw new InvalidOperationException("UserControl is not loaded yet"); }
        //    GetSelectedValue(PortNameComboBox, x => { config.PortName = x; });
        //    GetSelectedValue(BaudRateComboBox, x => { config.BaudRate = x; });
        //    GetSelectedValue(DataBitsComboBox, x => { config.DataBits = x; });
        //    GetSelectedValue(ParityComboBox, (Parity x) => { config.Parity = x; });
        //    GetSelectedValue(StopBitsComboBox, (StopBits x) => { config.StopBits = x; });
        //    GetSelectedValue(HandshakeComboBox, (Handshake x) => { config.Handshake = x; });
        //}

        //protected override void InternalUpdateControls()
        //{
        //    var config = Configuration;
        //    if (config == null) { throw new InvalidOperationException("Configuration is null"); }
        //    if (!IsLoaded) { throw new InvalidOperationException("UserControl is not loaded yet"); }
        //    SetSelectedIndex(PortNameComboBox, config.PortName);
        //    SetSelectedIndex(BaudRateComboBox, config.BaudRate);
        //    SetSelectedIndex(DataBitsComboBox, config.DataBits);
        //    SetSelectedIndexEnum(ParityComboBox, config.Parity);
        //    SetSelectedIndexEnum(StopBitsComboBox, config.StopBits);
        //    SetSelectedIndexEnum(HandshakeComboBox, config.Handshake);
        //}

        private void DataBitsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox_Validating_MustSelectIndex(DataBitsComboBox, null);
        }

        private void StopBitsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox_Validating_MustSelectIndex(StopBitsComboBox, null);
        }

        public string FinalValidationCheck()
        {
            // Load the control if it is not already loaded to ensure that all required fields
            // have correct values
            if (IsLoaded == false)
            {
                ConfigPortForm_Load(this, null);
            }
            if (string.IsNullOrEmpty(PortNameComboBox.Text))
            {
                return "Please select a value for Port Name";
            }
            if (string.IsNullOrEmpty(BaudRateComboBox.Text))
            {
                return "Please select a value for Baud Rate";
            }
            if (string.IsNullOrEmpty(DataBitsComboBox.Text))
            {
                return "Please select a value for Data Bits";
            }
            if (string.IsNullOrEmpty(ParityComboBox.Text))
            {
                return "Please select a value for Parity";
            }
            if (string.IsNullOrEmpty(StopBitsComboBox.Text))
            {
                return "Please select a value for Stop Bits";
            }
            if (string.IsNullOrEmpty(HandshakeComboBox.Text))
            {
                return "Please select a value for Handshake";
            }
            else
                return string.Empty;
        }

        #region Validation support
        private void RaiseValidating(Control sender)
        {
            if (sender != null)
            {
                if (typeof(ComboBox).IsAssignableFrom(sender.GetType()))
                    ComboBox_Validating_MustSelectIndex(sender, new CancelEventArgs());
                //else
                //    sender.Validating?.Invoke(sender, new CancelEventArgs());
            }
        }
        #endregion

        #region ComboBox utility functions
        protected KeyValuePair<string, Enum> EnumComboItem(Enum value)
        {
            return new KeyValuePair<string, Enum>(Convert.ToString(value), value);
        }

        protected KeyValuePair<string, Enum> EnumComboItem(string key, Enum value)
        {
            return new KeyValuePair<string, Enum>(key, value);
        }

        protected int FindIndexOf<T>(ComboBox source, T value, Func<object, T, bool> predicate)
        {
            int index = 0;
            foreach (var item in source.Items)
            {
                if (predicate(item, value))
                {
                    return index;
                }
                ++index;
            }
            // Not found
            return -1;
        }

        protected void GetSelectedValue(ComboBox source, Action<int> setProp)
        {
            GetSelectedValue(source, x => (int)x, setProp);
        }

        protected void GetSelectedValue(ComboBox source, Action<string> setProp)
        {
            GetSelectedValue(source, x => (string)x, setProp);
        }

        protected T GetKvpValue<T>(object item)
        {
            var kvp = (KeyValuePair<string, Enum>)item;
            return (T)(object)kvp.Value;
        }

        protected void GetSelectedValue<T>(ComboBox source, Action<T> setProp)
        {
            GetSelectedValue(source, x => { return GetKvpValue<T>(x); }, setProp);
        }

        protected void GetSelectedValue<T>(ComboBox source, Func<object, T> getValue, Action<T> setProp)
        {
            int index = source.SelectedIndex;
            if (index >= 0)
            {
                object item = source.SelectedItem;
                T value = getValue(item);
                setProp(value);
            }
            else
            {
                setProp(default(T));
            }
        }

        protected void SetSelectedIndex<T>(ComboBox source, T value, bool clearIfNotFound = false)
        {
            int index = (value == null) ? -1 : source.Items.IndexOf(value);
            if (index >= 0)
                source.SelectedIndex = index;
            else if (clearIfNotFound && source.SelectedIndex >= 0)
                source.SelectedIndex = -1;
        }

        protected void SetSelectedIndex<T>(ComboBox source, T value, Func<object, T, bool> predicate, bool clearIfNotFound = false)
        {
            int index = FindIndexOf(source, value, predicate);
            if (index >= 0)
                source.SelectedIndex = index;
            else if (clearIfNotFound && source.SelectedIndex >= 0)
                source.SelectedIndex = -1;
        }


        protected bool KeyValuePairCompare(object item, Enum value)
        {
            var kvp = (KeyValuePair<string, Enum>)item;
            bool result = ReferenceEquals(kvp.Value, value);
            result = EqualityComparer<Enum>.Default.Equals(kvp.Value, value);
            return result;
        }

        protected void SetSelectedIndexEnum(ComboBox source, Enum value, bool clearIfNotFound = false)
        {
            int index = FindIndexOf(source, value, KeyValuePairCompare);
            if (index >= 0)
                source.SelectedIndex = index;
            else if (clearIfNotFound && source.SelectedIndex >= 0)
                source.SelectedIndex = -1;
        }
        #endregion

        public SerialPortConfig GetSerialPortConfig()
        {
            var config = new SerialPortConfig();
            if (config == null) { throw new InvalidOperationException("Configuration is null"); }
            if (!IsLoaded) { throw new InvalidOperationException("Form is not loaded yet"); }
            GetSelectedValue(PortNameComboBox, x => { config.PortName = x; });
            GetSelectedValue(BaudRateComboBox, x => { config.BaudRate = x; });
            GetSelectedValue(DataBitsComboBox, x => { config.DataBits = x; });
            GetSelectedValue(ParityComboBox, (Parity x) => { config.Parity = x; });
            GetSelectedValue(StopBitsComboBox, (StopBits x) => { config.StopBits = x; });
            GetSelectedValue(HandshakeComboBox, (Handshake x) => { config.Handshake = x; });
            return config;
        }

        public void SetSerialPortConfig(SerialPortConfig config)
        {
            if (config == null) { throw new ArgumentNullException("Configuration is null"); }
            //if (!IsLoaded) { throw new InvalidOperationException("UserControl is not loaded yet"); }
            _delayConfigLoad = config;
            if (IsLoaded)
            {
                SetSelectedIndex(PortNameComboBox, config.PortName);
                SetSelectedIndex(BaudRateComboBox, config.BaudRate);
                SetSelectedIndex(DataBitsComboBox, config.DataBits);
                SetSelectedIndexEnum(ParityComboBox, config.Parity);
                SetSelectedIndexEnum(StopBitsComboBox, config.StopBits);
                SetSelectedIndexEnum(HandshakeComboBox, config.Handshake);
            }
        }

        private void MyOkButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}
