
using System;
using System.Data.SqlClient;
using System.Collections;
using System.Data;
using System.Collections.Generic;
using System.Windows.Forms;
using C3.Communi;
using Xdgk.Common;
using NLog;
//using C3.Data;
using Xdgk.Common;


namespace XD1100DPU
{
    internal class OutsideTemperatureProviderManager
    {
        private OutsideTemperatureProviderManager()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        static public IOutsideTemperatureProvider Provider
        {
            get
            {
                if (_p == null)
                {
                    _p = new FixedOTProvider();
                }
                return _p; 
            }
            set
            {
                _p = value;
            }
        } static private IOutsideTemperatureProvider _p;

#region IOutsideTemperatureProvider ≥…‘±
        /// <summary>
        /// 
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        static public float GetStandardOutsideTemperature(IDevice device)
        {
            return Provider.GetStandardOutsideTemperature(device);
        }

#endregion
    }

}
