using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using C3.Communi;
using Xdgk.Common;
using Xdgk.GR.Common;

namespace LYR001DPU
{
    /// <summary>
    /// 
    /// </summary>
    public class LYR001DeviceProcessor : TaskProcessorBase
    {
        #region Members
        #endregion //Members

        #region RemoveUnkonwnPlaceDevice
        /// <summary>
        /// 
        /// </summary>
        /// <param name="devices"></param>
        /// <returns></returns>
        private DeviceCollection RemoveUnkonwnPlaceDevice(DeviceCollection devices)
        {
            DeviceCollection r = new DeviceCollection();
            foreach (IDevice d in devices)
            {
                if (d is PlaceDeviceBase)
                {
                    PlaceDeviceBase pd = d as PlaceDeviceBase;
                    if (pd.Place != FluxPlace.Unknown)
                    {
                        r.Add(pd);
                    }
                }
            }
            return r;
        }
        #endregion //RemoveUnkonwnPlaceDevice

        #region OnProcess
        /// <summary>
        /// 
        /// </summary>
        /// <param name="task"></param>
        /// <param name="parseResult"></param>
        public override void OnProcess(ITask task, IParseResult pr)
        {
            if (pr.IsSuccess)
            {
                string opera = task.Opera.Name;
                if (StringHelper.Equal(opera, LYR001OperaNames.ReadReal))
                {
                    ProcessReadReal(task, pr);
                }
                else if (StringHelper.Equal(opera, LYR001OperaNames.ReadStatus))
                {
                    ProcessReadStatus(task, pr);
                }
                else if (
                    (StringHelper.Equal(opera, LYR001OperaNames.WriteOT)) ||
                    (StringHelper.Equal(opera, LYR001OperaNames.WriteOTMode)) ||
                    (StringHelper.Equal(opera, LYR001OperaNames.OPERA_READ)) ||
                    (StringHelper.Equal(opera, LYR001OperaNames.OPERA_WRITE)) ||
                    (StringHelper.Equal(opera, "ReadM"))
                    )
                {
//task.Opera.next
                }
                else
                {
                    string s = string.Format("not process LYR001 opera '{0}'", opera);
                    throw new NotImplementedException(s);
                }
            }
        }
        #endregion //OnProcess

        /// <summary>
        /// 
        /// </summary>
        /// <param name="task"></param>
        /// <param name="pr"></param>
        private void ProcessReadStatus(ITask task, IParseResult pr)
        {
            byte[] bs = (byte[])pr.Results["Status"];
            LYR001StatusData statusData = LYR001StatusDataParser.Parse(bs);


            LYR001Device d = (LYR001Device)task.Device;
            LYR001DataCache cache = d.DataCacheManager.GetDataCache();
            cache.StatusData = statusData;

            ProcessDataCache(d, cache);
        }

        //#region ProcessReadStatus
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="task"></param>
        ///// <param name="parseResult"></param>
        //private void ProcessReadStatus(ITask task, IParseResult pr)
        //{
        //    LYR001Device d = (LYR001Device ) task.Device;

        //    byte[] bsStatus = (byte[])pr.Results["data"];
        //    Debug.Assert(bsStatus.Length == 4);
        //    byte b = bsStatus[3];
        //    bool hasPowerAlarm = (b & (byte)Math.Pow(2, 7)) > 0;
        //    d.StatusAndAlarmDictionary[LYR001Device.StatusAndAlarmEnum.AlaramPower] = hasPowerAlarm;
        //}
        //#endregion //ProcessReadStatus

