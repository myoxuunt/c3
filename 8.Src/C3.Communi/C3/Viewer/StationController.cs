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

        //#region Station
        ///// <summary>
        ///// 
        ///// </summary>
        //public IStation Station
        //{
        //    get
        //    {
        //        return _station;
        //    }
        //    set
        //    {
        //        if (_station != value)
        //        {
        //            _station = value;
        //            this.UCStationViewer.Station = _station;
        //        }
        //    }
        //} private IStation _station;
        //#endregion //Station

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
