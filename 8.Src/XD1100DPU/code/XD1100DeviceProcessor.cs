using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using C3.Communi;
using Xdgk.Common;
using Xdgk.GR.Common;

namespace XD1100DPU
{
    /// <summary>
    /// 
    /// </summary>
    public class XD1100DeviceProcessor : TaskProcessorBase
    {
        #region Members
        /// <summary>
        /// 
        /// </summary>
        private string KIND_FLUX = "FluxDevice";

        /// <summary>
        /// 
        /// </summary>
        private string KIND_HEAT = "HeatDevice";

        /// <summary>
        /// 
        /// </summary>
        private const string POWER_ALARM = "µÁ‘¥π ’œ";
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
                if (StringHelper.Equal(opera, XD1100OperaNames.ReadReal))
                {
                    ProcessReadReal(task, pr);
                }
                else if( StringHelper.Equal ( opera, XD1100OperaNames.ReadStatus ))
                {
                    ProcessReadStatus(task, pr);
                }
                else if (
                    (StringHelper.Equal(opera, XD1100OperaNames.WriteOT)) ||
                    (StringHelper.Equal(opera, XD1100OperaNames.WriteOTMode)) ||
                    (StringHelper.Equal(opera, XD1100OperaNames.OPERA_READ)) ||
                    (StringHelper.Equal(opera, XD1100OperaNames.OPERA_WRITE))
                    )
                {

                }
                else
                {
                    string s = string.Format("not process xd1100 opera '{0}'", opera);
                    throw new NotImplementedException(s);
                }
            }
        }
        #endregion //OnProcess

        #region ProcessReadStatus
        /// <summary>
        /// 
        /// </summary>
        /// <param name="task"></param>
        /// <param name="parseResult"></param>
        private void ProcessReadStatus(ITask task, IParseResult pr)
        {
            XD1100Device d = (XD1100Device ) task.Device;

            byte[] bsStatus = (byte[])pr.Results["data"];
            Debug.Assert(bsStatus.Length == 4);
            byte b = bsStatus[3];
            bool hasPowerAlarm = (b & (byte)Math.Pow(2, 7)) > 0;
            d.StatusAndAlarmDictionary[XD1100Device.StatusAndAlarmEnum.AlaramPower] = hasPowerAlarm;
        }
        #endregion //ProcessReadStatus

        #region GetFluxDatas
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fluxDevices"></param>
        /// <returns>
        /// hashtable
        /// key:place   - value:list
        /// place - instantflux , sum
        /// </returns>
        private Hashtable GetFluxDatas(DeviceCollection fluxDevices)
        {
            Hashtable hs = new Hashtable();
            foreach (IDevice d in fluxDevices)
            {
                PlaceDeviceBase pd = d as PlaceDeviceBase;

                List<double> list = GetHashValue(hs, pd.Place);
                IData last = pd.DeviceDataManager.Last;
                list[0] += Convert.ToDouble(last.GetValue("InstantFlux"));
                list[1] += Convert.ToDouble(last.GetValue("Sum"));
            }

            return hs;
        }
        #endregion //GetFluxDatas

        #region GetHashValue
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hs"></param>
        /// <param name="fluxPlace"></param>
        /// <returns></returns>
        private List<double> GetHashValue(Hashtable hs, FluxPlace fluxPlace)
        {
            object obj =  hs[fluxPlace];
            if (obj != null)
            {
                return (List<double>)obj;
            }
            else
            {
                List<double> list = new List<double>();
                // add instantFlux, sum
                //
                list.Add(0);
                list.Add(0);

                hs[fluxPlace] = list;
                return list;
            }
        }
        #endregion //GetHashValue

        #region ProcessReadReal
        /// <summary>
        /// 
        /// </summary>
        /// <param name="d"></param>
        /// <param name="parseResult"></param>
        private void ProcessReadReal(ITask task, IParseResult pr)
        {
            XD1100Data data = new XD1100Data();

            data.DT = DateTime.Now;

            data.GT1 = Convert.ToSingle(pr.Results["GT1"]);
            data.BT1 = Convert.ToSingle(pr.Results["BT1"]);
            data.GT2 = Convert.ToSingle(pr.Results["GT2"]);
            data.BT2 = Convert.ToSingle(pr.Results["BT2"]);
            data.OT = Convert.ToSingle(pr.Results["OT"]);
            data.GTBase2 = Convert.ToSingle(pr.Results["GTBase2"]);
            data.GP1 = Convert.ToSingle(pr.Results["GP1"]);
            data.BP1 = Convert.ToSingle(pr.Results["BP1"]);
            data.WL = Convert.ToSingle(pr.Results["WL"]);
            data.GP2 = Convert.ToSingle(pr.Results["GP2"]);
            data.BP2 = Convert.ToSingle(pr.Results["BP2"]);
            data.I1 = Convert.ToSingle(pr.Results["I1"]);
            data.IR = Convert.ToSingle(pr.Results["IR"]);
            data.I2 = Convert.ToSingle(pr.Results["I2"]);
            data.S2 = Convert.ToInt32(pr.Results["S2"]);
            data.S1 = Convert.ToInt32(pr.Results["S1"]);
            data.SR = Convert.ToInt32(pr.Results["SR"]);
            data.OD = Convert.ToInt32(pr.Results["OD"]);
            //data.IH1 = Convert.ToDouble (parseResult.Results ["IH1"]);
            //data.SH1 = Convert.ToDouble (parseResult.Results ["SH1"]);
            data.IH1 = 0d;
            data.SH1 = 0d;

            // 2012-10-09 xd1100 pump status
            //
            bool[] pumpStatusArray = (bool[])pr.Results["pumpstate"];

            data.CM1 = IsPumpRun(pumpStatusArray[0]);
            data.CM2 = IsPumpRun(pumpStatusArray[1]);
            data.CM3 = IsPumpRun(pumpStatusArray[2]);
            data.RM1 = IsPumpRun(pumpStatusArray[3]);
            data.RM2 = IsPumpRun(pumpStatusArray[4]);

            object objWarn = pr.Results["Warn"];
            IList listWarn = (IList)objWarn;

            bool isContainsPowerAlaram = listWarn.Contains(POWER_ALARM);
            if (!isContainsPowerAlaram)
            {
                if (HasPowerAlaramInStatus(task.Device as XD1100Device))
                {
                    listWarn.Add(POWER_ALARM);
                }
            }

            WarnWrapper ww = new WarnWrapper(listWarn);
            data.Warn = ww;


            XD1100Device d = (XD1100Device)task.Device;
            //List<IFluxProvider> fluxProviderList = GetFluxProviderList(d);
            DeviceCollection fluxDevices = d.Station.Devices.GetDevices(this.KIND_FLUX);
            fluxDevices = RemoveUnkonwnPlaceDevice(fluxDevices);
            bool hasFluxDevices = fluxDevices.Count > 0;
            bool hasFluxData = fluxDevices.HasData(HasDataOption.All);

            bool success = true;
            if (hasFluxDevices)
            {
                if (hasFluxData)
                {
                    Hashtable fluxResultHashTable = GetFluxDatas(fluxDevices);
                    foreach (object obj in fluxResultHashTable.Keys)
                    {
                        FluxPlace place = (FluxPlace)obj;
                        List<double> list = (List<double>)fluxResultHashTable[obj];

                        switch (place)
                        {
                            case FluxPlace.FirstSide:
                                data.I1 = Convert.ToSingle(list[0]);
                                data.S1 = Convert.ToInt32(list[1]);
                                break;

                            case FluxPlace.SecondSide:
                                data.I2 = Convert.ToSingle(list[0]);
                                data.S2 = Convert.ToInt32(list[1]);
                                break;

                            case FluxPlace.RecruitSide:
                                data.IR = Convert.ToSingle(list[0]);
                                data.SR = Convert.ToInt32(list[1]);
                                break;

                            default:
                                break;

                        }
                    }
                }
                else
                {
                    success = false;
                }
            }

            if (!success)
            {
                return;
            }


            DeviceCollection heatDevices = d.Station.Devices.GetDevices(KIND_HEAT);
            //heatDevices = RemoveUnkonwnPlaceDevice(heatDevices);

            heatDevices = this.Filter(heatDevices, FluxPlace.FirstSide);

            bool hasHeatDevices = heatDevices.Count > 0;
            bool hasHeatData = heatDevices.HasData(HasDataOption.All);
            if (hasHeatDevices)
            {
                if (hasHeatData)
                {
                    double instantFlux = 0d;
                    double sumFlux = 0d;
                    double ih = 0d;
                    double sh = 0d;
                    foreach (IDevice hd in heatDevices)
                    {
                        IData last = hd.DeviceDataManager.Last;
                        ih += Convert.ToDouble(last.GetValue("InstantHeat"));
                        sh += Convert.ToDouble(last.GetValue("SumHeat"));
                        instantFlux += Convert.ToDouble(last.GetValue("InstantFlux"));
                        sumFlux += Convert.ToDouble(last.GetValue("Sum"));
                    }

                    data.I1 = Convert.ToSingle(instantFlux);
                    data.S1 = Convert.ToInt32(sumFlux);
                    data.IH1 = ih;
                    data.SH1 = sh;
                }
                else
                {
                    success = false;
                }

            }
            if (!success)
            {
                return;
            }

            switch (d.HtmMode.ModeValue)
            {
                case ModeValue.Indirect:
                    data.CyclePumpDatas.Add(new PumpData("1#", Convert.ToInt32(pr.Results["cyclefrequency"])));
                    data.RecruitPumpDatas.Add(new PumpData("1#", Convert.ToInt32(pr.Results["recruitfrequency"])));
                    break;

                case ModeValue.Direct:
                    data.GT1 = data.GT2;
                    data.BT1 = data.BT2;
                    break;

                case ModeValue.Mixed:
                    data.BT1 = data.BT2;
                    data.CyclePumpDatas.AddRange(
                        new PumpData("1#", Convert.ToInt32(pr.Results["cyclefrequency"])),
                        new PumpData("2#", Convert.ToInt32(pr.Results["recruitfrequency"])),
                        new PumpData("3#", Convert.ToInt32(pr.Results["I2"])));

                    break;
            }
            d.DeviceDataManager.Last = data;


            // save
            //
            int id = GuidHelper.ConvertToInt32(d.Guid);
            DBI.Instance.InsertXD1100Data(id, data);
        }
        #endregion //ProcessReadReal

        #region HasPowerAlaramInStatus
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool HasPowerAlaramInStatus(XD1100Device xd1100)
        {
            bool b = xd1100.StatusAndAlarmDictionary.ContainsKey(XD1100Device.StatusAndAlarmEnum.AlaramPower);
            if (b)
            {
                return xd1100.StatusAndAlarmDictionary[XD1100Device.StatusAndAlarmEnum.AlaramPower];
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
