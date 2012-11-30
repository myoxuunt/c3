using System;
using System.Collections.Generic;
using System.Text;

namespace Xdgk.Common
{
    /// <summary>
    /// 
    /// </summary>
    public interface IData
    {
        DateTime DT { get; set; }
        bool IsValid { get; }
        object GetValue(string propertyName);
        bool HasPropertyName(string propertyName);

        ReportItemCollection GetReportItems();
    }
}
