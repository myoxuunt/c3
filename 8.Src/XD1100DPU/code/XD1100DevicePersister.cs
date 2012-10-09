
using System;
using C3.Communi;
using Xdgk.Common;



namespace XD1100DPU
{
    public class XD1100DevicePersister : DevicePersisterBase
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="device"></param>
        protected override void OnAdd(IDevice device)
        {
            XD1100Device d = (XD1100Device)device;

            string s = string.Format(
                    "insert into tblDevice(DeviceAddress, deviceType, stationID, DeviceExtend) values({0}, '{1}', {2}, '{3}'); select @@identity;",
                    d.Address,
                    d.DeviceType.Type.Name,
                    GuidHelper.ConvertToInt32(d.Station.Guid),
                    GetExtend(d)
                    );

            object obj = DBI.Instance.ExecuteScalar(s);
            d.Guid = GuidHelper.Create(Convert.ToInt32(obj));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        private static string GetExtend(XD1100Device d)
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
                    "update tblDevice set DeviceAddress = {0}, DeviceExtend = '{1}' where DeviceID = {2}",
                    device.Address,
                    GetExtend((XD1100Device)device),
                    GuidHelper.ConvertToInt32(device.Guid));

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
