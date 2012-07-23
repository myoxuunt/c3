
using System;
using Xdgk.Common;

namespace C3.Communi
{
    /// <summary>
    /// 
    /// </summary>
    abstract public class DeviceDataBase : IDeviceData
    {
        /// <summary>
        /// 
        /// </summary>
        protected DeviceDataBase()
        {
            this.DT = DateTime.Now;
        }

        /// <summary>
        /// 
        /// </summary>
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

    }

}
