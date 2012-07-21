
using System;
using System.Windows.Forms;
using C3.Communi;
using System.Diagnostics;

namespace C3
{
    public class DeviceController: Controller
    {
        public DeviceController(View v)
            : base(v)
        {
        }

        // properties
        //
        // 1. device
        // 2. device data displayer
        // 3. task viewer

        protected override void OnSetModel()
        {
            DeviceView dv = this.View as DeviceView;
            Debug.Assert(dv != null);
            DeviceMode dm = this.Model as DeviceMode;
            Debug.Assert(dm != null);
            dv.UCDeviceViewer.Device = dm.Device;
        }
    }

}
