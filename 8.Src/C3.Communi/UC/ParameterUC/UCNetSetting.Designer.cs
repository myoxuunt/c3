namespace C3.Communi
{
    partial class UCNetSetting
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.panelByLocalPort = new System.Windows.Forms.Panel();
            this.numLocalPort = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.panelBylRemotePort = new System.Windows.Forms.Panel();
            this.numRemotePort = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.panelByIPAddress = new System.Windows.Forms.Panel();
            this.txtStationIP = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbDiscriminateMode = new System.Windows.Forms.ComboBox();
            this.panelByLocalPort.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numLocalPort)).BeginInit();
            this.panelBylRemotePort.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRemotePort)).BeginInit();
            this.panelByIPAddress.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelByLocalPort
            // 
            this.panelByLocalPort.Controls.Add(this.numLocalPort);
            this.panelByLocalPort.Controls.Add(this.label3);
            this.panelByLocalPort.Location = new System.Drawing.Point(0, 24);
            this.panelByLocalPort.Name = "panelByLocalPort";
            this.panelByLocalPort.Size = new System.Drawing.Size(390, 30);
            this.panelByLocalPort.TabIndex = 2;
            // 
            // numLocalPort
            // 
            this.numLocalPort.Location = new System.Drawing.Point(103, 3);
            this.numLocalPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numLocalPort.Name = "numLocalPort";
            this.numLocalPort.Size = new System.Drawing.Size(150, 21);
            this.numLocalPort.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(3, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 21);
            this.label3.TabIndex = 0;
            this.label3.Text = "本地端口:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelBylRemotePort
            // 
            this.panelBylRemotePort.Controls.Add(this.numRemotePort);
            this.panelBylRemotePort.Controls.Add(this.label4);
            this.panelBylRemotePort.Location = new System.Drawing.Point(0, 60);
            this.panelBylRemotePort.Name = "panelBylRemotePort";
            this.panelBylRemotePort.Size = new System.Drawing.Size(390, 30);
            this.panelBylRemotePort.TabIndex = 3;
            // 
            // numRemotePort
            // 
            this.numRemotePort.Location = new System.Drawing.Point(103, 3);
            this.numRemotePort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numRemotePort.Name = "numRemotePort";
            this.numRemotePort.Size = new System.Drawing.Size(150, 21);
            this.numRemotePort.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(3, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 21);
            this.label4.TabIndex = 0;
            this.label4.Text = "远程端口:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelByIPAddress
            // 
            this.panelByIPAddress.Controls.Add(this.txtStationIP);
            this.panelByIPAddress.Controls.Add(this.label1);
            this.panelByIPAddress.Location = new System.Drawing.Point(0, 96);
            this.panelByIPAddress.Name = "panelByIPAddress";
            this.panelByIPAddress.Size = new System.Drawing.Size(390, 30);
            this.panelByIPAddress.TabIndex = 4;
            // 
            // txtStationIP
            // 
            this.txtStationIP.Location = new System.Drawing.Point(103, 3);
            this.txtStationIP.Name = "txtStationIP";
            this.txtStationIP.Size = new System.Drawing.Size(150, 21);
            this.txtStationIP.TabIndex = 1;
            this.txtStationIP.Text = "0.0.0.0";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "站点IP:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(3, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 21);
            this.label2.TabIndex = 0;
            this.label2.Text = "标识方式:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbDiscriminateMode
            // 
            this.cmbDiscriminateMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDiscriminateMode.FormattingEnabled = true;
            this.cmbDiscriminateMode.Location = new System.Drawing.Point(103, 3);
            this.cmbDiscriminateMode.Name = "cmbDiscriminateMode";
            this.cmbDiscriminateMode.Size = new System.Drawing.Size(150, 20);
            this.cmbDiscriminateMode.TabIndex = 1;
            this.cmbDiscriminateMode.SelectedIndexChanged += new System.EventHandler(this.cmbDiscriminateMode_SelectedIndexChanged);
            // 
            // UCNetSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelByIPAddress);
            this.Controls.Add(this.panelBylRemotePort);
            this.Controls.Add(this.panelByLocalPort);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbDiscriminateMode);
            this.Name = "UCNetSetting";
            this.Size = new System.Drawing.Size(405, 154);
            this.Load += new System.EventHandler(this.UCNetSetting_Load);
            this.panelByLocalPort.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numLocalPort)).EndInit();
            this.panelBylRemotePort.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numRemotePort)).EndInit();
            this.panelByIPAddress.ResumeLayout(false);
            this.panelByIPAddress.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelByLocalPort;
        private System.Windows.Forms.NumericUpDown numLocalPort;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panelBylRemotePort;
        private System.Windows.Forms.NumericUpDown numRemotePort;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panelByIPAddress;
        private System.Windows.Forms.TextBox txtStationIP;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbDiscriminateMode;
    }
}
