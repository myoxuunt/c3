using System;
using System.Windows.Forms;
using Xdgk.Common;

namespace C3.Communi
{
    public class StationTreeNode : TreeNode
    {
        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="station"></param>
        public StationTreeNode(IStation station)
        {
            this.Station = station;
            this.Name = station.Name;
            this.Text = station.Name;

            //
            //
            station.CommuniPortChanged += new EventHandler(station_CommuniPortChanged);

            // 
            //
            //this.ImageKey = IconNames.Disconnect;
            //this.SelectedImageKey = IconNames.Disconnect;
            SetImageKey(IconNames.Disconnect);

            station.Tag = this;

            foreach (IDevice device in station.Devices)
            {
                DeviceTreeNode deviceNode = new DeviceTreeNode(device);
                this.Nodes.Add(deviceNode);
            }
        }
        #endregion //Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void station_CommuniPortChanged(object sender, EventArgs e)
        {
            IStation station = sender as IStation ;
            ICommuniPort cp = station.CommuniPort;

            if (cp != null)
            {
                SetImageKey(IconNames.Connect);
            }
            else
            {
                SetImageKey(IconNames.Disconnect);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        private void SetImageKey(string key)
        {
            if (this.TreeView != null)
            {
                Console.WriteLine(this.TreeView.InvokeRequired);
                if (this.TreeView.InvokeRequired)
                {
                }
                else
                {
                    this.ImageKey = key;
                    this.SelectedImageKey = key;
                }
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
