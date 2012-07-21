using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using C3.Communi;

namespace C3
{
    abstract  public class View
    {
        protected View(Control parentControl)
        {
            this._parentControl = parentControl;
        }

        public Control ParentControl
        {
            get { return _parentControl; }
        }

        private Control _parentControl;


        abstract public Control ViewControl { get; set; } 
    }

    public class StationView : View
    {

        public StationView(Control parentControl): base(parentControl)
        {
        }

        #region UCStationViewer
        /// <summary>
        /// 
        /// </summary>
        public UCStationViewer UcStationViewer
        {
            get
            {
                return ViewControl as UCStationViewer;
            }
        }
        #endregion //UCStationViewer

        public override Control ViewControl
        {
            get
            {
                if (_viewControl == null)
                {
                    _viewControl = new UCStationViewer();
                    _viewControl.Dock = DockStyle.Fill;
                    this.ParentControl.Controls.Add(_viewControl);
                }
                return _viewControl;
            }
            set { _viewControl = value; }
        }

        private Control _viewControl;
    }

    public class DeviceView : View
    {

        public DeviceView(Control parentControl)
            :base(parentControl)
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
                return this.ViewControl as UCDeviceViewer;
            }
        }
        // private UCDeviceViewer _uCDeviceViewer;
        #endregion //UCDeviceViewer


        public override Control ViewControl
        {
            get
            {
                if (_ucDeviceViewer==null   )
                {
                    _ucDeviceViewer = new UCDeviceViewer();
                    _ucDeviceViewer.Dock = DockStyle.Fill;
                    this.ParentControl.Controls.Add((_ucDeviceViewer));
                }
                return _ucDeviceViewer;
            }
            set { _ucDeviceViewer = (UCDeviceViewer)value; }
        }

        private UCDeviceViewer _ucDeviceViewer;
    }
    //public class DeviceDataDisplayer
    //{
    //    // properties
    //    //
    //    // 1. IDeviceData 
    //    // 2. device

    //}
}