        #region ProcessReadReal
        /// <summary>
        /// 
        /// </summary>
        /// <param name="d"></param>
        /// <param name="parseResult"></param>
        private void ProcessReadReal(ITask task, IParseResult pr)
        {
            //GRData data = new GRData();
            LYR001AnalogData data = new LYR001AnalogData();

            //data.DT = DateTime.Now;

            data.GT1 = Convert.ToSingle(pr.Results["GT1"]);
            data.BT1 = Convert.ToSingle(pr.Results["BT1"]);
            data.GT2 = Convert.ToSingle(pr.Results["GT2"]);
            data.BT2 = Convert.ToSingle(pr.Results["BT2"]);
            data.OT = Convert.ToSingle(pr.Results["OT"]);
            //data.GTBase2 = Convert.ToSingle(pr.Results["GTBase2"]);
            data.GP1 = Convert.ToSingle(pr.Results["GP1"]);
            data.BP1 = Convert.ToSingle(pr.Results["BP1"]);
            data.WL = Convert.ToSingle(pr.Results["WL"]);
            data.GP2 = Convert.ToSingle(pr.Results["GP2"]);
            data.BP2 = Convert.ToSingle(pr.Results["BP2"]);
            data.I1 = Convert.ToSingle(pr.Results["I1"]);
            data.IR = Convert.ToSingle(pr.Results["IR"]);
            //data.I2 = Convert.ToSingle(pr.Results["I2"]);
            //data.S2 = Convert.ToInt32(pr.Results["S2"]);
            data.S1 = Convert.ToInt32(pr.Results["S1"]);
            data.SR = Convert.ToInt32(pr.Results["SR"]);
            data.OD = Convert.ToInt32(pr.Results["OD"]);
            //data.IH1 = Convert.ToDouble (parseResult.Results ["IH1"]);
            //data.SH1 = Convert.ToDouble (parseResult.Results ["SH1"]);
            //data.IH1 = 0d;
            //data.SH1 = 0d;


            LYR001Device d = (LYR001Device)task.Device;
            LYR001DataCache dataCache = d.DataCacheManager.GetDataCache();
            dataCache.AnalogData = data;
            // TODO: process lyr001 data cache
            // 
            //d.DeviceDataManager.Last = data;


            //// save
            ////
            //int id = GuidHelper.ConvertToInt32(d.Guid);
            //DBI.Instance.InsertGRData(id, data);

            ProcessDataCache(d, dataCache);
        }
        #endregion //ProcessReadReal

        private void ProcessDataCache(LYR001Device d, LYR001DataCache dataCache)
        {
            if (dataCache.IsComplete())
            {
                GRData grdata = dataCache.ToGRData();
                d.DeviceDataManager.Last = grdata;

                // save
                //
                int id = GuidHelper.ConvertToInt32(d.Guid);
                DBI.Instance.InsertGRData(id, grdata);
            }
        }

        #region HasPowerAlaramInStatus
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool HasPowerAlaramInStatus(LYR001Device LYR001)
        {
            bool b = LYR001.StatusAndAlarmDictionary.ContainsKey(LYR001Device.StatusAndAlarmEnum.AlaramPower);
            if (b)
            {
                return LYR001.StatusAndAlarmDictionary[LYR001Device.StatusAndAlarmEnum.AlaramPower];
            }
            return false;
        }
        #endregion //HasPowerAlaramInStatus

        #region IsPumpRun
        /// <summary>
        /// 
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        private PumpStatus IsPumpRun(bool b)
        {
            return IsPumpRun(b ? PumpStatusEnum.Run : PumpStatusEnum.Stop);
        }
        #endregion //IsPumpRun

        #region IsPumpRun
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pse"></param>
        /// <returns></returns>
        private PumpStatus IsPumpRun(PumpStatusEnum pse)
        {
            if (pse == PumpStatusEnum.Run)
            {
                return PumpStatus.Run;
            }
            else if (pse == PumpStatusEnum.Stop)
            {
                return PumpStatus.Stop;
            }
            throw new ArgumentException(pse.ToString());
        }
        #endregion //IsPumpRun

        #region Filter
        /// <summary>
        /// 
        /// </summary>
        /// <param name="devices"></param>
        /// <param name="fluxPlace"></param>
        /// <returns></returns>
        private DeviceCollection Filter(DeviceCollection devices, FluxPlace fluxPlace)
        {
            DeviceCollection r = new DeviceCollection();
            foreach (IDevice device in devices)
            {
                PlaceDeviceBase pd = device as PlaceDeviceBase;
                if (pd.Place == fluxPlace)
                {
                    r.Add(pd);
                }
            }
            return r;
        }
        #endregion //Filter

        #region OnProcessUpload
        /// <summary>
        /// 
        /// </summary>
        /// <param name="device"></param>
        /// <param name="parseResult"></param>
        public override void OnProcessUpload(IDevice device, IParseResult pr)
        {
            throw new NotImplementedException();
        }
        #endregion //OnProcessUpload
    }
}
