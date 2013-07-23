using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Diagnostics;
using FlexCel.XlsAdapter;

namespace Xdgk.Common.Export
{
    /// <summary>
    /// DataViewGrid excel exporter
    /// </summary>
    public class DataGridViewExcelExporter
    {
        /// <summary>
        /// 最大导出行数
        /// </summary>
        public static readonly int MAX_ROW_COUNT = 65000;

        #region IsOutMaxCount
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        static public bool IsOutMaxCount(int dataGridViewRowCount)
        {
            return dataGridViewRowCount >= MAX_ROW_COUNT;
        }
        #endregion //IsOutMaxCount

        private DataGridView _dgv;

        #region DataFormatterCollection
        /// <summary>
        /// 
        /// </summary>
        public DataFormatterCollection DataFormatterCollection
        {
            get
            {
                if (_dataFormatterCollection == null)
                    _dataFormatterCollection = new DataFormatterCollection();
                return _dataFormatterCollection;
            }
            set
            {
                this._dataFormatterCollection = value;
            }
        } private DataFormatterCollection _dataFormatterCollection;
        #endregion //DataFormatterCollection

        #region XlsFile
        /// <summary>
        /// 
        /// </summary>
        public FlexCel.XlsAdapter.XlsFile XlsFile
        {
            get 
            {
                if (_xlsFile == null)
                {
                    _xlsFile = new FlexCel.XlsAdapter.XlsFile(true);
                    _xlsFile.NewFile(1);
                }
                return _xlsFile; 
            }
        } private FlexCel.XlsAdapter.XlsFile _xlsFile;
        #endregion //XlsFile

        #region ExcelExporter
        public DataGridViewExcelExporter(DataGridView dgv)
            : this(dgv, null, null)
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        public DataGridViewExcelExporter(DataGridView dgv, XlsFile xlsFile)
            : this(dgv, xlsFile, null)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public DataGridViewExcelExporter(DataGridView dgv, FlexCel.XlsAdapter.XlsFile xlsFile, DataFormatterCollection dataFormatterCollection)
        {
            if (dgv == null)
            {
                throw new ArgumentNullException("dgv");
            }
            this._dgv = dgv;
            this._xlsFile = xlsFile;
            this.DataFormatterCollection = dataFormatterCollection;
        }
        #endregion //ExcelExporter

        #region Export
        /// <summary>
        /// 
        /// </summary>
        public void Export()
        {
            if (IsOutMaxCount(_dgv.Rows.Count))
            {
                throw new InvalidOperationException("dataGridView.Rows.Count too large");
            }

            ExportHeader();
            ExportRows();
        }
        #endregion //Export

        #region ExportHeader
        /// <summary>
        /// 
        /// </summary>
        private void ExportHeader()
        {
            // column headtext
            //
            int headerRow = this._currentRow;
            foreach (DataGridViewColumn dgvcol in _dgv.Columns)
            {
                int headerCol = dgvcol.DisplayIndex + 1;
                string headerValue = dgvcol.HeaderText;
                XlsFile.SetCellValue(headerRow, headerCol, headerValue);
            }
        }
        #endregion //ExportHeader

        #region ExportRows
        /// <summary>
        /// 
        /// </summary>
        private void ExportRows()
        {
            // grid row value
            //
            this._currentRow++;
            foreach (DataGridViewRow dgvrow in _dgv.Rows)
            {
                foreach (DataGridViewCell cell in dgvrow.Cells)
                {
                    if (cell.Value != null)
                    {
                        object val = null;
                        IDataFormatter formatter = this.DataFormatterCollection.GetDataFormatter(cell.ValueType);
                        if (formatter != null)
                        {
                            val = formatter.Format(cell.Value);
                        }
                        else
                        {
                            val = cell.FormattedValue;
                        }

                        int col = _dgv.Columns[cell.ColumnIndex].DisplayIndex + 1;
                        XlsFile.SetCellValue(_currentRow, col, val);
                    }
                }
                _currentRow++;
            }
        }
        #endregion //ExportRows

        #region CurrentRow
        /// <summary>
        /// 获取或设置xls当前行, 数据将从当前行开始写入
        /// </summary>
        public int CurrentRow
        {
            get { return _currentRow; }
            set
            {
                if (_currentRow < 1 || _currentRow >= MAX_ROW_COUNT)
                {
                    string msg = string.Format("CurrentRow must >= 1 and < {0}", 
                        MAX_ROW_COUNT);
                    throw new ArgumentOutOfRangeException(msg);
                }
                _currentRow = value;
            }
        } private int _currentRow = 1;
        #endregion //CurrentRow

        #region Save
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        public void Save(string fileName)
        {
            this.XlsFile.Save(fileName);
        }
        #endregion //Save
    }
}
