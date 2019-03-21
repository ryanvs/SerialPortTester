using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace SerialPortTester
{
    public static class NativeMethods
    {
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        public enum ShowWindowCommands
        {
            /// <summary>
            /// Hides the window and activates another window.
            /// </summary>
            Hide = 0,
            /// <summary>
            /// Activates and displays a window. If the window is minimized or 
            /// maximized, the system restores it to its original size and position.
            /// An application should specify this flag when displaying the window 
            /// for the first time.
            /// </summary>
            Normal = 1,
            /// <summary>
            /// Activates the window and displays it as a minimized window.
            /// </summary>
            ShowMinimized = 2,
            /// <summary>
            /// Maximizes the specified window.
            /// </summary>
            Maximize = 3, // is this the right value?
            /// <summary>
            /// Activates the window and displays it as a maximized window.
            /// </summary>       
            ShowMaximized = 3,
            /// <summary>
            /// Displays a window in its most recent size and position. This value 
            /// is similar to <see cref="Win32.ShowWindowCommand.Normal"/>, except 
            /// the window is not activated.
            /// </summary>
            ShowNoActivate = 4,
            /// <summary>
            /// Activates the window and displays it in its current size and position. 
            /// </summary>
            Show = 5,
            /// <summary>
            /// Minimizes the specified window and activates the next top-level 
            /// window in the Z order.
            /// </summary>
            Minimize = 6,
            /// <summary>
            /// Displays the window as a minimized window. This value is similar to
            /// <see cref="Win32.ShowWindowCommand.ShowMinimized"/>, except the 
            /// window is not activated.
            /// </summary>
            ShowMinNoActive = 7,
            /// <summary>
            /// Displays the window in its current size and position. This value is 
            /// similar to <see cref="Win32.ShowWindowCommand.Show"/>, except the 
            /// window is not activated.
            /// </summary>
            ShowNA = 8,
            /// <summary>
            /// Activates and displays the window. If the window is minimized or 
            /// maximized, the system restores it to its original size and position. 
            /// An application should specify this flag when restoring a minimized window.
            /// </summary>
            Restore = 9,
            /// <summary>
            /// Sets the show state based on the SW_* value specified in the 
            /// STARTUPINFO structure passed to the CreateProcess function by the 
            /// program that started the application.
            /// </summary>
            ShowDefault = 10,
            /// <summary>
            ///  <b>Windows 2000/XP:</b> Minimizes a window, even if the thread 
            /// that owns the window is not responding. This flag should only be 
            /// used when minimizing windows from a different thread.
            /// </summary>
            ForceMinimize = 11
        }

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("shlwapi.dll")]
        private static extern bool PathIsNetworkPath(string pszPath);

        public enum SettableBaud
        {
            BAUD_075            = 0x00000001,   // 75 bps
            BAUD_110            = 0x00000002,   // 110 bps
            BAUD_134_5          = 0x00000004,   // 134.5 bps
            BAUD_150            = 0x00000008,   // 150 bps
            BAUD_300            = 0x00000010,   // 300 bps
            BAUD_600            = 0x00000020,   // 600 bps
            BAUD_1200           = 0x00000040,   // 1200 bps
            BAUD_1800           = 0x00000080,   // 1800 bps
            BAUD_2400           = 0x00000100,   // 2400 bps
            BAUD_4800           = 0x00000200,   // 4800 bps
            BAUD_7200           = 0x00000400,   // 7200 bps
            BAUD_9600           = 0x00000800,   // 9600 bps
            BAUD_14400          = 0x00001000,   // 14400 bps
            BAUD_19200          = 0x00002000,   // 19200 bps
            BAUD_38400          = 0x00004000,   // 38400 bps
            BAUD_56K            = 0x00008000,   // 56K bps
            BAUD_57600          = 0x00040000,   // 57600 bps
            BAUD_115200         = 0x00020000,   // 115200 bps
            BAUD_128K           = 0x00010000,   // 128K bps
            BAUD_USER           = 0x10000000,   // Programmable baud rate.
        }

        public enum ProvSubType
        {
            PST_FAX             = 0x00000021,   // FAX device
            PST_LAT             = 0x00000101,   // LAT protocol
            PST_MODEM           = 0x00000006,   // Modem device
            PST_NETWORK_BRIDGE  = 0x00000100,   // Unspecified network bridge
            PST_PARALLELPORT    = 0x00000002,   // Parallel port
            PST_RS232           = 0x00000001,   // RS-232 serial port
            PST_RS422           = 0x00000003,   // RS-422 port
            PST_RS423           = 0x00000004,   // RS-423 port
            PST_RS449           = 0x00000005,   // RS-449 port
            PST_SCANNER         = 0x00000022,   // Scanner device
            PST_TCPIP_TELNET    = 0x00000102,   // TCP/IP Telnet protocol
            PST_UNSPECIFIED     = 0x00000000,   // Unspecified
            PST_X25             = 0x00000103,   // X.25 standards
        }

        public enum ProvCapabilities
        {
            PCF_16BITMODE       = 0x0200,       // Special 16-bit mode supported
            PCF_DTRDSR          = 0x0001,       // DTR (data-terminal-ready)/DSR(data-set-ready) supported
            PCF_INTTIMEOUTS     = 0x0080,       // Interval time-outs supported
            PCF_PARITY_CHECK    = 0x0008,       // Parity checking supported
            PCF_RLSD            = 0x0004,       // RLSD(receive-line-signal-detect) supported
            PCF_RTSCTS          = 0x0002,       // RTS(request-to-send)/CTS(clear-to-send) supported
            PCF_SETXCHAR        = 0x0020,       // Settable XON/XOFF supported
            PCF_SPECIALCHARS    = 0x0100,       // Special character support provided
            PCF_TOTALTIMEOUTS   = 0x0040,       // The total(elapsed) time-outs supported
            PCF_XONXOFF         = 0x0010,       // XON/XOFF flow control supported
        }

        [Flags]
        public enum SettableParams
        {
            SP_PARITY           = 0x0001,       // Parity
            SP_BAUD             = 0x0002,       // Baud rate
            SP_DATABITS         = 0x0004,       // Data bits
            SP_STOPBITS         = 0x0008,       // Stop bits
            SP_HANDSHAKING      = 0x0010,       // Handshaking (flow control)
            SP_PARITY_CHECK     = 0x0020,       // Parity checking
            SP_RLSD             = 0x0040,       // RLSD (receive-line-signal-detect)
        }

        [Flags]
        public enum SettableDataBits
        {
            DATABITS_5          = 0x0001,
            DATABITS_6          = 0x0002,
            DATABITS_7          = 0x0004,
            DATABITS_8          = 0x0008,
            DATABITS_16         = 0x0010,
            DATABITS_16X        = 0x0020,
        }

        [Flags]
        public enum SettableStopParity
        {
            STOPBITS_10         = 0x0001,
            STOPBITS_15         = 0x0002,
            STOPBITS_20         = 0x0004,
            PARITY_NONE         = 0x0100,
            PARITY_ODD          = 0x0200,
            PARITY_EVEN         = 0x0400,
            PARITY_MARK         = 0x0800,
            PARITY_SPACE        = 0x1000,
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct COMMPROP
        {
            short wPacketLength;
            short wPacketVersion;
            int dwServiceMask;
            int dwReserved1;
            int dwMaxTxQueue;
            int dwMaxRxQueue;
            int dwMaxBaud;
            int dwProvSubType;
            int dwProvCapabilities;
            int dwSettableParams;
            int dwSettableBaud;
            short wSettableData;
            short wSettableStopParity;
            int dwCurrentTxQueue;
            int dwCurrentRxQueue;
            int dwProvSpec1;
            int dwProvSpec2;
            string wcProvChar;
        }

        private static Lazy<Dictionary<SettableBaud, int>> _baudMap =
            new Lazy<Dictionary<SettableBaud, int>>(GetBaudMap);

        private static Dictionary<SettableBaud, int> GetBaudMap()
        {
            return new Dictionary<SettableBaud, int>()
            {
                { SettableBaud.BAUD_075,    75 },
                { SettableBaud.BAUD_110,    110 },
                { SettableBaud.BAUD_134_5,  134 },
                { SettableBaud.BAUD_150,    150 },
                { SettableBaud.BAUD_300,    300 },
                { SettableBaud.BAUD_600,    600 },
                { SettableBaud.BAUD_1200,   1200 },
                { SettableBaud.BAUD_1800,   1800 },
                { SettableBaud.BAUD_2400,   2400 },
                { SettableBaud.BAUD_4800,   4800 },
                { SettableBaud.BAUD_7200,   7200 },
                { SettableBaud.BAUD_9600,   9600 },
                { SettableBaud.BAUD_14400,  14400 },
                { SettableBaud.BAUD_19200,  19200 },
                { SettableBaud.BAUD_38400,  38400 },
                { SettableBaud.BAUD_56K,    56000 },
                { SettableBaud.BAUD_57600,  57600 },
                { SettableBaud.BAUD_115200, 115200 },
                { SettableBaud.BAUD_128K,   128000 },
                { SettableBaud.BAUD_USER,   0 },
            };
        }
        public static int[] GetSettableBaudRates(int value)
        {
            var rates = new List<int>();
            foreach (SettableBaud item in Enum.GetValues(typeof(SettableBaud)))
            {
                if (item == SettableBaud.BAUD_USER)
                    continue;

                int mask = (int)item;
                if ((value & mask) != 0)
                {
                    int rate;
                    if (_baudMap.Value.TryGetValue(item, out rate))
                    {
                        rates.Add(rate);
                    }
                }
            }
            rates.Sort();
            return rates.ToArray();
        }
    }
}