using System;
using System.Windows.Forms;
using C3.Communi;

namespace C3
{
    public class StationController : Controller
    {
        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="panel"></param>
        public StationController(StationView view )
            : base(view)
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
                //if (_ctrl == null)
                //{
                //    _ctrl = new UCStationViewer();
                //    this.AddUCViewerToPanel(_ctrl);
                //}
                //return _ctrl as UCStationViewer;
                return null;
            }
        }
        #endregion //UCStationViewer

        protected override void OnSetModel()
        {
            StationView sv = this.View as StationView;
            StationModel sm = this.Model as StationModel;
            sv.UCStationViewer.Station = sm.Station;
        }
    }

}
