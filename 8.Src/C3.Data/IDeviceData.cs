﻿using System;
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
    #endregion //DeviceDataCollection

}
