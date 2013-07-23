using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Xdgk.Common.Export
{
    public class DataColumnXlsColumnPairCollection : Xdgk.Common.Collection<DataColumnXlsColumnPair>
    {
    }

    public class DataColumnXlsColumnPair
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="xlsColumn"></param>
        public DataColumnXlsColumnPair(string dataColumnName, int xlsColumn)
        {
            this.DataColumnName = dataColumnName;
            this.XlsColumnIndex = xlsColumn; ;
        }

        /// <summary>
        /// 
        /// </summary>
        public string DataColumnName
        {
            get { return _dataColumnName; }
            set 
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("DataColumnName");
                }
                _dataColumnName = value; 
            }
        } private string _dataColumnName;

        /// <summary>
        /// 
        /// </summary>
        public int XlsColumnIndex
        {
            get { return _xlsColumnIndex; }
            set 
            {
                if (value < 1)
                    throw new ArgumentException("value must >= 1");
                _xlsColumnIndex = value; 
            }
        } private int _xlsColumnIndex;
    }

}
