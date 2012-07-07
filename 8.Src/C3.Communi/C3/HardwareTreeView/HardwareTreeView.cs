using System;
using System.Drawing;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using C3.Communi;

namespace C3
{
    public class HardwareTreeView : TreeView
    {
        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        public HardwareTreeView()
        {
            ImageList ml = new ImageList();

            ml.Images.Add(IconNames.Empty, Icons.Empty);
            ml.Images.Add(IconNames.Connect, Icons.Connect);
            ml.Images.Add(IconNames.Disconnect, Icons.Disconnect);
            ml.Images.Add(IconNames.Device, Icons.Device);

            this.ImageList = ml;
        }
        #endregion //Constructor

        #region Hardware
        /// <summary>
        /// 
        /// </summary>
        public Hardware Hardware
        {
            get
            {
                return _hardware;
            }
            set
            {
                _hardware = value;
            }
        } private Hardware _hardware;
        #endregion //Hardware

        #region Bind
        /// <summary>
        /// 
        /// </summary>
        public void Bind()
        {
            foreach (IStation station in Hardware.Stations)
            {
                TreeNode stationNode = new StationTreeNode(station);
                this.Nodes.Add(stationNode);
            }
        }

        void a()
        {
            //this.Invoke (
        }
        #endregion //Bind
    }
}
