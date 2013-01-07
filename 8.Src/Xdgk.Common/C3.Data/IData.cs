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
        ReportItemCollection GetReportItems();
        AttributePropertyInfoPairCollection GetDeviceDataItemAttributes();
    }
}
