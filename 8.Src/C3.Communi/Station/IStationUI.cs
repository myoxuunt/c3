
using System;
using System.Diagnostics;
using System.Windows.Forms;
using Xdgk.Common;


namespace C3.Communi
{
    public interface IStationUI
    {
        ISPU Spu { get; set; }
        DialogResult Add(StationType stationType, StationCollection stations, out IStation newStation);
        DialogResult Edit(IStation station);
    }

}
