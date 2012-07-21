using System;
using C3.Communi;

namespace C3
{
    public class StationModel : Model
    {
        public StationModel(IStation station)
        {
            this._station = station;
            this.ControllerType = typeof(StationController);
        }

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
        } private IStation _station;
        #endregion //Station


        public override string Title
        {
            get { return this.Station.Name; }
        }
    }

}
