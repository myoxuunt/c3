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
            this.rbSocket = new System.Windows.Forms.RadioButton();
            this.rbSerialPort = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.rbNull = new System.Windows.Forms.RadioButton();
            this.ucNetSetting1 = new C3.Communi.UC.Parameter.UCNetSetting();
            this.ucSerialPortSetting1 = new CommuniServer.UCSerialPortSetting();
            this.SuspendLayout();
            // 
            // rbSocket
            // 
            this.rbSocket.Checked = true;
            this.rbSocket.Location = new System.Drawing.Point(153, 57);
            this.rbSocket.Name = "rbSocket";
            this.rbSocket.Size = new System.Drawing.Size(200, 21);
            this.rbSocket.TabIndex = 33;
            this.rbSocket.TabStop = true;
            this.rbSocket.Text = "通过网络";
            this.rbSocket.Click += new System.EventHandler(this.rbSocket_Click);
            // 
            // rbSerialPort
            // 
            this.rbSerialPort.Location = new System.Drawing.Point(153, 30);
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
            this.label5.Size = new System.Drawing.Size(150, 21);
            this.label5.TabIndex = 31;
            this.label5.Text = "通讯方式：";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // rbNull
            // 
            this.rbNull.Location = new System.Drawing.Point(153, 3);
            this.rbNull.Name = "rbNull";
            this.rbNull.Size = new System.Drawing.Size(200, 21);
            this.rbNull.TabIndex = 36;
            this.rbNull.Text = "无";
            this.rbNull.Click += new System.EventHandler(this.rbNull_Click);
            this.rbNull.CheckedChanged += new System.EventHandler(this.rbNull_CheckedChanged);
            // 
            // ucNetSetting1
            // 
            this.ucNetSetting1.CommuniPortConfig = null;
            this.ucNetSetting1.Location = new System.Drawing.Point(0, 243);
            this.ucNetSetting1.Name = "ucNetSetting1";
            this.ucNetSetting1.Size = new System.Drawing.Size(405, 154);
            this.ucNetSetting1.TabIndex = 35;
            // 
            // ucSerialPortSetting1
            // 
            this.ucSerialPortSetting1.Location = new System.Drawing.Point(0, 98);
            this.ucSerialPortSetting1.Name = "ucSerialPortSetting1";
            this.ucSerialPortSetting1.Size = new System.Drawing.Size(368, 139);
            this.ucSerialPortSetting1.TabIndex = 34;
            // 
            // UCCommuniPortConfigUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.rbNull);
            this.Controls.Add(this.ucNetSetting1);
            this.Controls.Add(this.ucSerialPortSetting1);
            this.Controls.Add(this.rbSocket);
            this.Controls.Add(this.rbSerialPort);
            this.Controls.Add(this.label5);
            this.Name = "UCCommuniPortConfigUI";
            this.Size = new System.Drawing.Size(400, 500);
            this.Load += new System.EventHandler(this.UCCommuniPortConfig_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RadioButton rbSocket;
        private System.Windows.Forms.RadioButton rbSerialPort;
        private System.Windows.Forms.Label label5;
        private CommuniServer.UCSerialPortSetting ucSerialPortSetting1;
        private C3.Communi.UC.Parameter.UCNetSetting ucNetSetting1;
        private System.Windows.Forms.RadioButton rbNull;
    }
}
