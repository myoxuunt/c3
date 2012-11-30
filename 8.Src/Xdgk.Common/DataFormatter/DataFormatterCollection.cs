
using System;
using System.Collections.Generic;
using System.Text;


namespace Xdgk.Common
{
    public class DataFormatterCollection : Collection<IDataFormatter>
    {
        // TODO: add default data formatter collection
        // float, double, datetime, decimal, int
        //
        /// <summary>
        /// 
        /// </summary>
        static public DataFormatterCollection DefaultDataFormatterCollection
        {
            get
            {
                if (_dataFormatterCollection == null)
                {
                    DataFormatterCollection s = new DataFormatterCollection();
                    s.Add(new SingleFormatter());
                    s.Add(new Int32Formatter());
                    s.Add(new DoubleFormatter());

                    _dataFormatterCollection = s;
                }
                return _dataFormatterCollection;
            }
            set { _dataFormatterCollection = value; }
        } static private DataFormatterCollection _dataFormatterCollection;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="item"></param>
        protected override void InsertItem(int index, IDataFormatter item)
        {
            IDataFormatter df = GetDataFormatter(item.GetType());
            if (df != null)
            {
                throw new ArgumentException(
                        string.Format("exist type {0}", item.GetType().FullName)
                        );
            }
            base.InsertItem(index, item);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        public IDataFormatter GetDataFormatter(Type type)
        {
            foreach (IDataFormatter df in this)
            {
                if (df.DataType == type)
                {
                    return df;
                }
            }
            return null;
        }
    }

}
