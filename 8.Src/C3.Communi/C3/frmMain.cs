using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using C3.Communi;

namespace C3
{
    public partial class frmMain : Form
    {

        private HardwareTreeView HardwareTreeView
        {
            get
            {
                if (_hardwareTreeView == null)
                {
                    _hardwareTreeView = new HardwareTreeView();
                    _hardwareTreeView.Hardware = App.Soft.Hardware;
                    _hardwareTreeView.Dock = DockStyle.Fill;
                    _hardwareTreeView.AfterSelect += new TreeViewEventHandler(t_AfterSelect);
                }
                return _hardwareTreeView;
            }
        } private HardwareTreeView _hardwareTreeView;


        #region UCTaskViewer
        /// <summary>
        /// 
        /// </summary>
        public UCTaskViewer UCTaskViewer
        {
            get
            {
                if (_uCTaskViewer == null)
                {
                    _uCTaskViewer = new UCTaskViewer();
                    _uCTaskViewer.Dock = DockStyle.Fill;
                }
                return _uCTaskViewer;
            }
        } private UCTaskViewer _uCTaskViewer;
        #endregion //UCTaskViewer


        public frmMain()
        {
            InitializeComponent();
            Init();
        }
            
        /// <summary>
        /// 
        /// </summary>
        private void Init()
        {
            this.sc1.Panel1.Controls.Add(this.HardwareTreeView);
            this.sc2.Panel2.Controls.Add(this.UCTaskViewer);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void t_AfterSelect(object sender, TreeViewEventArgs e)
        {
            DeviceTreeNode deviceNode = e.Node as DeviceTreeNode;
            if (deviceNode != null)
            {
                this.UCTaskViewer.Device = deviceNode.Device;
            }
        }

        private C3App App
        {
            get
            {
                return C3App.App;
            }
        }

        private Soft Soft
        {
            get { return this.App.Soft; }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            Soft soft = SoftManager.GetSoft();
            object o2 = soft.Hardware;
            int n = soft.Hardware.Stations.Count;

            n = soft.SocketListenerManager.SocketListeners.Count;

            this.Text = n.ToString();

            // spu
            //
            //this.ucSpus1.SPUs = App.Soft.SPUs;
            //this.ucSpus1.RefreshSPUs();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void mnuAbout_Click(object sender, EventArgs e)
        {
            frmHardware f = new frmHardware(C3App.App.Soft.Hardware.Stations[0]);
            f.ShowDialog();
            //this.treeView1 
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuCommuniDetail_Click(object sender, EventArgs e)
        {
            DeviceTreeNode deviceNode = this._hardwareTreeView.SelectedNode as DeviceTreeNode;
            if (deviceNode != null)
            {
                CommuniDetailCollection communiDetails = deviceNode.Device.CommuniDetails;
                frmCommuniDetails f = new frmCommuniDetails(deviceNode.Device, communiDetails);
                f.ShowDialog(this);

            }
            else
            {
                NUnit.UiKit.UserMessage.DisplayInfo("select device node");
            }
        }

        private void mnuM_Click(object sender, EventArgs e)
        {
            frmM f = new frmM();
            DialogResult dr = f.ShowDialog(this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuTaskView_Click(object sender, EventArgs e)
        {
            DeviceTreeNode deviceNode = this._hardwareTreeView.SelectedNode as DeviceTreeNode;
            if (deviceNode != null)
            {
                //UCTaskViewer f = new UCTaskViewer(deviceNode.Device);
                //f.ShowDialog(this);
            }
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void splitContainer2_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }
    }
}
