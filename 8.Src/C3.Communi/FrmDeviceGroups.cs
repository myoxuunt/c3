using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Xdgk.Common;

namespace C3.Communi
{
    public class FrmDeviceGroups : frmGroup2
    {
        #region DeviceType
        /// <summary>
        /// 
        /// </summary>
        public DeviceType DeviceType
        {
            get
            {
                return _deviceType;
            }
            set
            {
                _deviceType = value;
            }
        } private DeviceType _deviceType;
        #endregion //DeviceType

        #region Device
        /// <summary>
        /// 
        /// </summary>
        public IDevice Device
        {
            get
            {
                return _device;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Device");
                }
                _device = value;
            }
        } private IDevice _device;
        #endregion //Device

        #region Station
        /// <summary>
        /// 
        /// </summary>
        public IStation Station
        {
            get
            {
                return _station;
            }
            set
            {
                _station = value;
            }
        } private IStation _station;
        #endregion //Station

        #region Verify
        /// <summary>
        /// 
        /// </summary>
        protected override bool  Verify2()
        {
            bool exist = this.Station.Devices.ExistAddress(this.Device.Address, this.Device);
            if (exist)
            {
                NUnit.UiKit.UserMessage.DisplayFailure(
                        string.Format("Exist address '{0}'", Device.Address)
                        );
            }
            return !exist;
        }
        #endregion //Verify

        /// <summary>
        /// 
        /// </summary>
        protected override void  SetFormText()
        {
            // set form text
            //
            this.Text = string.Format(
                "{0} - {1} : {2}", 
                ADEStatusText.GetText(this.AdeStatus),
                this.Station.Name,
                this.DeviceType.Text);

        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // FrmDeviceGroups
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(665, 513);
            this.Name = "FrmDeviceGroups";
            this.Load += new System.EventHandler(this.FrmDeviceGroups_Load);
            this.ResumeLayout(false);

        }

        private void FrmDeviceGroups_Load(object sender, EventArgs e)
        {

        }
    }
}
