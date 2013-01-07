using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Xdgk.Common;
using C3.Communi;

namespace C3
{
    public partial class frmDeviceHistoryData : Form
    {
        private IDevice _device;
        private DataTable _dataSourceDataTable;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        public frmDeviceHistoryData(IDevice device)
        {
            if (device == null)
            {
                throw new ArgumentNullException("device");
            }
            _device = device;
            _dataSourceDataTable = ConvertToDataTable(_device.DeviceDataManager.Datas);

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
            this.Text = string.Format("{0}:{1}", 
                this._device.Station.Text, this._device.Text);
            //DataTable tbl = _dataSourceDataTable;

            foreach (DataColumn tblColumn in _dataSourceDataTable.Columns)
            {
                this.dataGridView1.Columns.Add(Create(tblColumn));
            }
            this.dataGridView1.DataSource = _dataSourceDataTable;
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
            int def = 80;
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


        /// <summary>
        /// 
        /// </summary>
        /// <param name="datas"></param>
        /// <returns></returns>
        private DataTable ConvertToDataTable(DataCollection datas)
        {
            DataTable tbl = new DataTable();
            if (datas.Count > 0)
            {
                IData last = datas[datas.Count - 1];
                Type lastType = last.GetType();
                //ReportItemCollection reportItems = last.GetReportItems();

                //foreach (ReportItem ri in reportItems)
                //{
                //    Type valueType = ri.Value.GetType();
                //    DataColumn column = new DataColumn(ri.Name, valueType);
                //    tbl.Columns.Add(column);
                //}

                //foreach (IData data in datas)
                //{
                //    if (data.GetType() != lastType)
                //    {
                //        continue;
                //    }
                //    object[] values = GetReportItemCollectionValues(data.GetReportItems());
                //    tbl.Rows.Add(values);
                //}



                AttributePropertyInfoPairCollection ss = last.GetDeviceDataItemAttributes();
                foreach (AttributePropertyInfoPair s in ss)
                {
                    Type valueType = s.PropertyInfo.PropertyType;
                    DataItemAttribute diAttribute = s.Attribute;
                    string columnName = diAttribute.Name;

                    DataColumn column = new DataColumn(columnName, valueType);
                    column.ExtendedProperties["unit"] = diAttribute.Unit.Text;
                    column.ExtendedProperties["format"] = diAttribute.Format;
                    column.ExtendedProperties["name"] = diAttribute.Name;
                    tbl.Columns.Add(column);
                }


                foreach (IData data in datas)
                {
                    if (data.GetType() != lastType)
                    {
                        continue;
                    }
                    object[] values = new object[ss.Count];
                    int idx = 0;
                    foreach (AttributePropertyInfoPair s in ss)
                    {
                        object v= s.PropertyInfo.GetValue(data, null);
                        values[idx++] = v;
                    }
                    tbl.Rows.Add(values);
                }
            }
            return tbl;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="ris"></param>
        ///// <returns></returns>
        //private object[] GetReportItemCollectionValues(ReportItemCollection ris)
        //{
        //    object[] r = new object[ris.Count] ;
        //    int idx = 0;
        //    foreach (ReportItem ri in ris)
        //    {
        //        r[idx++] = ri.Value;
        //    }
        //    return r;
        //}
    }
}
