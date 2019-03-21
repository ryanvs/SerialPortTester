namespace SerialPortTester
{
    partial class ErrorForm
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
            this.ErrorTreeView = new System.Windows.Forms.TreeView();
            this.ErrorSplitter = new System.Windows.Forms.Splitter();
            this.ErrorTextBox = new System.Windows.Forms.RichTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.CloseButton = new System.Windows.Forms.Button();
            this.CopyButton = new System.Windows.Forms.Button();
            this.WrapTextCheckBox = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ErrorTreeView
            // 
            this.ErrorTreeView.Dock = System.Windows.Forms.DockStyle.Left;
            this.ErrorTreeView.FullRowSelect = true;
            this.ErrorTreeView.HideSelection = false;
            this.ErrorTreeView.Location = new System.Drawing.Point(0, 0);
            this.ErrorTreeView.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ErrorTreeView.Name = "ErrorTreeView";
            this.ErrorTreeView.Size = new System.Drawing.Size(207, 281);
            this.ErrorTreeView.TabIndex = 0;
            this.ErrorTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.ErrorTreeView_AfterSelect);
            // 
            // ErrorSplitter
            // 
            this.ErrorSplitter.Location = new System.Drawing.Point(207, 0);
            this.ErrorSplitter.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ErrorSplitter.Name = "ErrorSplitter";
            this.ErrorSplitter.Size = new System.Drawing.Size(7, 281);
            this.ErrorSplitter.TabIndex = 1;
            this.ErrorSplitter.TabStop = false;
            // 
            // ErrorTextBox
            // 
            this.ErrorTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.ErrorTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ErrorTextBox.Location = new System.Drawing.Point(214, 0);
            this.ErrorTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ErrorTextBox.Name = "ErrorTextBox";
            this.ErrorTextBox.ReadOnly = true;
            this.ErrorTextBox.Size = new System.Drawing.Size(378, 281);
            this.ErrorTextBox.TabIndex = 2;
            this.ErrorTextBox.Text = "";
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.CloseButton);
            this.panel1.Controls.Add(this.CopyButton);
            this.panel1.Controls.Add(this.WrapTextCheckBox);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 281);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(592, 42);
            this.panel1.TabIndex = 3;
            // 
            // CloseButton
            // 
            this.CloseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CloseButton.Location = new System.Drawing.Point(477, 4);
            this.CloseButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(93, 34);
            this.CloseButton.TabIndex = 2;
            this.CloseButton.Text = "Close";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // CopyButton
            // 
            this.CopyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CopyButton.Location = new System.Drawing.Point(330, 4);
            this.CopyButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.CopyButton.Name = "CopyButton";
            this.CopyButton.Size = new System.Drawing.Size(140, 34);
            this.CopyButton.TabIndex = 1;
            this.CopyButton.Text = "Copy To Clipboard";
            this.CopyButton.UseVisualStyleBackColor = true;
            this.CopyButton.Click += new System.EventHandler(this.CopyButton_Click);
            // 
            // WrapTextCheckBox
            // 
            this.WrapTextCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.WrapTextCheckBox.AutoSize = true;
            this.WrapTextCheckBox.Checked = true;
            this.WrapTextCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.WrapTextCheckBox.Location = new System.Drawing.Point(228, 12);
            this.WrapTextCheckBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.WrapTextCheckBox.Name = "WrapTextCheckBox";
            this.WrapTextCheckBox.Size = new System.Drawing.Size(84, 21);
            this.WrapTextCheckBox.TabIndex = 0;
            this.WrapTextCheckBox.Text = "&Wrap text";
            this.WrapTextCheckBox.UseVisualStyleBackColor = true;
            this.WrapTextCheckBox.CheckedChanged += new System.EventHandler(this.WrapTextCheckBox_CheckedChanged);
            // 
            // ErrorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(592, 323);
            this.Controls.Add(this.ErrorTextBox);
            this.Controls.Add(this.ErrorSplitter);
            this.Controls.Add(this.ErrorTreeView);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimizeBox = false;
            this.Name = "ErrorForm";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Error";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView ErrorTreeView;
        private System.Windows.Forms.Splitter ErrorSplitter;
        private System.Windows.Forms.RichTextBox ErrorTextBox;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.Button CopyButton;
        private System.Windows.Forms.CheckBox WrapTextCheckBox;
    }
}