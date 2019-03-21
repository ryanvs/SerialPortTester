using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace SerialPortTester
{
    public partial class ErrorForm : Form
    {
        static string _defaultTitle;
        static string _product;

        private class NodeInfo
        {
            public int SelectionStart { get; set; }
            public int SelectionLength { get; set; }
            public string SelectionRtf { get; set; }
        }

        private Exception _exception;
        public Exception Exception
        {
            get { return _exception; }
            set
            {
                _exception = value;
                BuildTree(value, null);
            }
        }

        // Font sizes based on the "normal" size.
        Font _small;
        Font _med;
        Font _large;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool LockWindowUpdate(IntPtr hWndLock);

        public ErrorForm()
        {
            InitializeComponent();
        }

        public ErrorForm(string headerMessage, Exception e, IWin32Window owner = null)
            : this()
        {
            // We use three font sizes.  The smallest is based on whatever the "standard"
            // size is for the current system/app, taken from an arbitrary control.
            float baseSize = this.Font.Size;
            _small = new Font(this.Font, FontStyle.Regular);
            _med = new Font(this.Font.FontFamily, baseSize * 1.1f, FontStyle.Bold);
            _large = new Font(this.Font.FontFamily, baseSize * 1.2f, FontStyle.Bold);

            Text = DefaultTitle;
            BuildTree(e, headerMessage);
        }

        /// <summary>
        /// The default title to use for the ErrorDialog window.  Automatically initialized 
        /// to "Error - [ProductName]" where [ProductName] is taken from the application's
        /// AssemblyProduct attribute (set in the AssemblyInfo.cs file).  You can change this
        /// default, or ignore it and set Title yourself before calling ShowDialog().
        /// </summary>
        public static string DefaultTitle
        {
            get
            {
                if (_defaultTitle == null)
                {
                    if (string.IsNullOrEmpty(Product))
                    {
                        _defaultTitle = "Error";
                    }
                    else
                    {
                        _defaultTitle = "Error - " + Product;
                    }
                }

                return _defaultTitle;
            }

            set
            {
                _defaultTitle = value;
            }
        }

        /// <summary>
        /// Gets the value of the AssemblyProduct attribute of the app.  
        /// If unable to lookup the attribute, returns an empty string.
        /// </summary>
        public static string Product
        {
            get
            {
                if (_product == null)
                {
                    _product = GetProductName();
                }

                return _product;
            }
        }

        // Initializes the Product property.
        static string GetProductName()
        {
            string result = "";

            try
            {
                Assembly _appAssembly = GetAppAssembly();

                object[] customAttributes = _appAssembly.GetCustomAttributes(typeof(AssemblyProductAttribute), false);

                if ((customAttributes != null) && (customAttributes.Length > 0))
                {
                    result = ((AssemblyProductAttribute)customAttributes[0]).Product;
                }
            }
            catch
            { }

            return result;
        }

        // Tries to get the assembly to extract the product name from.
        private static Assembly GetAppAssembly()
        {
            Assembly _appAssembly = null;

            try
            {
                // This is supposedly how Windows.Forms.Application does it.
                if (Application.OpenForms.Count > 0)
                    _appAssembly = Application.OpenForms[0].GetType().Assembly;
            }
            catch
            { }

            // If the above didn't work, try less desireable ways to get an assembly.

            if (_appAssembly == null)
            {
                _appAssembly = Assembly.GetEntryAssembly();
            }

            if (_appAssembly == null)
            {
                _appAssembly = Assembly.GetExecutingAssembly();
            }

            return _appAssembly;
        }

        NodeInfo StartNewNodeInfo()
        {
            var info = new NodeInfo();
            info.SelectionStart = ErrorTextBox.SelectionStart;
            return info;
        }

        void EndNodeInfo(NodeInfo info)
        {
            int pos = ErrorTextBox.SelectionStart;
            info.SelectionLength = pos - info.SelectionStart;
            ErrorTextBox.Select(info.SelectionStart, info.SelectionLength);
            info.SelectionRtf = ErrorTextBox.SelectedRtf;
            ErrorTextBox.Select(pos, 0);
        }

        // Builds the tree in the left pane.
        // Each TreeViewItem.Tag will contain a list of Inlines
        // to display in the right-hand pane When it is selected.
        void BuildTree(Exception e, string summaryMessage)
        {
            try
            {
                LockWindowUpdate(ErrorTextBox.Handle);
                ErrorTextBox.Clear();

                // The first node in the tree contains the summary message and all the
                // nested exception messages.
                var firstItem = new TreeNode();
                firstItem.Text = "All Messages";
                firstItem.NodeFont = new System.Drawing.Font(ErrorTreeView.Font, FontStyle.Bold);

                var tag = StartNewNodeInfo();
                firstItem.Tag = tag;

                ErrorTreeView.Nodes.Clear();
                ErrorTreeView.Nodes.Add(firstItem);

                // Now add top-level nodes for each exception while building
                // the contents of the first node.
                int count = 0;
                var items = new List<Exception>();
                if (e != null) items.Add(e);
                while (items.Count > 0)
                {
                    e = items[0];
                    if (count > 0)
                    {
                        AppendText("____________________________________________________");
                        AppendNewLine(2);
                    }

                    AddException(e);

                    var aggEx = e as AggregateException;
                    if (aggEx != null)
                    {
                        foreach (var e2 in aggEx.InnerExceptions)
                            items.Add(e2);
                    }
                    else if (e.InnerException != null)
                    {
                        items.Add(e.InnerException);
                    }

                    items.RemoveAt(0);
                    ++count;
                }

                EndNodeInfo(tag);
                ErrorTreeView.SelectedNode = firstItem;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.TraceError("ErrorDialog.BuildTree - {0}: {1}\r\n{2}", ex.GetType().Name, ex.Message, ex);
            }
            finally
            {
                LockWindowUpdate(IntPtr.Zero);
            }

        }

        void AddNodeProperty(TreeNode parent, string propName, object propVal)
        {
            var node = new TreeNode();
            var tag = StartNewNodeInfo();

            node.Text = propName;
            node.Tag = tag;
            parent.Nodes.Add(node);
            AddProperty(propName, propVal);
            EndNodeInfo(tag);
        }

        void AddProperty(string propName, object propVal)
        {
            //AppendNewLine(1);
            AppendMediumText(propName + ":");
            AppendNewLine(1);

            string text = null;
            if (propVal is string)
            {
                text = (string)propVal;
            }
            else if (propVal != null)
            {
                text = propVal.ToString();
            }
            AddLines(propVal as string);
            AppendNewLine(1);
        }

        // Adds the string to the list of Inlines, substituting
        // LineBreaks for an newline chars found.
        void AddLines(string str)
        {
            string[] lines = (str ?? "").Split('\n');

            foreach (string line in lines)
            {
                AppendText(line.Trim('\r'));
                AppendNewLine();
            }
        }

        void AppendLargeText(string text)
        {
            AppendText(text, _large);
        }

        void AppendMediumText(string text)
        {
            AppendText(text, _med);
        }

        void AppendText(string text, Font font = null)
        {
            if (text == null) return;
            if (font == null) font = _small;
            int pos = ErrorTextBox.SelectionStart;
            ErrorTextBox.AppendText(text);
            int len = ErrorTextBox.TextLength - pos;
            ErrorTextBox.Select(pos, len);
            ErrorTextBox.SelectionFont = font;
            ErrorTextBox.Select(ErrorTextBox.TextLength, 0);
            // Reset to small font
            if (ErrorTextBox.SelectionFont != _small)
                ErrorTextBox.SelectionFont = _small;
        }

        void AppendNewLine(int count = 1)
        {
            for (int index = 0; index < count; index++)
            {
                ErrorTextBox.AppendText(Environment.NewLine);
            }
        }

        // Adds the exception as a new top-level node to the tree with child nodes
        // for all the exception's properties.
        void AddException(Exception e)
        {
            // Create a list of Inlines containing all the properties of the exception object.
            // The three most important properties (message, type, and stack trace) go first.
            var exceptionItem = new TreeNode();
            var tag = StartNewNodeInfo();
            System.Reflection.PropertyInfo[] properties = e.GetType().GetProperties();

            exceptionItem.Text = e.GetType().Name;
            exceptionItem.Tag = tag;
            ErrorTreeView.Nodes.Add(exceptionItem);

            AppendLargeText(e.GetType().Name);
            AppendNewLine(2);

            AddNodeProperty(exceptionItem, "Message", e.Message);
            AddNodeProperty(exceptionItem, "Stack Trace", e.StackTrace);

            foreach (PropertyInfo info in properties)
            {
                // Skip InnerException because it will get a whole
                // top-level node of its own.

                if (info.Name == "InnerException" ||
                    info.Name == "InnerExceptions" ||
                    info.Name == "Message" ||
                    info.Name == "StackTrace")
                    continue;

                object value = null;

                try
                {
                    // TargetSite will throw an exception if the referenced assembly cannot be loaded
                    value = info.GetValue(e, null);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Trace.TraceError("ErrorDialog.AddException - {0}: {1}\r\n{2}", info.Name, ex.Message, ex);
                }

                string text = null;
                if (value == null)
                {
                    continue;
                }
                else if (value is string)
                {
                    text = (string)value;
                }
                else if (value is IDictionary)
                {
                    text = RenderDictionary(value as IDictionary);
                }
                else if (value is IEnumerable && !(value is string))
                {
                    text = RenderEnumerable(value as IEnumerable);
                }
                else
                {
                    text = value.ToString();
                }

                if (!string.IsNullOrEmpty(text))
                {
                    // Add the property to list for the exceptionItem.
                    AddNodeProperty(exceptionItem, info.Name, text);
                }

            }
            EndNodeInfo(tag);
        }

        static string RenderEnumerable(IEnumerable data)
        {
            StringBuilder result = new StringBuilder();

            foreach (object obj in data)
            {
                result.AppendFormat("{0}\n", obj);
            }

            if (result.Length > 0) result.Length = result.Length - 1;
            return result.ToString();
        }

        static string RenderDictionary(IDictionary data)
        {
            StringBuilder result = new StringBuilder();

            foreach (object key in data.Keys)
            {
                if (key != null && data[key] != null)
                {
                    result.AppendLine(key.ToString() + " = " + data[key].ToString());
                }
            }

            if (result.Length > 0) result.Length = result.Length - 1;
            return result.ToString();
        }

        private void ErrorTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            ShowCurrentItem();
        }

        void ShowCurrentItem()
        {
            var node = ErrorTreeView.SelectedNode;
            if (node != null)
            {
                var info = node.Tag as NodeInfo;
                if (info != null)
                {
                    ErrorTextBox.Rtf = info.SelectionRtf;
                    ErrorTextBox.SelectionStart = 0;
                    ErrorTextBox.ScrollToCaret();
                }
            }
        }

        private void WrapTextCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            var source = (CheckBox)sender;
            ErrorTextBox.WordWrap = source.Checked;
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void CopyButton_Click(object sender, EventArgs e)
        {
            ErrorTreeView.SelectedNode = ErrorTreeView.Nodes[0];
            var dto = new DataObject();
            dto.SetText(ErrorTextBox.Text, TextDataFormat.UnicodeText);
            dto.SetText(ErrorTextBox.Rtf, TextDataFormat.Rtf);
            Clipboard.Clear();
            Clipboard.SetDataObject(dto);
        }

        /// <summary>
        /// Gets the Main Form (the first open form is the main form)
        /// </summary>
        static Form MainForm
        {
            get
            {
                if (Application.OpenForms.Count > 0)
                {
                    var f = Application.OpenForms[0];
                    // Do not use the form if it is disposing or disposed, nor if it is not visible
                    if (f != null && !f.Disposing && !f.IsDisposed && f.Visible)
                        return f;
                }
                return null;
            }
        }

        /// <summary>
        /// Show the Error information dialog - handles multi-threaded issues with the Owner (MainForm)
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="title"></param>
        public static void ShowExceptionForm(Exception ex, string title)
        {
            try
            {
                // Unwrap the inner exception of an AggregateException, if there is only 1 inner exception
                var ae = ex as AggregateException;
                if (ae != null && ae.InnerExceptions.Count == 1)
                {
                    ex = ae.InnerException;
                }

                var f = MainForm;
                if (f != null)
                {
                    f.Invoke((Action)(() =>
                    {
                        using (var errForm = new ErrorForm(title, ex, f))
                        {
                            errForm.ShowDialog(f);
                        }
                    }));
                }
                else
                {
                    using (var errForm = new ErrorForm(title, ex))
                    {
                        errForm.StartPosition = FormStartPosition.CenterScreen;
                        errForm.ShowDialog();
                    }
                }
            }
            catch
            {
                MessageBox.Show(ex.Message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
