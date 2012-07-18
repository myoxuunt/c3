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
        public UCTaskViewer(IDevice device)
        {
            InitializeComponent();
            this._device = device;
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
            ITask current = _device.CurrentTask ;
            if (current!= null)
            {
                ListViewItem taskLvi = CreateTaskListViewItem( current );
                this.listView1.Items.Add(taskLvi);
            }

            foreach (ITask task in _device.Tasks.ToArray())
            {
                ListViewItem lvi = CreateTaskListViewItem(task);
                this.listView1.Items.Add(lvi);
            }
        }

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
