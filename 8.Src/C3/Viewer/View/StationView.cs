using System;
using System.Windows.Forms;

namespace C3
{
    public class StationView : View
    {
        #region Constuctor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parentControl"></param>
        public StationView(Control parentControl)
            : base(parentControl)
        {
        }
        #endregion //Constructor

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

        #region ViewControl
        /// <summary>
        /// 
        /// </summary>
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
        } private Control _viewControl;
        #endregion //ViewControl
    }

}
