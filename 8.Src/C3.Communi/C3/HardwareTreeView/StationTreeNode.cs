using System;
using System.Windows.Forms;
using Xdgk.Common;

namespace C3.Communi
{
    public class StationTreeNode : TreeNode
    {
        public StationTreeNode(IStation station)
        {

            this.Station = station;
            this.Name = station.Name;
            this.Text = station.Name;

            this.ImageKey = IconNames.Disconnect;
            this.SelectedImageKey = IconNames.Disconnect;

            station.Tag = this;

            foreach (IDevice device in station.Devices)
            {
                DeviceTreeNode deviceNode = new DeviceTreeNode(device);
                this.Nodes.Add(deviceNode);
            }
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
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("station");
                }
                _station = value;
            }
        } private IStation _station;
        #endregion //Station

    }

}
