
using System.Collections.Generic;
using System.Data;
using C3.Communi;

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
