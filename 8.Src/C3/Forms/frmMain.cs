﻿using System;
using System.Windows.Forms;
using C3.Communi;

namespace C3
{
    public partial class FrmMain : Form
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


        public FrmMain()
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
            //this.sc2.Panel2.Controls.Add(this.UCTaskViewer);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void t_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode node = e.Node;
            Model model = null;

            if (node is DeviceTreeNode)
            {
                DeviceTreeNode deviceNode = node as DeviceTreeNode;
                IDevice device = deviceNode.Device;
                //this.UCTaskViewer.Device = deviceNode.Device;
                //this.ViewerWrapper.View(device);
                model = new DeviceMode(device);
            }
            else if (node is StationTreeNode)
            {
                StationTreeNode stationNode = node as StationTreeNode;
                IStation station = stationNode.Station;
                //this.ViewerWrapper.View(station);
                model = new StationModel(station);
            }

            this.ControllerManager.Act(model);
        }

        #region ControllerManager
        /// <summary>
        /// 
        /// </summary>
        public ControllerManager ControllerManager
        {
            get
            {
                if (_controllerManager == null)
                {
                    _controllerManager = new ControllerManager();
                    this.sc1.Panel2.Controls.Add(_controllerManager.UcViewerWrapper);
                }
                return _controllerManager;
            }
            set
            {
                _controllerManager = value;
            }
        } private ControllerManager _controllerManager;
        #endregion //ControllerManager



        //public ViewerWrapper ViewerWrapper
        //{
        //    get
        //    {
        //        if (_viewerWrapper == null)
        //        {
        //            _viewerWrapper = new ViewerWrapper();
        //            this.sc1.Panel2.Controls.Add(_viewerWrapper.UCViewerWrapper);
        //        }
        //        return _viewerWrapper;
        //    }
        //} private ViewerWrapper _viewerWrapper;

        /// <summary>
        /// 
        /// </summary>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuTest_Click(object sender, EventArgs e)
        {
            Test();
        }

        /// <summary>
        /// 
        /// </summary>
        private void Test()
        {
            //string s = Soft.Hardware.Stations[0].Devices[0].ToString();
        }

        private void mnuDeviceEdit_Click(object sender, EventArgs e)
        {
            DeviceTreeNode deviceNode = this._hardwareTreeView.SelectedNode as DeviceTreeNode;
            if (deviceNode != null)
            {
                IDevice d = deviceNode.Device;
                IDeviceUI ui = d.Dpu.DeviceUI;
                DialogResult dr = ui.Edit(d);
                if (dr == DialogResult.OK)
                {
                    deviceNode.RefreshDeviceTreeNode();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private StationTreeNode GetSelectedStationTreeNode()
        {
            StationTreeNode stationTreeNode = null;
            TreeNode node = this._hardwareTreeView.SelectedNode;
            if (node is StationTreeNode)
            {
                stationTreeNode = (StationTreeNode)node;
            }
            return stationTreeNode;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private IStation GetSelectedStation(bool showNotSelectedMsg)
        {
            IStation r = null;
            TreeNode node = this._hardwareTreeView.SelectedNode;
            if (node is StationTreeNode)
            {
                StationTreeNode stationNode = (StationTreeNode)node;
                r = stationNode.Station;
            }
            if (r == null && showNotSelectedMsg)
            {
                NUnit.UiKit.UserMessage.DisplayFailure("selected station first");
            }
            return r;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuDeviceAdd_Click(object sender, EventArgs e)
        {
            IStation selectedStation = this.GetSelectedStation(true);
            if (selectedStation != null)
            {
                frmDeviceType f = new frmDeviceType();
                if (f.ShowDialog() == DialogResult.OK)
                {
                    DeviceType deviceType = f.SelectedDeviceType;

                    IDPU dpu = Soft.DPUs[deviceType];

                    IDeviceUI deviceUI = GetDeviceUI(deviceType);
                    IDevice newDevice;
                    DialogResult dr = deviceUI.Add(deviceType, selectedStation, out newDevice);
                    if (dr == DialogResult.OK)
                    {
                        selectedStation.Devices.Add(newDevice);
                        newDevice.Station = selectedStation;

                        // 
                        //
                        newDevice.Dpu = Soft.DPUs[deviceType];

                        // persister device
                        //
                        dpu.DevicePersister.Add(newDevice);

                        // task device
                        //
                        dpu.TaskFactory.Create(newDevice);

                        // add device tree node
                        //
                        DeviceTreeNode deviceTreeNode = new DeviceTreeNode(newDevice);
                        this.GetSelectedStationTreeNode().Nodes.Add(deviceTreeNode);

                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private IDeviceUI GetDeviceUI(DeviceType deviceType)
        {
            IDPU dpu = this.Soft.DPUs[deviceType];
            if (dpu == null)
            {
                throw new InvalidOperationException(
                    string.Format ("not find dpu by device type '{0}'", deviceType ));
            }
            return dpu.DeviceUI;
        }
    }


}
