
using System;
using System.Windows.Forms;
using C3.Communi;

namespace C3
{
    public class DeviceViewer : ViewerBase
    {
        public DeviceViewer(Panel panel)
            : base(panel)
        {
        }

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
                if (_device != value)
                {
                    _device = value;
                    this.UCDeviceViewer.Device = _device;
                }
            }
        } private IDevice _device;
        #endregion //Device


        #region UCDeviceViewer
        /// <summary>
        /// 
        /// </summary>
        public UCDeviceViewer UCDeviceViewer
        {
            get
            {
                if (_ctrl == null)
                {
                    _ctrl = new UCDeviceViewer();
                    this.AddUCViewerToPanel(_ctrl);
                }
                return _ctrl as UCDeviceViewer;
            }
        }
        // private UCDeviceViewer _uCDeviceViewer;
        #endregion //UCDeviceViewer
        // properties
        //
        // 1. device
        // 2. device data displayer
        // 3. task viewer
    }

}
