using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Xdgk.Common;

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

            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ReadOnly = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmDeviceHistoryData_Load(object sender, EventArgs e)
        {
            DataTable tbl = _dataSource as DataTable;

            foreach (DataColumn tblColumn in tbl.Columns)
            {
                this.dataGridView1.Columns.Add(Create(tblColumn));
            }
            this.dataGridView1.DataSource = _dataSource;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tblColumn"></param>
        /// <returns></returns>
        private DataGridViewColumn Create(DataColumn tblColumn)
        {
            string name = (string)tblColumn.ExtendedProperties["name"];
            string unit = (string)tblColumn.ExtendedProperties["unit"];
            string format = (string)tblColumn.ExtendedProperties["format"];

            DataGridViewTextBoxColumn dgColumn = new DataGridViewTextBoxColumn();
            dgColumn.Name = tblColumn.ColumnName;
            string headerText = name;
            if (unit.Length > 0)
            {
                headerText += string.Format("({0})", unit);
            }

            dgColumn.HeaderText = headerText;
            dgColumn.Width = GetDataGridViewColumnWidth(tblColumn.DataType);
            dgColumn.DataPropertyName = tblColumn.ColumnName;
            if (!string.IsNullOrEmpty(format))
            {
                dgColumn.DefaultCellStyle.Format = format;
            }
            return dgColumn;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private int GetDataGridViewColumnWidth(Type type)
        {
            int def = 100;
            int[] widths = new int[] { 140 };
            Type[] types = new Type[] { typeof(DateTime) };

            for (int i = 0; i < types.Length; i++)
            {
                if (type == types[i])
                {
                    return widths[i];
                }
            }
            return def;
        }

    }
}
