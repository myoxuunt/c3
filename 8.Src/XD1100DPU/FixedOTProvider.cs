
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
    internal class FixedOTProvider : IOutsideTemperatureProvider 
    {
        public float Value
        {
            get { return _value; }
            set { _value = value; }
        } private float _value;

#region IOutsideTemperatureProvider ≥…‘±

        /// <summary>
        /// 
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        public float GetStandardOutsideTemperature(IDevice device)
        {
            return Value;
        }

#endregion
    }

}
