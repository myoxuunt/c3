using System;
using System.Diagnostics;
using System.Windows.Forms;
using C3.Communi;
using C3.Resources;
using NLog;

namespace C3
{
    public partial class UCDeviceViewer : UserControl
    {
        /// <summary>
        /// 
        /// </summary>
        static private Logger _log = LogManager.GetCurrentClassLogger();

        #region UCDeviceViewer
        public UCDeviceViewer()
        {
            InitializeComponent();

            InitListView();
        }
        #endregion //UCDeviceViewer

        #region InitListView
        /// <summary>
        /// 
        /// </summary>
        private void InitListView()
        {
            // device data
            //
            this.chNO.Text = Resources.DeviceDataResource.NO;
            this.chName.Text = DeviceDataResource.Name;
            this.chValue.Text = DeviceDataResource.Value;
            this.chUnit.Text = DeviceDataResource.Unit;

            this.chNO.Width = int.Parse(Resources.DeviceDataResource.NOWidth);
            this.chName.Width = int.Parse(DeviceDataResource.NameWidth);
            this.chValue.Width = int.Parse(DeviceDataResource.ValueWidth);
            this.chUnit.Width = int.Parse(DeviceDataResource.UnitWidth);


            // task
            //
            this.chOperaName.Text = TaskResource.Name;
            this.chLastExecute.Text = TaskResource.LastExecute;
            this.chStrategy.Text = TaskResource.Strategy;
            this.chStatus.Text = TaskResource.Status;

            this.chOperaName.Width = int.Parse(TaskResource.NameWidth);
            this.chLastExecute.Width = int.Parse(TaskResource.LastExecuteWidth);
            this.chStrategy.Width = int.Parse(TaskResource.StrategyWidth);
            this.chStatus.Width = int.Parse(TaskResource.StatusWidth);

        }
        #endregion //InitListView

        #region Device
        /// <summary>
        /// 
        /// </summary>
        public IDevice Device
        {
            get
            {
                return _device;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Device");
                }

                if (_device != value)
                {
                    if (_device != null)
                    {
                        UnregisterEvents(_device);
                    }

                    _device = value;

                    FillTaskListView();
                    FillDeviceLastData();

                    //
                    //
                    RegisterEvents(_device);
                }
            }
        } private IDevice _device;
        #endregion //Device

        #region RegisterEvents
        /// <summary>
        /// 
        /// </summary>
        private void RegisterEvents(IDevice device)
        {
            DeviceDataManager dataMan = device.DeviceDataManager;
            dataMan.LastDataChanged += new EventHandler(device_LastDataChanged);

            //
            //
            TaskManager taskMan = device.TaskManager;
            taskMan.CurrentChanged += new EventHandler(taskMan_CurrentChanged);
            taskMan.CurrentStatusChanged += new EventHandler(taskMan_CurrentStatusChanged);
        }
        #endregion //RegisterEvents

        #region taskMan_CurrentStatusChanged
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void taskMan_CurrentStatusChanged(object sender, EventArgs e)
        {
            TaskManager taskMan = sender as TaskManager;
            ITask current = taskMan.Current;

            UpdateTaskListViewItem(current);
        }
        #endregion //taskMan_CurrentStatusChanged

        #region UpdateTaskListViewItem
        /// <summary>
        /// 
        /// </summary>
        /// <param name="current"></param>
        private void UpdateTaskListViewItem(ITask current)
        {
            ListViewItem lvi = FindTaskListViewItem(current);
            Debug.Assert(lvi != null);

            lvi.SubItems[lvTask.Columns.Count - 1].Text = current.Status.ToString();
        }
        #endregion //UpdateTaskListViewItem

        #region ListViewItem
        /// <summary>
        /// 
        /// </summary>
        /// <param name="current"></param>
        /// <returns></returns>
        private ListViewItem FindTaskListViewItem(ITask current)
        {
            ListViewItem lvi = null;
            foreach (ListViewItem item in this.lvTask.Items)
            {
                if (item.Tag == current)
                {
                    lvi = item;
                    break;
                }
            }
            return lvi;
        }
        #endregion //ListViewItem

        #region taskMan_CurrentChanged
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void taskMan_CurrentChanged(object sender, EventArgs e)
        {
            TaskManager taskMan = sender as TaskManager;
            ITask current = taskMan.Current;
            FillTaskListView();
        }
        #endregion //taskMan_CurrentChanged

        #region UnregisterEvents
        /// <summary>
        /// 
        /// </summary>
        /// <param name="device"></param>
        private void UnregisterEvents(IDevice device)
        {
            DeviceDataManager man = device.DeviceDataManager;
            man.LastDataChanged -= device_LastDataChanged;

            //
            //
            TaskManager taskMan = device.TaskManager;
            taskMan.CurrentChanged -= new EventHandler(taskMan_CurrentChanged);
            taskMan.CurrentStatusChanged -= new EventHandler(taskMan_CurrentStatusChanged);
        }
        #endregion //UnregisterEvents

        #region device_LastDataChanged
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void device_LastDataChanged(object sender, EventArgs e)
        {
            this.FillDeviceLastData();
        }
        #endregion //device_LastDataChanged

        #region FillDeviceLastData
        /// <summary>
        /// 
        /// </summary>
        private void FillDeviceLastData()
        {
            if (this.Device != null)
            {
                int selectedIdx = ListViewHelper.GetSelectedIndex(this.lvDeviceDataLast);

                this.lvDeviceDataLast.Items.Clear();

                IDeviceData lastData = Device.DeviceDataManager.Last;
                if (lastData != null)
                {
                    ReportItemCollection reportItems = lastData.GetReportItems();
                    for (int i = 0; i < reportItems.Count; i++)
                    {
                        ReportItem item = reportItems[i];
                        int no = i + 1;
                        ListViewItem lvi = CreateListViewItem(no, item);
                        this.lvDeviceDataLast.Items.Add(lvi);
                    }

                    ListViewHelper.SetSelectedIndex(this.lvDeviceDataLast, selectedIdx);
                }
            }
        }
        #endregion //FillDeviceLastData

        #region CreateListViewItem
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private ListViewItem CreateListViewItem(int no, ReportItem item)
        {
            string[] items = new string[] { no.ToString(), item.Name, item.FormatedValue, item.Unit.Text };

            ListViewItem lvi = new ListViewItem(items);
            lvi.Tag = item;
            return lvi;
        }
        #endregion //CreateListViewItem

        #region FillTaskListView
        /// <summary>
        /// 
        /// </summary>
        private void FillTaskListView()
        {
            this.lvTask.Items.Clear();

            if (_device != null)
            {
                ITask current = _device.TaskManager.Current;
                if (current != null)
                {
                    ListViewItem taskLvi = CreateTaskListViewItem(current);
                    taskLvi.Text += "(c)";
                    this.lvTask.Items.Add(taskLvi);
                }

                foreach (ITask task in _device.TaskManager.Tasks.ToArray())
                {
                    ListViewItem lvi = CreateTaskListViewItem(task);
                    this.lvTask.Items.Add(lvi);
                }
            }
        }
        #endregion //FillTaskListView

        #region CreateTaskListViewItem
        /// <summary>
        /// 
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        private ListViewItem CreateTaskListViewItem(ITask task)
        {
            string[] items = new string[] { task.Opera.Name, task.LastExecute.ToString (), 
                "-",
                task.Status.ToString() };
            ListViewItem lvi = new ListViewItem(items);
            lvi.Tag = task;
            return lvi;
        }
        #endregion //CreateTaskListViewItem
    }

}
