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
            this.InitializeComponent();
            ImageList ml = new ImageList();

            ml.Images.Add(IconNames.Empty, Icons.Empty);
            ml.Images.Add(IconNames.Connect, Icons.Connect);
            ml.Images.Add(IconNames.Disconnect, Icons.Disconnect);
            ml.Images.Add(IconNames.Device, Icons.Device);

            this.ImageList = ml;
        }
        #endregion //Constructor

        private System.ComponentModel.IContainer components;
        private ContextMenuStrip cmsTreeView;
        private ToolStripMenuItem toolStripMenuItem1;

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
                if (_hardware != value)
                {
                    _hardware = value;
                    if (_hardware != null)
                    {
                        Bind();
                    }
                }
            }
        } private Hardware _hardware;
        #endregion //Hardware

        #region Bind
        /// <summary>
        /// 
        /// </summary>
        private void Bind()
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


        #region InitializeComponent
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.cmsTreeView = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsTreeView.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmsTreeView
            // 
            this.cmsTreeView.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.cmsTreeView.Name = "cmsTreeView";
            this.cmsTreeView.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.cmsTreeView.Size = new System.Drawing.Size(169, 26);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(168, 22);
            this.toolStripMenuItem1.Text = "toolStripMenuItem1";
            // 
            // HardwareTreeView
            // 
            this.LineColor = System.Drawing.Color.Black;
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.HardwareTreeView_MouseUp);
            this.cmsTreeView.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion //InitializeComponent

        #region DeviceContextMenus
        /// <summary>
        /// 
        /// </summary>
        public ToolStripItem[] DeviceContextMenus
        {
            get
            {
                return _deviceContextMenus;
            }
            set
            {

                _deviceContextMenus = value;
            }
        } private ToolStripItem[] _deviceContextMenus;
        #endregion //DeviceContextMenus

        #region StationContextMenus
        /// <summary>
        /// 
        /// </summary>
        public ToolStripItem[] StationContextMenus
        {
            get { return _stationContextMenus; }
            set { _stationContextMenus = value; }
        } private ToolStripItem[] _stationContextMenus;
        #endregion //StationContextMenus

        #region HardwareTreeView_MouseUp
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HardwareTreeView_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                TreeNode node = this.GetNodeAt(e.X, e.Y);
                if (node != null)
                {
                    //node.IsSelected = true;
                    this.SelectedNode = node;
                    if (node is StationTreeNode)
                    {
                        this.cmsTreeView.Items.Clear();
                        //this.cmsTreeView.Items.Add ( this.mnu
                        this.AddMenu(this.StationContextMenus);
                    }
                    else if (node is DeviceTreeNode)
                    {
                        this.AddMenu(this.DeviceContextMenus);
                    }
                    else
                    {
                    }
                    Console.WriteLine(node.Text);
                    this.cmsTreeView.Show(this, e.X, e.Y);
                }
            }
        }
        #endregion //HardwareTreeView_MouseUp

        #region AddMenu
        /// <summary>
        /// 
        /// </summary>
        /// <param name="menus"></param>
        private void AddMenu(ToolStripItem[] menus)
        {
            this.cmsTreeView.Items.Clear();

            foreach (ToolStripItem item in menus)
            {
                this.cmsTreeView.Items.Add(item);
            }
        }
        #endregion //AddMenu
    }
}
