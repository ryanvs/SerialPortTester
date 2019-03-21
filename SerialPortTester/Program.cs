using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SerialPortTester
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Set the unhandled exception mode to force all Windows Forms errors to go through our handler. 
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

            // Add the event handler for handling non-UI thread exceptions to the event.  
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            // Add the event handler for handling UI thread exceptions to the event. 
            Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }

        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            var ex = e.Exception;
            Trace.TraceError(string.Format("ThreadException: {0}={1}", ex.GetType().Name, ex.Message));
            ErrorForm.ShowExceptionForm(e.Exception, Application.ProductName + " Thread Exception");
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var ex = e.ExceptionObject as Exception ?? new Exception("Unknown AppDomain Exception");
            Trace.TraceError(string.Format("UnhandledException in AppDomain: {0}={1}", ex.GetType().Name, ex.Message));
            ErrorForm.ShowExceptionForm(ex, Application.ProductName + " Unhandled Exception");
        }
    }
}
