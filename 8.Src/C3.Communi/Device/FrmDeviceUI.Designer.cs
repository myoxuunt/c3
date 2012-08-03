namespace C3.Communi
{
    partial class FrmDeviceUI
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
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.gvDevice = new System.Windows.Forms.DataGridView();
            this.chName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ucDeviceParameters1 = new C3.Communi.UCDeviceParameters();
            ((System.ComponentModel.ISupportInitialize)(this.gvDevice)).BeginInit();
            this.SuspendLayout();
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(344, 375);
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(422, 375);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(12, 12);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(268, 68);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // gvDevice
            // 
            this.gvDevice.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvDevice.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.chName,
            this.chValue});
            this.gvDevice.Location = new System.Drawing.Point(12, 86);
            this.gvDevice.Name = "gvDevice";
            this.gvDevice.RowTemplate.Height = 23;
            this.gvDevice.Size = new System.Drawing.Size(268, 150);
            this.gvDevice.TabIndex = 19;
            // 
            // chName
            // 
            this.chName.DataPropertyName = "Name";
            this.chName.HeaderText = "Name";
            this.chName.Name = "chName";
            this.chName.ReadOnly = true;
            // 
            // chValue
            // 
            this.chValue.DataPropertyName = "Value";
            this.chValue.HeaderText = "Value";
            this.chValue.Name = "chValue";
            // 
            // ucDeviceParameters1
            // 
            this.ucDeviceParameters1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ucDeviceParameters1.Location = new System.Drawing.Point(344, 69);
            this.ucDeviceParameters1.Name = "ucDeviceParameters1";
            this.ucDeviceParameters1.Size = new System.Drawing.Size(537, 300);
            this.ucDeviceParameters1.TabIndex = 20;
            // 
            // FrmDeviceUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(934, 442);
            this.Controls.Add(this.ucDeviceParameters1);
            this.Controls.Add(this.gvDevice);
            this.Controls.Add(this.richTextBox1);
            this.Name = "FrmDeviceUI";
            this.Text = "FrmDeviceUI";
            this.Load += new System.EventHandler(this.FrmDeviceUI_Load);
            this.Controls.SetChildIndex(this.okButton, 0);
            this.Controls.SetChildIndex(this.cancelButton, 0);
            this.Controls.SetChildIndex(this.richTextBox1, 0);
            this.Controls.SetChildIndex(this.gvDevice, 0);
            this.Controls.SetChildIndex(this.ucDeviceParameters1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.gvDevice)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.DataGridView gvDevice;
        private System.Windows.Forms.DataGridViewTextBoxColumn chName;
        private System.Windows.Forms.DataGridViewTextBoxColumn chValue;
        private UCDeviceParameters ucDeviceParameters1;
    }
}