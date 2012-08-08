
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
    public class FrmDeviceGroups : FrmGroups
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
        protected override bool Verify()
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

        #region Fill
        /// <summary>
        /// 
        /// </summary>
        protected override void Fill()
        {
            // set form text
            //
            this.Text = string.Format(
                "{0} - {1} : {2}", 
                ADEStatusText.GetText(this.AdeStatus),
                this.Station.Name,
                this.DeviceType.Text);

            base.Fill();
        }
        #endregion //Fill
    }

}
