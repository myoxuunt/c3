using System;
using System.Reflection ;
using System.Windows.Forms;
using System.Collections.Generic;


namespace C3.Communi
{
    public class DeviceUI : DeviceUIBase 
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="deviceType"></param>
        /// <param name="station"></param>
        /// <param name="newDevice"></param>
        /// <returns></returns>
        protected override DialogResult OnAdd(Type deviceType, IStation station, out IDevice newDevice)
        {
            return FrmGroups.Add (deviceType, station, out newDevice);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        protected override DialogResult OnEdit(IDevice device)
        {
            return FrmGroups.Edit(device);
        }
    }

}
