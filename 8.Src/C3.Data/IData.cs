using System;
using System.Collections.Generic;
using System.Text;

namespace C3.Data
{
    /// <summary>
    /// 
    /// </summary>
    public interface IData
    {
        DateTime DT { get; set; }
        ReportItemCollection GetReportItems();
    }
}
