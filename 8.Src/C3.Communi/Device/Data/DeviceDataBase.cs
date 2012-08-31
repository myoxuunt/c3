using System;
using System.Reflection;
using C3.Data;

namespace C3.Communi
{
    /// <summary>
    /// 
    /// </summary>
    abstract public class DeviceDataBase : IDeviceData
    {

        #region DeviceDataBase
        /// <summary>
        /// 
        /// </summary>
        protected DeviceDataBase()
        {
            this.DT = DateTime.Now;
        }
        #endregion //DeviceDataBase

        #region DT
        /// <summary>
        /// 
        /// </summary>
        /// 
        //[DeviceDataItem("Ê±¼ä", -1, Unit.None, "G")]
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
        public AttributePropertyInfoPairCollection GetDeviceDataItemAttributes()
        {
            AttributePropertyInfoPairCollection result = new AttributePropertyInfoPairCollection();
            PropertyInfo[] propertyInfos = this.GetType().GetProperties();
            foreach (PropertyInfo pi in propertyInfos)
            {
                object[] atts = pi.GetCustomAttributes(typeof(DeviceDataItemAttribute), false);
                if (atts.Length > 0)
                {
                    DeviceDataItemAttribute att = (DeviceDataItemAttribute)atts[0];

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
        public ReportItemCollection GetReportItems()
        {
            ReportItemCollection reportItems = new ReportItemCollection();

            AttributePropertyInfoPairCollection attPropertyInfos = this.GetDeviceDataItemAttributes();
            foreach (AttributePropertyInfoPair item in attPropertyInfos)
            {
                DeviceDataItemAttribute att = item.Attribute;
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
