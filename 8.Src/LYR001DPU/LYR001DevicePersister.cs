using System;
using C3.Communi;
using Xdgk.Common;
using Xdgk.GR.Common;

namespace LYR001DPU
{
    public class LYR001DevicePersister : DevicePersisterBase
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="device"></param>
        protected override void OnAdd(IDevice device)
        {
            LYR001Device d = (LYR001Device)device;

            string s = string.Format(
                    "insert into tblDevice(DeviceAddress, deviceType, stationID, DeviceExtend, DeviceName) values({0}, '{1}', {2}, '{3}', '{4}')",
                    d.Address,
                    d.DeviceType.Type.Name,
                    GuidHelper.ConvertToInt32(d.Station.Guid),
                    GetExtend(d),
                    d.Name 
                    );
            DBI.Instance.ExecuteScalar(s);
            d.Guid = GuidHelper.Create(GetMaxDeviceID(DBI.Instance));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        private static string GetExtend(LYR001Device d)
        {
            StringStringDictionary ssDict = new StringStringDictionary();
            ssDict["HtmMode"] = d.HtmMode.ModeValue.ToString();

            string extend = StringStringDictionaryConverter.ToString(ssDict);
            return extend;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="device"></param>
        protected override void OnUpdate(IDevice device)
        {
            // TODO:
            //
            string s = string.Format(
                    "update tblDevice set DeviceAddress = {0}, DeviceName = '{1}', DeviceExtend = '{2}' where DeviceID = {3}",
                    device.Address,
                    device.Name,
                    GetExtend((LYR001Device)device),
                    GuidHelper.ConvertToInt32(device.Guid)
                    );

            DBI.Instance.ExecuteScalar(s);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="device"></param>
        protected override void OnDelete(IDevice device)
        {
            string s = string.Format(
                    "delete from tblDevice where DeviceID = {0}",
                    GuidHelper.ConvertToInt32(device.Guid));
            DBI.Instance.ExecuteScalar(s);

        }
    }

}
