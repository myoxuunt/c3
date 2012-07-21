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

        /// <summary>
        /// 
        /// </summary>
        protected override void OnSetModel()
        {
            StationView sv = this.View as StationView;
            StationModel sm = this.Model as StationModel;
            sv.UcStationViewer.Station = sm.Station;
        }
    }

}
