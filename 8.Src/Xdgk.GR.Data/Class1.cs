using System;
using System.Collections.Generic;
using System.Text;
using C3.Data;

namespace Xdgk.GR.Data
{
    /// <summary>
    /// 
    /// </summary>
    public class FlowmeterData : DeviceDataBase
    {
        #region InstantFlux
        /// <summary>
        /// 
        /// </summary>
        [DeviceDataItem("瞬时", 10, Unit.M3PerSecond, "f2")]
        public double InstantFlux
        {
            get
            {
                return _instantFlux;
            }
            set
            {
                _instantFlux = value;
            }
        } private double _instantFlux;
        #endregion //InstantFlux

        #region Sum
        /// <summary>
        /// 
        /// </summary>
        [DeviceDataItem("累计", 20, Unit.M3, "f0")]
        public double Sum
        {
            get
            {
                return _sum;
            }
            set
            {
                _sum = value;
            }
        } private double _sum;
        #endregion //Sum
    }
}
