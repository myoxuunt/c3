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
        public UCDeviceViewer()
        {
            InitializeComponent();
        }

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
                }
            }
        } private IDevice _device;
        #endregion //Device

        //private void frmTask_Load(object sender, EventArgs e)
        //{
        //    FillTaskListView();
        //}

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
    }
}
