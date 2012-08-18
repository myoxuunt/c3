namespace C3.Communi
{
    partial class UCCommuniPortConfigUI
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
            C3.Communi.RemoteIPAddressConfig remoteIPAddressConfig1 = new C3.Communi.RemoteIPAddressConfig();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCCommuniPortConfigUI));
            C3.Communi.SerialPortSetting serialPortSetting1 = new C3.Communi.SerialPortSetting();
            this.rbSocket = new System.Windows.Forms.RadioButton();
            this.rbSerialPort = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.rbNull = new System.Windows.Forms.RadioButton();
            this.ucNetSetting1 = new C3.Communi.UC.Parameter.UCNetSetting();
            this.ucSerialPortSetting1 = new CommuniServer.UCSerialPortSetting();
            this.lblTimeout = new System.Windows.Forms.Label();
            this.numTimeout = new System.Windows.Forms.NumericUpDown();
            this.lblTimeoutUnit = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numTimeout)).BeginInit();
            this.SuspendLayout();
            // 
            // rbSocket
            // 
            this.rbSocket.Checked = true;
            this.rbSocket.Location = new System.Drawing.Point(103, 57);
            this.rbSocket.Name = "rbSocket";
            this.rbSocket.Size = new System.Drawing.Size(200, 21);
            this.rbSocket.TabIndex = 33;
            this.rbSocket.TabStop = true;
            this.rbSocket.Text = "通过网络";
            this.rbSocket.Click += new System.EventHandler(this.rbSocket_Click);
            // 
            // rbSerialPort
            // 
            this.rbSerialPort.Location = new System.Drawing.Point(103, 30);
            this.rbSerialPort.Name = "rbSerialPort";
            this.rbSerialPort.Size = new System.Drawing.Size(200, 21);
            this.rbSerialPort.TabIndex = 32;
            this.rbSerialPort.Text = "通过串口";
            this.rbSerialPort.Click += new System.EventHandler(this.rbSerialPort_Click);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(3, 3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 21);
            this.label5.TabIndex = 31;
            this.label5.Text = "通讯方式:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // rbNull
            // 
            this.rbNull.Location = new System.Drawing.Point(103, 3);
            this.rbNull.Name = "rbNull";
            this.rbNull.Size = new System.Drawing.Size(200, 21);
            this.rbNull.TabIndex = 36;
            this.rbNull.Text = "无";
            this.rbNull.Click += new System.EventHandler(this.rbNull_Click);
            this.rbNull.CheckedChanged += new System.EventHandler(this.rbNull_CheckedChanged);
            // 
            // ucNetSetting1
            // 
            remoteIPAddressConfig1.RemoteIPAddress = ((System.Net.IPAddress)(resources.GetObject("remoteIPAddressConfig1.RemoteIPAddress")));
            remoteIPAddressConfig1.RemoteIPAddressString = "0.0.0.0";
            remoteIPAddressConfig1.TimeoutMilliSecond = ((uint)(10000u));
            this.ucNetSetting1.CommuniPortConfig = remoteIPAddressConfig1;
            this.ucNetSetting1.Location = new System.Drawing.Point(0, 257);
            this.ucNetSetting1.Name = "ucNetSetting1";
            this.ucNetSetting1.Size = new System.Drawing.Size(405, 154);
            this.ucNetSetting1.TabIndex = 35;
            // 
            // ucSerialPortSetting1
            // 
            this.ucSerialPortSetting1.Location = new System.Drawing.Point(0, 112);
            this.ucSerialPortSetting1.Name = "ucSerialPortSetting1";
            serialPortSetting1.BaudRate = 9600;
            serialPortSetting1.DataBits = 8;
            serialPortSetting1.Parity = System.IO.Ports.Parity.None;
            serialPortSetting1.PortName = "Com1";
            serialPortSetting1.StopBits = System.IO.Ports.StopBits.One;
            this.ucSerialPortSetting1.SerialPortSetting = serialPortSetting1;
            this.ucSerialPortSetting1.Size = new System.Drawing.Size(368, 139);
            this.ucSerialPortSetting1.TabIndex = 34;
            // 
            // lblTimeout
            // 
            this.lblTimeout.Location = new System.Drawing.Point(3, 83);
            this.lblTimeout.Name = "lblTimeout";
            this.lblTimeout.Size = new System.Drawing.Size(100, 21);
            this.lblTimeout.TabIndex = 37;
            this.lblTimeout.Text = "超时时间:";
            this.lblTimeout.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // numTimeout
            // 
            this.numTimeout.Location = new System.Drawing.Point(103, 83);
            this.numTimeout.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.numTimeout.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numTimeout.Name = "numTimeout";
            this.numTimeout.Size = new System.Drawing.Size(150, 21);
            this.numTimeout.TabIndex = 38;
            this.numTimeout.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblTimeoutUnit
            // 
            this.lblTimeoutUnit.Location = new System.Drawing.Point(259, 83);
            this.lblTimeoutUnit.Name = "lblTimeoutUnit";
            this.lblTimeoutUnit.Size = new System.Drawing.Size(100, 21);
            this.lblTimeoutUnit.TabIndex = 39;
            this.lblTimeoutUnit.Text = "秒";
            this.lblTimeoutUnit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // UCCommuniPortConfigUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblTimeoutUnit);
            this.Controls.Add(this.numTimeout);
            this.Controls.Add(this.lblTimeout);
            this.Controls.Add(this.rbNull);
            this.Controls.Add(this.ucNetSetting1);
            this.Controls.Add(this.ucSerialPortSetting1);
            this.Controls.Add(this.rbSocket);
            this.Controls.Add(this.rbSerialPort);
            this.Controls.Add(this.label5);
            this.Name = "UCCommuniPortConfigUI";
            this.Size = new System.Drawing.Size(400, 500);
            this.Load += new System.EventHandler(this.UCCommuniPortConfig_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numTimeout)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RadioButton rbSocket;
        private System.Windows.Forms.RadioButton rbSerialPort;
        private System.Windows.Forms.Label label5;
        private CommuniServer.UCSerialPortSetting ucSerialPortSetting1;
        private C3.Communi.UC.Parameter.UCNetSetting ucNetSetting1;
        private System.Windows.Forms.RadioButton rbNull;
        private System.Windows.Forms.Label lblTimeout;
        private System.Windows.Forms.NumericUpDown numTimeout;
        private System.Windows.Forms.Label lblTimeoutUnit;
    }
}
