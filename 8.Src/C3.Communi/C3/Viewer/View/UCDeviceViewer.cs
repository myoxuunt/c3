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
                if (_device != value)
                {
                    _device = value;
                    this.richTextBox1.Text = DateTime.Now.ToString() + _device.ToString();
                    FillTaskListView();
                    FillDeviceLastData();
                }
            }
        } private IDevice _device;
        #endregion //Device

        #region FillDeviceLastData
        /// <summary>
        /// 
        /// </summary>
        private void FillDeviceLastData()
        {
            if (this.Device != null)
            {
                this.lvDeviceDataLast.Items.Clear();

                IDeviceData lastData = Device.LastData;
                if (lastData != null)
                {
                    ReportItemCollection reportItems = lastData.GetReportItems();
                    for (int i = 0; i < reportItems.Count; i++)
                    {
                        ReportItem item = reportItems[i];
                        ListViewItem lvi = CreateListViewItem(i + 1,item);
                        this.lvDeviceDataLast.Items.Add(lvi);
                    }
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
