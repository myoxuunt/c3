using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using C3.Communi;

namespace C3
{
    public partial class UCDeviceViewer : UserControl
    {

        #region UCDeviceViewer
        public UCDeviceViewer()
        {
            InitializeComponent();
        }
        #endregion //UCDeviceViewer

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


                    this.richTextBox1.Text = DateTime.Now.ToString() + _device.ToString();

                    FillTaskListView();
                    FillDeviceLastData();

                    //
                    //
                    RegisterEvents(_device);
                }
            }
        } private IDevice _device;
        #endregion //Device

        #region UnregisterEvents
        /// <summary>
        /// 
        /// </summary>
        /// <param name="device"></param>
        private void UnregisterEvents(IDevice device)
        {
            device.LastDataChanged -= new EventHandler(device_LastDataChanged);
        }
        #endregion //UnregisterEvents

        #region RegisterEvents
        /// <summary>
        /// 
        /// </summary>
        private void RegisterEvents(IDevice device)
        {
            device.LastDataChanged += new EventHandler(device_LastDataChanged);
        }
        #endregion //RegisterEvents

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

                IDeviceData lastData = Device.LastData;
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
            this.listView1.Items.Clear();

            if (_device != null)
            {
                ITask current = _device.CurrentTask;
                if (current != null)
                {
                    ListViewItem taskLvi = CreateTaskListViewItem(current);
                    this.listView1.Items.Add(taskLvi);
                }

                foreach (ITask task in _device.Tasks.ToArray())
                {
                    ListViewItem lvi = CreateTaskListViewItem(task);
                    this.listView1.Items.Add(lvi);
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
            return lvi;
        }
        #endregion //CreateTaskListViewItem
    }

}
