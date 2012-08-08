using System;
using System.Reflection ;
using System.Windows.Forms;
using System.Collections.Generic;
using Xdgk.Common;


namespace C3.Communi
{
    public class DeviceUI : DeviceUIBase 
    {
        public DeviceUI(IDPU dpu)
        {
            this.Dpu = dpu;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="deviceType"></param>
        /// <param name="station"></param>
        /// <param name="newDevice"></param>
        /// <returns></returns>
        protected override DialogResult OnAdd(DeviceType deviceType, IStation station, out IDevice newDevice)
        {
            newDevice = null;
            FrmDeviceGroups f = new FrmDeviceGroups();

            f.DeviceType = deviceType; 
            //f.Device = (IDevice)Activator.CreateInstance(f.DeviceType.Type);
            f.Device = f.DeviceType.Create();
            f.Station = station;
            f.AdeStatus = ADEStatus .Add;
            f.Groups = f.Device.Groups;

            DialogResult dr = f.ShowDialog();
            if (dr == DialogResult.OK)
            {
                newDevice = f.Device;
            }
            return dr;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        protected override DialogResult OnEdit(IDevice device)
        {
            FrmDeviceGroups f = new FrmDeviceGroups();

            f.Device = device;
            f.Station = device.Station;
            f.DeviceType = device.DeviceType;
            f.AdeStatus = ADEStatus.Edit;

            f.Groups = device.Groups;

            DialogResult dr = f.ShowDialog();
            return dr;
        }
    }
}
