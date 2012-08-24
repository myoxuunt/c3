using System;

namespace C3.Communi
{
    public interface IDeviceData
    {
        DateTime DT { get; set; }
        ReportItemCollection GetReportItems();

    }
}
