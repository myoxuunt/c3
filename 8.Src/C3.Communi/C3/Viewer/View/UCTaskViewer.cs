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
    public partial class UCTaskViewer : UserControl 
    {
        public UCTaskViewer()
        {
            InitializeComponent();
        }

        public IDevice Device
        {
            get { return _device; }
            set 
            {
                if (_device != value)
                {
                    _device = value;
                    FillTaskListView();
                }
            }
        }
        private IDevice _device;

        private void frmTask_Load(object sender, EventArgs e)
        {
            FillTaskListView();
        }

        /// <summary>
        /// 
        /// </summary>
        private void FillTaskListView()
        {
            this.listView1.Items.Clear();

            if (_device != null)
            {
                ITask current = _device.TaskManager.Current;
                if (current != null)
                {
                    ListViewItem taskLvi = CreateTaskListViewItem(current);
                    this.listView1.Items.Add(taskLvi);
                }

                foreach (ITask task in _device.TaskManager.Tasks.ToArray())
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
            ListViewItem lvi= new ListViewItem(items);
            return lvi;
        }
    }
}
