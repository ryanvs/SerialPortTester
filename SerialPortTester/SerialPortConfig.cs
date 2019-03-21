using System.IO.Ports;
using System.Runtime.Serialization;

namespace SerialPortTester
{
    public class SerialPortConfig
    {
        [DataMember]
        public string PortName { get; set; }

        [DataMember]
        public int BaudRate { get; set; }

        [DataMember]
        public int DataBits { get; set; }

        [DataMember]
        public Parity Parity { get; set; }

        [DataMember]
        public StopBits StopBits { get; set; }

        [DataMember]
        public Handshake Handshake { get; set; }

        [DataMember]
        public bool DtrEnable { get; set; }

        [DataMember]
        public bool RtsEnable { get; set; }

        [DataMember]
        public int? ReadTimeout { get; set; }

        [DataMember]
        public int? WriteTimeout { get; set; }

        public static SerialPortConfig Create(SerialPort port)
        {
            var config = new SerialPortConfig();
            config.GetPortConfiguration(port);
            return config;
        }

        public static SerialPortConfig Default(string portName = null)
        {
            return new SerialPortConfig()
            {
                PortName = portName ?? "COM1",
                BaudRate = 115200,
                DataBits = 8,
                Parity = Parity.None,
                StopBits = StopBits.One,
                Handshake = Handshake.None,
            };
        }

        public void GetPortConfiguration(SerialPort port)
        {
            PortName     = port.PortName;
            BaudRate     = port.BaudRate;
            DataBits     = port.DataBits;
            Parity       = port.Parity;
            StopBits     = port.StopBits;
            Handshake    = port.Handshake;
            DtrEnable    = port.DtrEnable;
            RtsEnable    = port.RtsEnable;
            ReadTimeout  = port.ReadTimeout;
            WriteTimeout = port.WriteTimeout;
        }

        public void SetPortConfiguration(SerialPort port)
        {
            port.PortName     = PortName;
            port.BaudRate     = BaudRate;
            port.DataBits     = DataBits;
            port.Parity       = Parity;
            port.StopBits     = StopBits;
            port.Handshake    = Handshake;
            port.DtrEnable    = DtrEnable;
            port.RtsEnable    = RtsEnable;
            if (ReadTimeout >= 0)
                port.ReadTimeout  = ReadTimeout.Value;
            if (WriteTimeout.HasValue)
                port.WriteTimeout = WriteTimeout.Value;
        }

        private string GetParityChar(Parity parity)
        {
            switch (parity)
            {
                case Parity.None:  return "N";
                case Parity.Odd:   return "O";
                case Parity.Even:  return "E";
                case Parity.Mark:  return "M";
                case Parity.Space: return "S";
                default:           return "?";
            }
        }

        private string GetStopBitsString(StopBits stopBits)
        {
            switch (stopBits)
            {
                case StopBits.None:         return "0";
                case StopBits.One:          return "1";
                case StopBits.Two:          return "2";
                case StopBits.OnePointFive: return "1.5";
                default:                    return "?";
            }
        }

        [IgnoreDataMember]
        public string PortConfig
        {
            get
            {
                return string.Format("{0},{1},{2},{3},{4},{5}",
                    PortName, BaudRate, DataBits,
                    GetParityChar(Parity), GetStopBitsString(StopBits),
                    Handshake);
            }
        }
    }
}
