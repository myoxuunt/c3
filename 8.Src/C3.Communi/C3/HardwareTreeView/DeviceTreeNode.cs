
using System;
using System.Windows.Forms;
using Xdgk.Common;

namespace C3.Communi
{
    public class DeviceTreeNode : TreeNode
    {
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="device"></param>
        public DeviceTreeNode(IDevice device)
        {
            this.Device = device;
            this.Device.Tag = this;

            this.ImageKey = IconNames.Device;
            this.SelectedImageKey = IconNames.Device;

            this.Name = device.Name;
            this.Text = device.Text;
        }

    }

}
