using System;
using System.Reflection;
//using C3.Data;

namespace Xdgk.Common
{
    /// <summary>
    /// 
    /// </summary>
    abstract public class DataBase : IData
    {

        #region DataBase
        /// <summary>
        /// 
        /// </summary>
        protected DataBase()
        {
            this.DT = DateTime.Now;
        }
        #endregion //DataBase

        #region DT
        /// <summary>
        /// 
        /// </summary>
        /// 
        [DataItem("Ê±¼ä", -1, Unit.None, "G")]
        public DateTime DT
        {
            get
            {
                return _dt;
            }
            set
            {
                _dt = value;
            }
        } private DateTime _dt;
        #endregion //DT

        #region GetDeviceDataItemAttributes
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal AttributePropertyInfoPairCollection GetDeviceDataItemAttributes()
        {
            AttributePropertyInfoPairCollection result = new AttributePropertyInfoPairCollection();
            PropertyInfo[] propertyInfos = this.GetType().GetProperties();
            foreach (PropertyInfo pi in propertyInfos)
            {
                object[] atts = pi.GetCustomAttributes(typeof(DataItemAttribute), false);
                if (atts.Length > 0)
                {
                    DataItemAttribute att = (DataItemAttribute)atts[0];

                    AttributePropertyInfoPair pair = new AttributePropertyInfoPair(
                        att, pi);

                    result.Add(pair);
                }
            }

            // sort
            //
            result.Sort();

            return result;
        }
        #endregion //GetDeviceDataItemAttributes

        #region GetReportItems
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        virtual public ReportItemCollection GetReportItems()
        {
            ReportItemCollection reportItems = new ReportItemCollection();

            AttributePropertyInfoPairCollection attPropertyInfos = this.GetDeviceDataItemAttributes();
            foreach (AttributePropertyInfoPair item in attPropertyInfos)
            {
                DataItemAttribute att = item.Attribute;
                PropertyInfo pi = item.PropertyInfo;
                object value = pi.GetValue(this, null);
                ReportItem reportItem = new ReportItem(att.Name, value, att.Unit, att.Format);
                reportItems.Add(reportItem);
            }

            return reportItems;
        }
        #endregion //GetReportItems
    }
}
