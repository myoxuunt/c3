using System;
using System.Collections.Generic;
using System.Text;

namespace C3.Data
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDeviceData
    {
        DateTime DT { get; set; }
        ReportItemCollection GetReportItems();
    }

    #region DeviceDataCollection
    /// <summary>
    /// 
    /// </summary>
    public class DeviceDataCollection : Xdgk.Common.Collection<IDeviceData>
    {
        private const int DEFAULT_CAPABILITY = 1000;
        private const int MIN_CAPABILITY = 10;

        #region Add
        /// <summary>
        /// 
        /// </summary>
        /// <param name="deviceData"></param>
        internal new void Add(IDeviceData deviceData)
        {
            base.Add(deviceData);
        }
        #endregion //Add

        #region Insert
        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="item"></param>
        internal new void Insert(int index, IDeviceData item)
        {
            base.Insert(index, item);
        }
        #endregion //Insert

        #region Capability
        /// <summary>
        /// 
        /// </summary>
        public int Capability
        {
            get { return _capability; }
            set
            {
                if (value < MIN_CAPABILITY)
                {
                    value = MIN_CAPABILITY;
                }
                _capability = value;
            }
        } private int _capability = DEFAULT_CAPABILITY;
        #endregion //Capability

        #region InsertItem
        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="item"></param>
        protected override void InsertItem(int index, IDeviceData item)
        {
            if (this.Count >= this.Capability)
            {
                if (this.Count > 0)
                {
                    this.RemoveAt(0);
                }
            }
            base.InsertItem(index, item);
        }
        #endregion //InsertItem
    }
    #endregion //DeviceDataCollection

}
