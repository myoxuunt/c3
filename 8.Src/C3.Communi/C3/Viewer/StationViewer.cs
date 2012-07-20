using System;
using System.Windows.Forms;
using C3.Communi;

namespace C3
{
    public class StationViewer : ViewerBase
    {
        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="panel"></param>
        public StationViewer(Panel panel)
            : base(panel)
        {
        }
        #endregion //Constructor

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
                if (_station != value)
                {
                    _station = value;
                    this.UCStationViewer.Station = _station;
                }
            }
        } private IStation _station;
        #endregion //Station

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

}
