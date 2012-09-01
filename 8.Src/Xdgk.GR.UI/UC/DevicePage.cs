using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Xdgk.Communi;

namespace CZGR
{
    public class DevicePage : Option.OptionPage  
    {
        private TextBox txtDeviceType;
        private TextBox txtDeviceAddress;
        private Label label2;
        private TextBox txtStation;
        private Label label3;
        private Label label1;

        private Device _device;
        public DevicePage(Device device)
        {
            _device = device;
        }

        /// <summary>
        /// 
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.txtDeviceType = new System.Windows.Forms.TextBox();
            this.txtDeviceAddress = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtStation = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "设备类型：";
            // 
            // txtDeviceType
            // 
            this.txtDeviceType.Location = new System.Drawing.Point(104, 49);
            this.txtDeviceType.Name = "txtDeviceType";
            this.txtDeviceType.Size = new System.Drawing.Size(174, 21);
            this.txtDeviceType.TabIndex = 1;
            // 
            // txtDeviceAddress
            // 
            this.txtDeviceAddress.Location = new System.Drawing.Point(104, 76);
            this.txtDeviceAddress.Name = "txtDeviceAddress";
            this.txtDeviceAddress.Size = new System.Drawing.Size(174, 21);
            this.txtDeviceAddress.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "设备地址：";
            // 
            // txtStation
            // 
            this.txtStation.Location = new System.Drawing.Point(104, 103);
            this.txtStation.Name = "txtStation";
            this.txtStation.Size = new System.Drawing.Size(174, 21);
            this.txtStation.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(33, 106);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "所属站点：";
            // 
            // DevicePage
            // 
            this.Controls.Add(this.txtStation);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtDeviceAddress);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtDeviceType);
            this.Controls.Add(this.label1);
            this.Name = "DevicePage";
            this.Size = new System.Drawing.Size(362, 290);
            this.Load += new System.EventHandler(this.DevicePage_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }
        private void DevicePage_Load(object sender, EventArgs e)
        {
        }
    }
}
