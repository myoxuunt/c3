using System;
using System.Collections.Generic;
using System.Text;

namespace C3.Data
{
    public interface IDeviceData
    {
        DateTime DT { get; set; }
        ReportItemCollection GetReportItems();
    }
}
