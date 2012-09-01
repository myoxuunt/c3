
using System;
using System.Data.SqlClient;
using System.Collections;
using System.Data;
using System.Collections.Generic;
using System.Windows.Forms;
using C3.Communi;
using Xdgk.Common;
using NLog;
using C3.Data;


namespace XD1100DPU
{
    internal class DeviceOTProvider : IOutsideTemperatureProvider
    {

        public DeviceOTProvider (IOutside outside)
        {
            this.Outside = outside;
        }

        /// <summary>
        /// 
        /// </summary>
        public IOutside Outside
        {
            get { return _outside; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Outside");
                }
                _outside = value;
            }
        } private IOutside _outside;

#region IOutsideTemperatureProvider ≥…‘±
        /// <summary>
        /// 
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        public float GetStandardOutsideTemperature(IDevice device)
        {
            return this.Outside.OutsideTemperature;
        }

#endregion
    }

}
