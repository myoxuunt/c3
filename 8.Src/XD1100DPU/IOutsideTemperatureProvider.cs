
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
    internal interface IOutsideTemperatureProvider
    {
        float GetStandardOutsideTemperature(IDevice device);
    }

}
