using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace C3
{
    public partial class frmDeviceHistoryData : Form
    {
        /// <summary>
        /// 
        /// </summary>
        public object DataSource
        {
            get { return _dataSource; }
            set { _dataSource = value; }
        } private object _dataSource;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        public frmDeviceHistoryData()
        {
            InitializeComponent();
        }

        public System.Windows.Forms.DataGridView  DV
        {
            get { return this.dataGridView1; }
    }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmDeviceHistoryData_Load(object sender, EventArgs e)
        {
            this.dataGridView1.DataSource = _dataSource;
            //this.dataGridView1.Columns[0].
        }

    }
}
