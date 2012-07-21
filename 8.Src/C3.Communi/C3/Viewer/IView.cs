using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using C3.Communi;

namespace C3
{
    public class View
    {
        public View(Control parentControl)
        {
            this._ctrl = parentControl;
        }

        protected Control _ctrl;
        public void AddUCViewerToPanel(Control c)
        { 
        }
    }

    public class StationView : View
    {

        public StationView(Control parentControl)
            : base(parentControl)
        {
        }

        #region UCStationViewer
        /// <summary>
        /// 
        /// </summary>
        public UCStationViewer UCStationViewer
        {
            get
            {
                if (_ctrl == null)
                {
                    _ctrl = new UCStationViewer();
                    this.AddUCViewerToPanel(_ctrl);
                }
                return _ctrl as UCStationViewer;
            }
        }
        #endregion //UCStationViewer
    }

    public class DeviceView : View
    {

        public DeviceView(Control parentControl)
            : base(parentControl)
        {
        }
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
    }
    //public class DeviceDataDisplayer
    //{
    //    // properties
    //    //
    //    // 1. IDeviceData 
    //    // 2. device

    //}
}
