
using System.Collections.Generic;
using System.Data;
using C3.Communi;

namespace LYR001DPU
{
    internal class LYR001DeviceSourceProvider : DeviceSourceProviderBase
    {
        public override IDeviceSource[] OnGetDeviceSources()
        {
            List<IDeviceSource> list = new List<IDeviceSource>();

            DataTable tbl = DBI.Instance.ExecuteLYR001DeviceDataTable();
            foreach (DataRow row in tbl.Rows)
            {
                LYR001DeviceSource item = new LYR001DeviceSource(row);
                list.Add(item);
            }
            return list.ToArray();
        }
    }

}
