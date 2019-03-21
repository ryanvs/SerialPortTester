namespace SerialPortTester
{
    partial class ConfigPortForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.myErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.myToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.PortNameLabel = new System.Windows.Forms.Label();
            this.PortNameComboBox = new System.Windows.Forms.ComboBox();
            this.BaudRateLabel = new System.Windows.Forms.Label();
            this.BaudRateComboBox = new System.Windows.Forms.ComboBox();
            this.DataBitsLabel = new System.Windows.Forms.Label();
            this.DataBitsComboBox = new System.Windows.Forms.ComboBox();
            this.ParityLabel = new System.Windows.Forms.Label();
            this.ParityComboBox = new System.Windows.Forms.ComboBox();
            this.StopBitsLabel = new System.Windows.Forms.Label();
            this.StopBitsComboBox = new System.Windows.Forms.ComboBox();
            this.HandshakeLabel = new System.Windows.Forms.Label();
            this.HandshakeComboBox = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.MyOkButton = new System.Windows.Forms.Button();
            this.MyCancelButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.myErrorProvider)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // myErrorProvider
            // 
            this.myErrorProvider.ContainerControl = this;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.PortNameLabel, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.PortNameComboBox, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.BaudRateLabel, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.BaudRateComboBox, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.DataBitsLabel, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.DataBitsComboBox, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.ParityLabel, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.ParityComboBox, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.StopBitsLabel, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.StopBitsComboBox, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.HandshakeLabel, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.HandshakeComboBox, 1, 5);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 9;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(302, 213);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // PortNameLabel
            // 
            this.PortNameLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.PortNameLabel.AutoSize = true;
            this.PortNameLabel.Location = new System.Drawing.Point(3, 6);
            this.PortNameLabel.Name = "PortNameLabel";
            this.PortNameLabel.Size = new System.Drawing.Size(75, 17);
            this.PortNameLabel.TabIndex = 0;
            this.PortNameLabel.Text = "Port &Name";
            // 
            // PortNameComboBox
            // 
            this.PortNameComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.PortNameComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.PortNameComboBox.FormattingEnabled = true;
            this.PortNameComboBox.Location = new System.Drawing.Point(89, 3);
            this.PortNameComboBox.Name = "PortNameComboBox";
            this.PortNameComboBox.Size = new System.Drawing.Size(190, 24);
            this.PortNameComboBox.TabIndex = 1;
            this.PortNameComboBox.SelectedIndexChanged += new System.EventHandler(this.PortNameComboBox_SelectedIndexChanged);
            this.PortNameComboBox.Validating += new System.ComponentModel.CancelEventHandler(this.ComboBox_Validating_MustSelectIndex);
            // 
            // BaudRateLabel
            // 
            this.BaudRateLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.BaudRateLabel.AutoSize = true;
            this.BaudRateLabel.Location = new System.Drawing.Point(3, 36);
            this.BaudRateLabel.Name = "BaudRateLabel";
            this.BaudRateLabel.Size = new System.Drawing.Size(75, 17);
            this.BaudRateLabel.TabIndex = 0;
            this.BaudRateLabel.Text = "&Baud Rate";
            // 
            // BaudRateComboBox
            // 
            this.BaudRateComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.BaudRateComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.BaudRateComboBox.FormattingEnabled = true;
            this.BaudRateComboBox.Location = new System.Drawing.Point(89, 33);
            this.BaudRateComboBox.Name = "BaudRateComboBox";
            this.BaudRateComboBox.Size = new System.Drawing.Size(190, 24);
            this.BaudRateComboBox.TabIndex = 1;
            this.BaudRateComboBox.SelectedIndexChanged += new System.EventHandler(this.ComboBoxValidate_SelectedIndexChanged);
            this.BaudRateComboBox.Validating += new System.ComponentModel.CancelEventHandler(this.ComboBox_Validating_MustSelectIndex);
            // 
            // DataBitsLabel
            // 
            this.DataBitsLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.DataBitsLabel.AutoSize = true;
            this.DataBitsLabel.Location = new System.Drawing.Point(3, 66);
            this.DataBitsLabel.Name = "DataBitsLabel";
            this.DataBitsLabel.Size = new System.Drawing.Size(65, 17);
            this.DataBitsLabel.TabIndex = 0;
            this.DataBitsLabel.Text = "&Data Bits";
            // 
            // DataBitsComboBox
            // 
            this.DataBitsComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.DataBitsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DataBitsComboBox.FormattingEnabled = true;
            this.DataBitsComboBox.Location = new System.Drawing.Point(89, 63);
            this.DataBitsComboBox.Name = "DataBitsComboBox";
            this.DataBitsComboBox.Size = new System.Drawing.Size(190, 24);
            this.DataBitsComboBox.TabIndex = 1;
            this.DataBitsComboBox.SelectedIndexChanged += new System.EventHandler(this.ComboBoxValidate_SelectedIndexChanged);
            this.DataBitsComboBox.Validating += new System.ComponentModel.CancelEventHandler(this.ComboBox_Validating_MustSelectIndex);
            // 
            // ParityLabel
            // 
            this.ParityLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.ParityLabel.AutoSize = true;
            this.ParityLabel.Location = new System.Drawing.Point(3, 96);
            this.ParityLabel.Name = "ParityLabel";
            this.ParityLabel.Size = new System.Drawing.Size(44, 17);
            this.ParityLabel.TabIndex = 0;
            this.ParityLabel.Text = "&Parity";
            // 
            // ParityComboBox
            // 
            this.ParityComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.ParityComboBox.DisplayMember = "Key";
            this.ParityComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ParityComboBox.FormattingEnabled = true;
            this.ParityComboBox.Location = new System.Drawing.Point(89, 93);
            this.ParityComboBox.Name = "ParityComboBox";
            this.ParityComboBox.Size = new System.Drawing.Size(190, 24);
            this.ParityComboBox.TabIndex = 1;
            this.ParityComboBox.ValueMember = "Value";
            this.ParityComboBox.SelectedIndexChanged += new System.EventHandler(this.ComboBoxValidate_SelectedIndexChanged);
            this.ParityComboBox.Validating += new System.ComponentModel.CancelEventHandler(this.ComboBox_Validating_MustSelectIndex);
            // 
            // StopBitsLabel
            // 
            this.StopBitsLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.StopBitsLabel.AutoSize = true;
            this.StopBitsLabel.Location = new System.Drawing.Point(3, 126);
            this.StopBitsLabel.Name = "StopBitsLabel";
            this.StopBitsLabel.Size = new System.Drawing.Size(64, 17);
            this.StopBitsLabel.TabIndex = 0;
            this.StopBitsLabel.Text = "&Stop Bits";
            // 
            // StopBitsComboBox
            // 
            this.StopBitsComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.StopBitsComboBox.DisplayMember = "Key";
            this.StopBitsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.StopBitsComboBox.FormattingEnabled = true;
            this.StopBitsComboBox.Location = new System.Drawing.Point(89, 123);
            this.StopBitsComboBox.Name = "StopBitsComboBox";
            this.StopBitsComboBox.Size = new System.Drawing.Size(190, 24);
            this.StopBitsComboBox.TabIndex = 1;
            this.StopBitsComboBox.ValueMember = "Value";
            this.StopBitsComboBox.SelectedIndexChanged += new System.EventHandler(this.ComboBoxValidate_SelectedIndexChanged);
            this.StopBitsComboBox.Validating += new System.ComponentModel.CancelEventHandler(this.ComboBox_Validating_MustSelectIndex);
            // 
            // HandshakeLabel
            // 
            this.HandshakeLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.HandshakeLabel.AutoSize = true;
            this.HandshakeLabel.Location = new System.Drawing.Point(3, 156);
            this.HandshakeLabel.Name = "HandshakeLabel";
            this.HandshakeLabel.Size = new System.Drawing.Size(80, 17);
            this.HandshakeLabel.TabIndex = 0;
            this.HandshakeLabel.Text = "&Handshake";
            // 
            // HandshakeComboBox
            // 
            this.HandshakeComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.HandshakeComboBox.DisplayMember = "Key";
            this.HandshakeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.HandshakeComboBox.FormattingEnabled = true;
            this.HandshakeComboBox.Location = new System.Drawing.Point(89, 153);
            this.HandshakeComboBox.Name = "HandshakeComboBox";
            this.HandshakeComboBox.Size = new System.Drawing.Size(190, 24);
            this.HandshakeComboBox.TabIndex = 1;
            this.HandshakeComboBox.ValueMember = "Value";
            this.HandshakeComboBox.SelectedIndexChanged += new System.EventHandler(this.ComboBoxValidate_SelectedIndexChanged);
            this.HandshakeComboBox.Validating += new System.ComponentModel.CancelEventHandler(this.ComboBox_Validating_MustSelectIndex);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.AutoSize = true;
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.MyOkButton, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.MyCancelButton, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 213);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(302, 34);
            this.tableLayoutPanel2.TabIndex = 4;
            // 
            // MyOkButton
            // 
            this.MyOkButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.MyOkButton.Location = new System.Drawing.Point(35, 3);
            this.MyOkButton.Name = "MyOkButton";
            this.MyOkButton.Size = new System.Drawing.Size(80, 28);
            this.MyOkButton.TabIndex = 0;
            this.MyOkButton.Text = "&OK";
            this.MyOkButton.UseVisualStyleBackColor = true;
            this.MyOkButton.Click += new System.EventHandler(this.MyOkButton_Click);
            // 
            // MyCancelButton
            // 
            this.MyCancelButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.MyCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.MyCancelButton.Location = new System.Drawing.Point(186, 3);
            this.MyCancelButton.Name = "MyCancelButton";
            this.MyCancelButton.Size = new System.Drawing.Size(80, 28);
            this.MyCancelButton.TabIndex = 0;
            this.MyCancelButton.Text = "&Cancel";
            this.MyCancelButton.UseVisualStyleBackColor = true;
            // 
            // ConfigPortForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.MyCancelButton;
            this.ClientSize = new System.Drawing.Size(302, 247);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.tableLayoutPanel2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(250, 250);
            this.Name = "ConfigPortForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Configure Port";
            ((System.ComponentModel.ISupportInitialize)(this.myErrorProvider)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ErrorProvider myErrorProvider;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label PortNameLabel;
        private System.Windows.Forms.ComboBox PortNameComboBox;
        private System.Windows.Forms.Label BaudRateLabel;
        private System.Windows.Forms.ComboBox BaudRateComboBox;
        private System.Windows.Forms.Label DataBitsLabel;
        private System.Windows.Forms.ComboBox DataBitsComboBox;
        private System.Windows.Forms.Label ParityLabel;
        private System.Windows.Forms.ComboBox ParityComboBox;
        private System.Windows.Forms.Label StopBitsLabel;
        private System.Windows.Forms.ComboBox StopBitsComboBox;
        private System.Windows.Forms.Label HandshakeLabel;
        private System.Windows.Forms.ComboBox HandshakeComboBox;
        private System.Windows.Forms.ToolTip myToolTip;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button MyOkButton;
        private System.Windows.Forms.Button MyCancelButton;
    }
}