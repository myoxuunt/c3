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

        #region UCTaskViewer
        /// <summary>
        /// 
        /// </summary>
        public UCTaskViewer()
        {
            InitializeComponent();
        }
        #endregion //UCTaskViewer

        #region Device
        /// <summary>
        /// 
        /// </summary>
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
        } private IDevice _device;
        #endregion //Device

        #region frmTask_Load
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmTask_Load(object sender, EventArgs e)
        {
            FillTaskListView();
        }
        #endregion //frmTask_Load

        #region FillTaskListView
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
            ListViewItem lvi= new ListViewItem(items);
            lvi.Tag = task;
            return lvi;
        }
        #endregion //CreateTaskListViewItem

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmsTaskItem_Opening(object sender, CancelEventArgs e)
        {
            Console.WriteLine("taskItem opening");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuRunTask_Click(object sender, EventArgs e)
        {
            ITask sel = GetSelectedTask();
            if (sel != null)
            {

            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private itask getselectedtask()
        {
            itask r = null;
            if (this.listview1.selecteditems.count > 0)
            {
                r = (itask)this.listview1.selecteditems[0].tag;
            }
            return r;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuTaskDetail_Click(object sender, EventArgs e)
        {

        }
    }
}
