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
            this.panelByRemoteIPAndPort = new System.Windows.Forms.Panel();
            this.txtRemoteIPAddress = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.numRemotePort2 = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbConnectionType = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.panelByLocalPort.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numLocalPort)).BeginInit();
            this.panelBylRemotePort.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRemotePort)).BeginInit();
            this.panelByIPAddress.SuspendLayout();
            this.panelByRemoteIPAndPort.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRemotePort2)).BeginInit();
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
            // panelByRemoteIPAndPort
            // 
            this.panelByRemoteIPAndPort.Controls.Add(this.numRemotePort2);
            this.panelByRemoteIPAndPort.Controls.Add(this.label6);
            this.panelByRemoteIPAndPort.Controls.Add(this.txtRemoteIPAddress);
            this.panelByRemoteIPAndPort.Controls.Add(this.label5);
            this.panelByRemoteIPAndPort.Controls.Add(this.label7);
            this.panelByRemoteIPAndPort.Controls.Add(this.cmbConnectionType);
            this.panelByRemoteIPAndPort.Location = new System.Drawing.Point(0, 96);
            this.panelByRemoteIPAndPort.Name = "panelByRemoteIPAndPort";
            this.panelByRemoteIPAndPort.Size = new System.Drawing.Size(390, 83);
            this.panelByRemoteIPAndPort.TabIndex = 5;
            // 
            // txtRemoteIPAddress
            // 
            this.txtRemoteIPAddress.Location = new System.Drawing.Point(103, 3);
            this.txtRemoteIPAddress.Name = "txtRemoteIPAddress";
            this.txtRemoteIPAddress.Size = new System.Drawing.Size(150, 21);
            this.txtRemoteIPAddress.TabIndex = 3;
            this.txtRemoteIPAddress.Text = "0.0.0.0";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(3, 3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 21);
            this.label5.TabIndex = 0;
            this.label5.Text = "站点IP:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // numRemotePort2
            // 
            this.numRemotePort2.Location = new System.Drawing.Point(103, 30);
            this.numRemotePort2.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numRemotePort2.Name = "numRemotePort2";
            this.numRemotePort2.Size = new System.Drawing.Size(150, 21);
            this.numRemotePort2.TabIndex = 4;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(3, 28);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(100, 21);
            this.label6.TabIndex = 1;
            this.label6.Text = "远程端口:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbConnectionType
            // 
            this.cmbConnectionType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbConnectionType.FormattingEnabled = true;
            this.cmbConnectionType.Location = new System.Drawing.Point(103, 57);
            this.cmbConnectionType.Name = "cmbConnectionType";
            this.cmbConnectionType.Size = new System.Drawing.Size(150, 20);
            this.cmbConnectionType.TabIndex = 5;
            this.cmbConnectionType.SelectedIndexChanged += new System.EventHandler(this.cmbDiscriminateMode_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(3, 56);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(100, 21);
            this.label7.TabIndex = 2;
            this.label7.Text = "连接方式:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
            this.Controls.Add(this.panelByRemoteIPAndPort);
            this.Name = "UCNetSetting";
            this.Size = new System.Drawing.Size(405, 325);
            this.Load += new System.EventHandler(this.UCNetSetting_Load);
            this.panelByLocalPort.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numLocalPort)).EndInit();
            this.panelBylRemotePort.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numRemotePort)).EndInit();
            this.panelByIPAddress.ResumeLayout(false);
            this.panelByIPAddress.PerformLayout();
            this.panelByRemoteIPAndPort.ResumeLayout(false);
            this.panelByRemoteIPAndPort.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRemotePort2)).EndInit();
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
        private System.Windows.Forms.Panel panelByRemoteIPAndPort;
        private System.Windows.Forms.NumericUpDown numRemotePort2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtRemoteIPAddress;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbConnectionType;
    }
}
