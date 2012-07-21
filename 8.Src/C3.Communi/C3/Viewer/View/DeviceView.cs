
using System;
using System.Windows.Forms;

namespace C3
{
    public class DeviceView : View
    {

        #region DeviceView
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parentControl"></param>
        public DeviceView(Control parentControl)
            : base(parentControl)
        {
        }
        #endregion //DeviceView

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

        #region ViewControl
        /// <summary>
        /// 
        /// </summary>
        public override Control ViewControl
        {
            get
            {
                if (_ucDeviceViewer == null)
                {
                    _ucDeviceViewer = new UCDeviceViewer();
                    _ucDeviceViewer.Dock = DockStyle.Fill;
                    this.ParentControl.Controls.Add((_ucDeviceViewer));
                }
                return _ucDeviceViewer;
            }
            set { _ucDeviceViewer = (UCDeviceViewer)value; }
        } private UCDeviceViewer _ucDeviceViewer;
        #endregion //ViewControl
    }
}
