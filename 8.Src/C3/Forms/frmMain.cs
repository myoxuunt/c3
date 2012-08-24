using System;
using System.Diagnostics;
using System.Windows.Forms;
using C3.Communi;
using System.Net;

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
            this.Text = C3App.App.Config.AppName;

            this.sc1.Panel1.Controls.Add(this.HardwareTreeView);
            //this.sc2.Panel2.Controls.Add(this.UCTaskViewer);

            //
            //
            string s = string.Empty;
            foreach (SocketListener item in this.Soft.SocketListenerManager.SocketListeners)
            {
                s += item.LocalEndpoint.ToString() + ", ";
            }
            if (s.Length > 2)
            {
                s = s.Remove(s.Length - 2);
            }

            this.tssListenPort.Text = string.Format("{0}: {1}", Strings.Listening, s);
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

        #region Soft
        private Soft Soft
        {
            get { return this.App.Soft; }
        }
        #endregion //Soft

        #region frmMain_Load
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmMain_Load(object sender, EventArgs e)
        {
            Soft soft = SoftManager.GetSoft();
            object o2 = soft.Hardware;
            int n = soft.Hardware.Stations.Count;

            n = soft.SocketListenerManager.SocketListeners.Count;

            //this.Text = n.ToString();

            // spu
            //
            //this.ucSpus1.SPUs = App.Soft.SPUs;
            //this.ucSpus1.RefreshSPUs();

            this.mnuToolbar.Checked = this.toolStrip1.Visible;
            this.mnuStatusbar.Checked = this.statusStrip1.Visible;
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
            DialogResult dr = NUnit.UiKit.UserMessage.Ask(Strings.SureToQuit);
            if (dr == DialogResult.Yes)
            {
                this.Close();
            }
        }
        #endregion //mnuExit_Click

        #region mnuAbout_Click
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuAbout_Click(object sender, EventArgs e)
        {
            string s = string.Format(
                "{0} v{1}",
                C3App.App.Config.AppName,
                ProductVersion);

            NUnit.UiKit.UserMessage.DisplayInfo(s);
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
            IDevice device = this.GetSelectedDevice(true);
            if (device != null)
            {
                CommuniDetailCollection communiDetails = device.CommuniDetails;
                frmCommuniDetails f = new frmCommuniDetails(device, communiDetails);
                f.ShowDialog(this);
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
            frmGroups f = new frmGroups();
            //GroupCollection gs = new GroupCollection();
            //CommuniPortConfigParameter p = new CommuniPortConfigParameter(
            //    "nonennnnn",
            //    new SerialCommuniPortConfig(
            //    new SerialPortSetting("com1",
            //        9600, System.IO.Ports.Parity.Even,
            //        8, System.IO.Ports.StopBits.None)),
            //        0);
            //Group g = new Group();
            //g.Parameters.Add(p);
            //gs.Add(g);
            //f.Groups = gs;
            //f.ShowDialog();
        }

        #region mnuDeviceEdit_Click
        private void mnuDeviceEdit_Click(object sender, EventArgs e)
        {
            IDevice device = GetSelectedDevice(true);
            if (device!= null)
            {
                IDeviceUI ui = device.Dpu.DeviceUI;
                DialogResult dr = ui.Edit(device);
                if (dr == DialogResult.OK)
                {
                    device.Dpu.DevicePersister.Update(device);

                    DeviceTreeNode deviceNode = (DeviceTreeNode)this.HardwareTreeView.SelectedNode;
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
                NUnit.UiKit.UserMessage.DisplayFailure(Strings.SelectStationFirst);
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
                NUnit.UiKit.UserMessage.DisplayFailure(Strings.SelectDeviceFirst);
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
                    string.Format("not find dpu by device type '{0}'", deviceType));
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
                DialogResult dr = NUnit.UiKit.UserMessage.Ask(Strings.SureToDelete);
                if (dr == DialogResult.Yes)
                {
                    IStation station = selected.Station;
                    station.Devices.Remove(selected);


                    selected.Dpu.DevicePersister.Delete(selected);

                    DeviceTreeNode deviceNode = (DeviceTreeNode)selected.Tag;
                    deviceNode.Remove();

                    //
                    //
                    this.HardwareTreeView.SelectedNode = (StationTreeNode)station.Tag;
                }
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

                StationCollection stations = this.Soft.Hardware.Stations;
                IStation newStation;
                DialogResult dr2 = stationUI.Add(stationType, stations, out newStation);
                if (dr2 == DialogResult.OK)
                {
                    Debug.Assert(newStation.StationType != null);
                    Debug.Assert(newStation.Spu != null);
                    Debug.Assert(newStation.Guid != null);

                    stations.Add(newStation);
                    spu.StationPersister.Add(newStation);

                    StationTreeNode stationNode = new StationTreeNode(newStation);
                    this.HardwareTreeView.Nodes.Add(stationNode);
                }
            }
        }
        #endregion //mnuStationAdd_Click

        #region mnuStationEdit_Click
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
                    station.Spu.StationPersister.Update(station);

                    StationTreeNode stationNode = (StationTreeNode)station.Tag;
                    stationNode.RefreshStationTreeNode();
                }
            }
        }
        #endregion //mnuStationEdit_Click

        #region mnuStationDelete_Click
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuStationDelete_Click(object sender, EventArgs e)
        {
            IStation station = GetSelectedStation(true);
            if (station != null)
            {
                DialogResult dr = NUnit.UiKit.UserMessage.Ask(Strings.SureToDelete);
                if (dr == DialogResult.Yes)
                {
                    station.Spu.StationPersister.Delete(station);
                    StationTreeNode stationNode = station.Tag as StationTreeNode;
                    stationNode.Remove();
                }
            }
        }
        #endregion //mnuStationDelete_Click

        #region mnuToolbar_Click
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuToolbar_Click(object sender, EventArgs e)
        {
            this.mnuToolbar.Checked = !this.mnuToolbar.Checked;
            this.toolStrip1.Visible = this.mnuToolbar.Checked;
        }
        #endregion //mnuToolbar_Click

        #region mnuStatusbar_Click
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuStatusbar_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            item.Checked = !item.Checked;

            this.statusStrip1.Visible = item.Checked;
        }
        #endregion //mnuStatusbar_Click
    }
}
