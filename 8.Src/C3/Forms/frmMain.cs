using System;
using System.Windows.Forms;
using C3.Communi;

namespace C3
{
    public partial class FrmMain : Form
    {

        #region HardwareTreeView
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
        #endregion //HardwareTreeView


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


        #region FrmMain
        public FrmMain()
        {
            InitializeComponent();
            Init();
        }
        #endregion //FrmMain

        #region Init
        /// <summary>
        /// 
        /// </summary>
        private void Init()
        {
            this.sc1.Panel1.Controls.Add(this.HardwareTreeView);
            //this.sc2.Panel2.Controls.Add(this.UCTaskViewer);

        }
        #endregion //Init

        #region t_AfterSelect
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
        #endregion //t_AfterSelect

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

        #region App
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
        #endregion //App

        private Soft Soft
        {
            get { return this.App.Soft; }
        }

        #region frmMain_Load
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
        #endregion //frmMain_Load

        #region mnuExit_Click
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion //mnuExit_Click

        #region mnuAbout_Click
        private void mnuAbout_Click(object sender, EventArgs e)
        {
            frmHardware f = new frmHardware(C3App.App.Soft.Hardware.Stations[0]);
            f.ShowDialog();
            //this.treeView1 
        }
        #endregion //mnuAbout_Click

        #region mnuCommuniDetail_Click
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
        #endregion //mnuCommuniDetail_Click

        #region mnuM_Click
        private void mnuM_Click(object sender, EventArgs e)
        {
            frmM f = new frmM();
            DialogResult dr = f.ShowDialog(this);
        }
        #endregion //mnuM_Click

        #region mnuTaskView_Click
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
        #endregion //mnuTaskView_Click

        #region mnuTest_Click
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuTest_Click(object sender, EventArgs e)
        {
            Test();
        }
        #endregion //mnuTest_Click

        /// <summary>
        /// 
        /// </summary>
        private void Test()
        {
            //string s = Soft.Hardware.Stations[0].Devices[0].ToString();
        }

        #region mnuDeviceEdit_Click
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
        #endregion //mnuDeviceEdit_Click

        #region GetSelectedStationTreeNode
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
        #endregion //GetSelectedStationTreeNode

        #region GetSelectedStation
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
        #endregion //GetSelectedStation

        #region GetSelectedDevice
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private IDevice GetSelectedDevice(bool showNotSelectedMsg)
        {
            IDevice r = null;
            TreeNode node = this._hardwareTreeView.SelectedNode;
            if (node is DeviceTreeNode)
            {
                DeviceTreeNode deviceNode = (DeviceTreeNode)node;
                r = deviceNode.Device;
            }
            if (r == null && showNotSelectedMsg)
            {
                NUnit.UiKit.UserMessage.DisplayFailure("selected device first");
            }
            return r;
        }
        #endregion //GetSelectedDevice

        #region mnuDeviceAdd_Click
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
        #endregion //mnuDeviceAdd_Click

        #region GetDeviceUI
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
        #endregion //GetDeviceUI

        #region mnuDeviceDelete_Click
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuDeviceDelete_Click(object sender, EventArgs e)
        {
            IDevice selected = GetSelectedDevice(true);
            if (selected != null)
            {
                IStation station = selected.Station;
                station.Devices.Remove(selected);


                DeviceTreeNode deviceNode = (DeviceTreeNode)selected.Tag;
                deviceNode.Remove();

                //
                //
                this.HardwareTreeView.SelectedNode = (StationTreeNode)station.Tag;
            }
        }
        #endregion //mnuDeviceDelete_Click

        #region mnuStationAdd_Click
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuStationAdd_Click(object sender, EventArgs e)
        {
            frmStationType f = new frmStationType();
            DialogResult dr = f.ShowDialog();
            if (dr == DialogResult.OK)
            {
                StationType stationType = f.SelectedStationType;
                ISPU spu = Soft.SPUs[stationType];
                IStationUI stationUI = spu.StationUI;

                StationCollection stations = null;
                IStation newStation;
                DialogResult dr2 = stationUI.Add(stationType, stations, out newStation);
                if (dr2 == DialogResult.OK)
                {
                    stations.Add(newStation);

                    StationTreeNode stationNode = new StationTreeNode(newStation);
                    this.HardwareTreeView.Nodes.Add(stationNode);
                }
            }
        }
        #endregion //mnuStationAdd_Click

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuStationEdit_Click(object sender, EventArgs e)
        {
            IStation station = GetSelectedStation(true);
            if (station != null)
            {
                IStationUI stationUI = station.Spu.StationUI;
                DialogResult dr = stationUI.Edit(station);
                if (dr == DialogResult.OK)
                {
                    StationTreeNode stationNode = (StationTreeNode)station.Tag;
                    stationNode.RefreshStationTreeNode();
                }
            }
        }
    }
}
