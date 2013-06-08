using System;
using System.Data;
using System.Collections.Specialized;
using Xdgk.Common;

namespace C3.Communi
{
    abstract public class DevicePersisterBase : IDevicePersister
    {


        public void Add(IDevice device)
        {
            OnAdd(device);
        }


        public void Update(IDevice device)
        {
            OnUpdate(device);
        }

        public void Delete(IDevice device)
        {
            OnDelete(device);
        }

        protected abstract void OnAdd(IDevice device);
        protected abstract void OnUpdate(IDevice device);
        protected abstract void OnDelete(IDevice device);

        /// <summary>
        /// get max deviceid, if not exist throw exception
        /// </summary>
        /// <returns></returns>
        static public int GetMaxDeviceID(DBIBase dbi)
        {
            string sql = "select Max(DeviceID) from tblDevice";
            object obj = dbi.ExecuteScalar(sql);
            if (obj != null && obj != DBNull.Value)
            {
                int lastDeviceID = Convert.ToInt32(obj);
                return lastDeviceID;
            }
            else
            {
                string msg = "not find max deviceid in tblDevice";
                throw new InvalidOperationException(msg);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stationID"></param>
        /// <param name="deviceType"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        static public int GetDeviceID(DBIBase dbi, int stationID, string deviceType, ulong deviceAddress)
        {
            string sql = "select deviceID from tblDevice where stationID = @stationID and deviceAddress = @deviceAddress and deviceType = @deviceType";

            ListDictionary list = new ListDictionary();
            list.Add("stationID", stationID);
            list.Add("deviceType", deviceType);
            list.Add("deviceAddress", deviceAddress);
            object obj = dbi.ExecuteScalar(sql, list);

            if (obj != null && obj != DBNull.Value)
            {
                return Convert.ToInt32(obj);
            }
            else 
            {
                string msg = string.Format(
                    "not find deviceid by stationID='{0}', deviceType='{1}', deviceAddress='{2}'",
                    stationID, deviceType , deviceAddress);
                throw new InvalidOperationException(msg);
            }
        }
    }

}
