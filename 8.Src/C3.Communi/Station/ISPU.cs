using System;
using System.Diagnostics;
using System.Windows.Forms;
using Xdgk.Common;

namespace C3.Communi
{
    /// <summary>
    /// 
    /// </summary>
    public interface IStationUI
    {
        DialogResult Add(StationType stationType, StationCollection stations, out IStation newStation);
        DialogResult Edit(IStation station);
    }

    public class StationUI : IStationUI
    {
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
            f.Station = stationType.Create();
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

    }

    public interface ISPU
    {
        string Name { get; set; }
        string Description { get; set; }
        StationType StationType {get;set;}
        IStationFactory StationFactory { get; set; }
        IStationPersister StationPersister { get; set; }
        IStationSourceProvider StationSourceProvider { get; set; }
        IStationUI StationUI { get; set; }
    }

}
