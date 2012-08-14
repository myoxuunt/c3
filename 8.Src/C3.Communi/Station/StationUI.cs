using System;
using System.Diagnostics;
using System.Windows.Forms;
using Xdgk.Common;


namespace C3.Communi
{
    public class StationUI : IStationUI
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="spu"></param>
        public StationUI(ISPU spu)
        {
            this.Spu = spu;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stationType"></param>
        /// <param name="newStation"></param>
        /// <returns></returns>
        public DialogResult Add(StationType stationType, StationCollection stations, out IStation newStation)
        {
            newStation = null;

            Debug.Assert(stationType != null);
            Debug.Assert(stations != null);

            FrmStationGroups f = new FrmStationGroups();
            f.AdeStatus = ADEStatus.Add;
            f.StationType = stationType;
            f.Stations = stations;
            f.Station = stationType.Create(this.Spu);
            f.Groups = f.Station.Groups;
            DialogResult dr = f.ShowDialog();
            if (dr == DialogResult.OK)
            {
                newStation = f.Station;
            }
            return dr;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="station"></param>
        /// <returns></returns>
        public DialogResult Edit(IStation station)
        {
            if (station == null)
            {
                throw new ArgumentNullException("station");
            }

            FrmStationGroups f = new FrmStationGroups();
            f.Station = station;
            f.Stations = station.Stations;
            f.AdeStatus = ADEStatus.Edit;
            f.StationType = station.StationType;
            f.Groups = station.Groups;

            return f.ShowDialog();
        }

        #region Spu
        /// <summary>
        /// 
        /// </summary>
        public ISPU Spu
        {
            get
            {
                return _spu;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Spu");
                }
                _spu = value;
            }
        } private ISPU _spu;

        #endregion //Spu
    }

}
