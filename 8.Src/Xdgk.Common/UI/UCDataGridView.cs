using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Xdgk.Common ;

namespace Xdgk.UI.Forms
{
    /// <summary>
    /// 
    /// </summary>
    public partial class UCDataGridView : UserControl
    {
        public UCDataGridView()
        {
            InitializeComponent();
            InitDataGridView();
        }

        public object DataSource
        {
            get { return this.dataGridView1.DataSource; }
            set { this.dataGridView1.DataSource = value; }
        }


        /// <summary>
        /// 
        /// </summary>
        public DGVColumnConfigCollection DgvColumnConfigs
        {
            get
            {
                if (_dgvColumnConfigs == null)
                {
                    _dgvColumnConfigs = new DGVColumnConfigCollection();
                }
                return _dgvColumnConfigs;
            }
            set
            {
                if (value != _dgvColumnConfigs)
                {
                    _dgvColumnConfigs = value;
                    SetDataGridViewColumns();
                }
            }
        } private DGVColumnConfigCollection _dgvColumnConfigs;

        /// <summary>
        /// 
        /// </summary>
        private void SetDataGridViewColumns()
        {
            this.dataGridView1.Columns.Clear();

            foreach (DGVColumnConfig c in this.DgvColumnConfigs)
            {
                DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
                col.DataPropertyName = c.DataPropertyName;
                col.HeaderText = c.Text;
                col.Visible = c.Visible;

                if (c.Width > 0)
                {
                    col.Width = c.Width;
                }
                else
                {
                    col.Width = 100;
                }

                if (!string.IsNullOrEmpty(c.Format))
                {
                    col.DefaultCellStyle.Format = c.Format;
                }

                this.dataGridView1.Columns.Add(col);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        private void InitDataGridView()
        {
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToAddRows = false;


        }



        /// <summary>
        /// 
        /// </summary>
        public DataGridView DataGridView
        {
            get { return this.dataGridView1; }
        }

        public string ColumnConfigFile
        {
            get { return _columnConfigFile; }
            set 
            { 
                _columnConfigFile = value;
                DGVColumnConfigCollection cfg = DGVColumnConfigCollectionFactory.CreateFromXml(value);
                this.DgvColumnConfigs = cfg;
            }
        } private string _columnConfigFile;
    }
}
