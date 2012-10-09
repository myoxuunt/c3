
using System;
using System.Data.SqlClient;
using System.Collections;
using System.Data;
using System.Collections.Generic;
using System.Windows.Forms;
using C3.Communi;
using Xdgk.Common;
using NLog;
using Xdgk.Common;

namespace XD1100DPU
{
    internal class XD1100DeviceSourceProvider : DeviceSourceProviderBase
    {
        public override IDeviceSource[] OnGetDeviceSources()
        {
            List<IDeviceSource> list = new List<IDeviceSource>();

            DataTable tbl = DBI.Instance.ExecuteXD1100DeviceDataTable();
            foreach (DataRow row in tbl.Rows)
            {
                XD1100DeviceSource item = new XD1100DeviceSource(row);
                list.Add(item);
            }
            return list.ToArray();
        }
    }

}
