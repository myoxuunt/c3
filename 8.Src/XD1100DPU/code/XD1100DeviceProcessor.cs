using System;
using System.Collections;
using System.Data;
using System.Collections.Generic;
using System.Windows.Forms;
using C3.Communi;
using Xdgk.Common;
//using C3.Communi;
using Xdgk.GR.Common;
using NLog;

namespace XD1100DPU
{
    /// <summary>
    /// 
    /// </summary>
    public class XD1100DeviceProcessor : TaskProcessorBase
    {
        string KIND_FLUX = "FluxDevice";
        string KIND_HEAT = "HeatDevice";

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
        //private bool IsFluxDevice(IDevice device)
        //{
        //    bool r = false;
        //    Type t = device.GetType();
        //    object[] objs = t.GetCustomAttributes(typeof(DeviceKind), true);
        //    foreach (object obj in objs)
        //    {
        //        DeviceKindAttribute kind = obj as DeviceKindAttribute;
        //        if (StringHelper.Equal(kind.Name, KIND_FLUX))
        //        {
        //            r = true;
        //            break;
        //        }
        //    }
        //    return r;
        //}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        //private List<IFluxProvider> GetFluxProviderList(IDevice device)
        //private DeviceCollection GetFluxDevices(IDevice device)
        //{
        //    //List<IFluxProvider> r = new List<IFluxProvider>();
        //    DeviceCollection r = new DeviceCollection();

        //    DeviceCollection devices = device.Station.Devices;
        //    foreach (IDevice item in devices)
        //    {
        //        if (item != device &&
        //            IsFluxDevice(item)
        //            )
        //        {
        //            //r = (IFluxProvider)item;
        //            //break;
        //            r.Add(item);
        //        }
        //    }
        //    return r;
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="task"></param>
        /// <param name="pr"></param>
        public override void OnProcess(ITask task, IParseResult pr)
        {
            if (pr.IsSuccess)
            {
                string opera = task.Opera.Name;
                if (StringHelper.Equal(opera, XD1100OperaNames.ReadReal))
                {
                    ProcessReadReal(task, pr);
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

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="fluxProviderList"></param>
        ///// <returns></returns>
        //private bool IsFluxProviderDataValid(List<IFluxProvider> fluxProviderList)
        //{
        //    bool r = true;
        //    foreach (IFluxProvider fp in fluxProviderList)
        //    {
        //        if (fp.FluxPlace != FluxPlace.Unknown)
        //        {
        //            if (!IsFluxProviderDataValid(fp))
        //            {
        //                r = false;
        //                break;
        //            }
        //        }
        //    }
        //    return r;
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="fp"></param>
        ///// <returns></returns>
        //private bool IsFluxProviderDataValid(IFluxProvider fp)
        //{
        //    return fp.FluxDataDT != DateTime.MinValue;
        //}


        /// <summary>
        /// 
        /// </summary>
        /// <param name="d"></param>
        /// <param name="pr"></param>
        //private void ProcessReadReal(XD1100Device d, IParseResult pr, List<IFluxProvider> fluxProviderList)
        private void ProcessReadReal ( ITask task, IParseResult pr )
        {


                    //if (IsFluxProviderDataValid(fluxProviderList))
                    //{
                    //    ProcessReadReal(d, pr, fluxProviderList);
                    //}

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
            //data.IH1 = Convert.ToDouble (pr.Results ["IH1"]);
            //data.SH1 = Convert.ToDouble (pr.Results ["SH1"]);
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
            WarnWrapper ww = new WarnWrapper(listWarn);
            data.Warn = ww;


            XD1100Device d = (XD1100Device)task.Device;
            //List<IFluxProvider> fluxProviderList = GetFluxProviderList(d);
            DeviceCollection fluxDevices = d.Station.Devices.GetDevices(this.KIND_FLUX);
            fluxDevices = RemoveUnkonwnPlaceDevice(fluxDevices);
            bool hasFluxDevices = fluxDevices.Count > 0;
            bool hasFluxData = fluxDevices.HasData(HasDataOption.All);

            bool success = true;
            if (hasFluxDevices )
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
                        IData  last = hd.DeviceDataManager.Last;
                        ih += Convert.ToDouble(last.GetValue("InstantHeat"));
                        sh += Convert.ToDouble(last.GetValue("SumHeat"));
                        instantFlux += Convert.ToDouble(last.GetValue("InstantFlux"));
                        sumFlux += Convert.ToDouble(last.GetValue ("Sum"));
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

            //
            //
            //if (fluxProviderList != null)
            //foreach (IFluxProvider fluxProvider in fluxProviderList)
            //{
            //    switch (fluxProvider.FluxPlace)
            //    {
            //        case FluxPlace.FirstSide:
            //            data.I1 = Convert.ToSingle(fluxProvider.InstantFlux);
            //            data.S1 = Convert.ToInt32(fluxProvider.Sum);
            //            break;

            //        case FluxPlace.SecondSide:
            //            data.I2 = Convert.ToSingle(fluxProvider.InstantFlux);
            //            data.S2 = Convert.ToInt32(fluxProvider.Sum);
            //            break;

            //        case FluxPlace.RecruitSide:
            //            data.IR = Convert.ToSingle(fluxProvider.InstantFlux);
            //            data.SR = Convert.ToInt32(fluxProvider.Sum);
            //            break;

            //        default:
            //            break;
            //    }
            //}
            switch (d.HtmMode.ModeValue)
            {
                case ModeValue.Direct:
                    data.GT1 = data.GT2;
                    data.BT1 = data.BT2;
                    break;

                case ModeValue.Mixed:
                    data.BT1 = data.BT2;
                    break;
            }
            d.DeviceDataManager.Last = data;


            // save
            //
            int id = GuidHelper.ConvertToInt32(d.Guid);
            DBI.Instance.InsertXD1100Data(id, data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        private PumpStatus IsPumpRun(bool b)
        {
            return IsPumpRun(b ? PumpStatusEnum.Run : PumpStatusEnum.Stop);
        }
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="device"></param>
        /// <param name="pr"></param>
        public override void OnProcessUpload(IDevice device, IParseResult pr)
        {
            throw new NotImplementedException();
        }
    }

}
