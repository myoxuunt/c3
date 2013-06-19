using System;
using System.Diagnostics;
using System.Text;
using System.Collections.Generic;
using C3.Communi;
using Xdgk.Common;
using Xdgk.Common.Calc;

namespace XD100EDPU
{
    internal class Xd100eProcessor : TaskProcessorBase
    {

        private string
            READ_REAL = "ReadReal",
            READ_REAL_DI = "ReadRealDI";

            const string KIND = "HeatDevice";
            const string KIND_FLUX_DEVICE = "FluxDevice";

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
                //Xd100eData data = Xd100eData.GetCachedData();

                Xd100e xd100eDevice = (Xd100e)task.Device;
                Xd100eData data = xd100eDevice.GetCachedData();

                Console.WriteLine("ori xd100e id: " + GuidHelper.ConvertToInt32(xd100eDevice.Guid));

                string opera = task.Opera.Name;

                if (StringHelper.Equal(opera, READ_REAL))
                {
                    for (int i = Xd100eData.BeginChannelNO; i <= Xd100eData.EndChannelNO; i++)
                    {
                        int no = i;
                        string channelName = GetChannelName(no);

                        float val = Convert.ToSingle(pr.Results[channelName]);
                        val /= 100f;

                        data.SetChannelDataAI(no, val);
                        data.IsSetAI = true;
                    }
                }

                else if (StringHelper.Equal(opera, READ_REAL_DI))
                {
                    //XD100EData data = xd100eDevice.XD100EData;
                    for (int i = Xd100eData.BeginChannelNO; i <= Xd100eData.EndChannelNO; i++)
                    {
                        int no = i;
                        string channelName = GetChannelName(no);
                        byte value = Convert.ToByte(pr.Results[channelName]);
                        data.SetChannelDataDI(no, value > 0);
                        data.IsSetDI = true;
                    }

                }

                //
                //
                if (data.IsComplete())
                {
                    double ir = 0d, sr = 0d;
                    //bool hasRecuritFluxDevice;
                    string recuritEx = string.Empty;

                    //bool success = GetFluxValues(xd100eDevice, FluxPlace.RecruitSide, out hasRecuritFluxDevice,
                    //    out ir, out sr, out recuritEx);
                    
                    // get kind == fluxdevice device collection
                    //
                    DeviceCollection rDevices = xd100eDevice.Station.Devices.GetDevices(KIND_FLUX_DEVICE);
                    rDevices = Filter(rDevices, FluxPlace.RecruitSide);
                    bool hasRecuritFluxDevice = rDevices.Count > 0;
                    bool hasRecuritFluxData = rDevices.HasData(HasDataOption.All);
                    if (hasRecuritFluxData)
                    {
                        ir = Calc(rDevices, "InstantFlux", CalcType.Sum);
                        sr = Calc(rDevices, "Sum", CalcType.Sum);
                        recuritEx = GetRecuritEx(rDevices);
                    }
                    
                    // get recruite device collection
                    // get recruite data
                    // 
                    // get kind == heatdevcie device collection
                    // check has data
                    // [x] get place == first ?
                    // get device gt bt

                    double if1 = 0d, sf1 = 0d, gt1 = 0d, bt1 = 0d, ih1 = 0d, sh1=0d;
                    //bool hasFirstFluxDevice;
                    //string firstEx;
                    //bool success2 = GetFluxValues(xd100eDevice, FluxPlace.FirstSide, out hasFirstFluxDevice,
                    //    out if1, out sf1, out firstEx);

                    DeviceCollection hs = xd100eDevice.Station.Devices.GetDevices(KIND);
                    hs = Filter(hs, FluxPlace.FirstSide);

                    bool hasFirstFluxDevice = hs.Count > 0;
                    bool hasFirstFluxData = hs.HasData(HasDataOption.All);
                    if (hasFirstFluxData)
                    {
                        if1 = Calc(hs, "InstantFlux", CalcType.Sum);
                        sf1 = Calc(hs, "Sum", CalcType.Sum);
                        gt1 = Calc(hs, "GT", CalcType.Avg);
                        bt1 = Calc(hs, "BT", CalcType.Avg);
                        ih1 = Calc(hs, "InstantHeat", CalcType.Sum);
                        sh1 = Calc(hs, "SumHeat", CalcType.Sum);

                    }

                    bool success = true;
                    if (hasRecuritFluxDevice )
                    {
                        if (hasRecuritFluxData)
                        {
                            data.IR = ir;
                            data.SR = sr;
                            data.REx = recuritEx;
                        }
                        else
                        {
                            success = false;
                        }
                    }

                    // 
                    //
                    if (success)
                    {
                        if (hasFirstFluxDevice)
                        {
                            if (hasFirstFluxData)
                            {
                                // xd100e ai5 == if1
                                //
                                // ai1 - gt1, ai2 - bt1
                                //
                                data.IF1 = if1;
                                data.SF1 = sf1;

                                data.AI5 = Convert.ToSingle(if1);
                                data.AI1 = Convert.ToSingle(gt1);
                                data.AI2 = Convert.ToSingle(bt1);

                                data.IH1 = ih1;
                                data.SH1 = sh1;
                            }
                            else
                            {
                                success = false;
                            }
                        }
                    }

                    if (success)
                    {
                        data.DT = DateTime.Now;
                        xd100eDevice.DeviceDataManager.Last = data;

                        int deviceID = GuidHelper.ConvertToInt32(xd100eDevice.Guid);
                        Console.WriteLine("write xd100e id: " + deviceID);
                        DBI.Instance.InsertXd100eData(deviceID, data);
                    }

                    //if (success && success2)
                    //{
                    //    data.IR = ir;
                    //    data.SR = sr;
                    //    data.REx = recuritEx;

                    //    data.IF1 = if1;
                    //    data.SF1 = sf1;

                    //    if (hasFirstFluxDevice)
                    //    {
                    //        // xd100e ai5 == if1
                    //        //
                    //        // ai1 - gt1, ai2 - bt1
                    //        //
                    //        data.AI5 = Convert.ToSingle(if1);
                    //        data.AI1 = Convert.ToSingle(GetGT1FromCRL_G(xd100eDevice));
                    //        data.AI2 = Convert.ToSingle(GetBT1FromCRL_G(xd100eDevice));
                    //    }

                    //    data.DT = DateTime.Now;
                    //    xd100eDevice.DeviceDataManager.Last = data;

                    //    int deviceID = GuidHelper.ConvertToInt32(xd100eDevice.Guid);
                    //    Console.WriteLine("write xd100e id: " + deviceID);
                    //    DBI.Instance.InsertXd100eData(deviceID, data);
                    //}
                }
            }
        }
        #endregion //OnProcess

        private double GetBT1FromCRL_G(Xd100e xd100eDevice)
        {
            double r = 0d;
            /*
            foreach (IDevice device in xd100eDevice.Station.Devices)
            {
                if (device is IBT1Provider)
                {
                    IBT1Provider bt1Pro = device as IBT1Provider;
                    if (bt1Pro.BT1DataDT != DateTime.MinValue)
                    {
                        r = bt1Pro.BT1;
                    }
                }
            }
            */
            // 1. get heat kind device collection
            // 2. get device collection with place != unknown
            // 3. if device count > 1, select first device
            // 4. get device.bt1
            DeviceCollection heatDevices = xd100eDevice.Station.Devices.GetDevices(KIND);
            RemoveUnknownPlaceDevice(heatDevices);

            bool hasData = heatDevices.HasData(HasDataOption.All);
            if (hasData)
            {
                r = Calc(heatDevices, "BT", CalcType.Avg);
            }
            return r;
        }

        /// <summary>
        /// get crl-g gt1 property
        /// </summary>
        /// <param name="xd100eDevice"></param>
        /// <returns></returns>
        private double GetGT1FromCRL_G(Xd100e xd100eDevice)
        {
            double r = 0d;
            DeviceCollection heatDevices = xd100eDevice.Station.Devices.GetDevices(KIND);
            RemoveUnknownPlaceDevice(heatDevices);

            bool hasData = heatDevices.HasData(HasDataOption.All);
            if (hasData)
            {
                r = Calc(heatDevices, "GT", CalcType.Avg);
            }
            return r;
        }

        private double Calc(DeviceCollection devices, string propertyName, CalcType calcType)
        {
            Calculator c = new Calculator(calcType);

            foreach (IDevice device in devices)
            {
                IData last = device.DeviceDataManager.Last;
                Debug.Assert(last != null);

                object obj = ReflectionHelper.GetPropertyValue(last, propertyName);
                double val = Convert.ToDouble(obj);

                c.Add(val);
            }

            return c.Calc();
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

        private void RemoveUnknownPlaceDevice(DeviceCollection devices)
        {
            for (int i = devices.Count -1; i >= 0; i--)
            {
                IDevice device = devices[i];
                if (device is PlaceDeviceBase)
                {
                    PlaceDeviceBase placeDevice = device as PlaceDeviceBase;
                    if (placeDevice.Place == FluxPlace.Unknown)
                    {
                        devices.Remove(device);
                    }
                }
            }
        }


        //#region GetFirstSideValues
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="xd100eDevice"></param>
        ///// <param name="if1"></param>
        ///// <param name="sf1"></param>
        ///// <returns></returns>
        //private bool GetFirstSideValues(Xd100e xd100eDevice, out double if1, out double sf1)
        //{
        //    bool hasSide1FluxProvider = false;
        //    bool r = false;

        //    if1 = 0d;
        //    sf1 = 0d;
        //    IStation st = xd100eDevice.Station;

        //    foreach (IDevice d in st.Devices)
        //    {
        //        if (d is IFluxProvider)
        //        {
        //            IFluxProvider fp = d as IFluxProvider;
        //            if (fp.FluxPlace == FluxPlace.FirstSide)
        //            {
        //                hasSide1FluxProvider = true;
        //                if (fp.FluxDataDT != DateTime.MinValue)
        //                {
        //                    if1 = fp.InstantFlux;
        //                    sf1 = fp.Sum;
        //                    r = true;
        //                }
        //            }
        //        }
        //    }

        //    if (!r && !hasSide1FluxProvider)
        //    {
        //        r = true;
        //    }
        //    return r;
        //}
        //#endregion //GetFirstSideValues

        //#region GetFluxValues
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="xd100e"></param>
        ///// <param name="ir"></param>
        ///// <param name="sr"></param>
        ///// <returns></returns>
        //private bool GetFluxValues(Xd100e xd100e, FluxPlace fluxPlace, out bool hasFluxProviderDevice, 
        //    out double ir, out double sr, out string recuritEx)
        //{
        //    hasFluxProviderDevice = false;
        //    ir = 0d;
        //    sr = 0d;
        //    recuritEx = string.Empty;

        //    IStation st = xd100e.Station;
        //    List<IFluxProvider> fluxProviderList = GetFluxProviderList(st, fluxPlace);

        //    if (fluxProviderList.Count == 0)
        //    {
        //        return true;
        //    }

        //    hasFluxProviderDevice = true;
        //    if (!IsFluxDataValid(fluxProviderList))
        //    {
        //        return false;
        //    }

        //    ir = CalcIR(fluxProviderList);
        //    sr = CalcSR(fluxProviderList);
        //    //recuritEx = GetRecuritEx(fluxProviderList);
        //    return true;
        //}
        //#endregion //GetFluxValues

        #region GetRecuritEx
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fluxProviderList"></param>
        /// <remarks>
        /// recurit ex message:
        /// 1. DT
        /// 2. Device.Name
        /// 3. IR
        /// 4. SR
        /// </remarks>
        /// <returns></returns>
        //private string GetRecuritEx(List<IFluxProvider> fluxProviderList)
        private string GetRecuritEx(DeviceCollection devices)
        {
            //DeviceCollection devices = null;
            StringBuilder sb = new StringBuilder();
            //foreach (IFluxProvider fp in fluxProviderList)
            foreach ( IDevice device in devices )
            {
                IData last = device.DeviceDataManager.Last;
                string s = string.Format(
                    "{0},{1},{2},{3}|",
                    /*
                    fp.FluxDataDT,
                    ((IDevice)fp).Name,
                    fp.InstantFlux, 
                    fp.Sum
                     */
                    last.GetValue("DT"),
                    device.Name,
                    last.GetValue("InstantFlux"),
                    last.GetValue("Sum")
                    );
                sb.Append(s);
            }

            // remove last '|' char
            //
            if (sb.Length > 0)
            {
                sb.Remove(sb.Length - 1, 1);
            }
            return sb.ToString();
        }
        #endregion //GetRecuritEx

        //#region CalcIR
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="fluxProviderList"></param>
        ///// <returns></returns>
        //private double CalcIR(List<IFluxProvider> fluxProviderList)
        //{
        //    double r = 0d;
        //    foreach (IFluxProvider fp in fluxProviderList)
        //    {
        //        r += fp.InstantFlux;
        //    }
        //    return r;
        //}
        //#endregion //CalcIR

        //#region CalcSR
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="fluxProviderList"></param>
        ///// <returns></returns>
        //private double CalcSR(List<IFluxProvider> fluxProviderList)
        //{
        //    double r = 0d;
        //    foreach (IFluxProvider fp in fluxProviderList)
        //    {
        //        r += fp.Sum;
        //    }
        //    return r;
        //}
        //#endregion //CalcSR

        //#region GetFluxProviderList
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="xd100e"></param>
        ///// <returns></returns>
        ////private List<IFluxProvider> GetFluxProviderList(Xd100e xd100e)
        //private List<IFluxProvider> GetFluxProviderList(IStation st, FluxPlace fluxPlace)
        //{
        //    //IStation st = xd100e.Station;

        //    List<IFluxProvider> list = new List<IFluxProvider>();
        //    //foreach (IDevice device in st.Devices)
        //    foreach (IDevice device in st.Devices)
        //    {
        //        if (device is IFluxProvider)
        //        {
        //            IFluxProvider fp = device as IFluxProvider;
        //            //if (fp.FluxPlace == FluxPlace.RecruitSide)
        //            if ( fp.FluxPlace == fluxPlace )
        //            {
        //                list.Add(fp);
        //            }
        //        }
        //    }
        //    return list;
        //}
        //#endregion //GetFluxProviderList

        //#region IsFluxDataValid
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="fluxProviderList"></param>
        ///// <returns></returns>
        //private bool IsFluxDataValid(List<IFluxProvider> fluxProviderList)
        //{
        //    // all fp is valid then return true
        //    //
        //    bool r = true;
        //    foreach (IFluxProvider fp in fluxProviderList)
        //    {
        //        if (!IsFluxDataValid(fp))
        //        {
        //            r = false;
        //            break;
        //        }
        //    }
        //    return r;
        //}
        //#endregion //IsFluxDataValid

        //#region IsFluxDataValid
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="fp"></param>
        ///// <returns></returns>
        //private bool IsFluxDataValid(IFluxProvider fp)
        //{
        //    return (fp.FluxDataDT != DateTime.MinValue);
        //}
        //#endregion //IsFluxDataValid


        #region GetChannelName
        /// <summary>
        /// 
        /// </summary>
        /// <param name="no"></param>
        /// <returns></returns>
        private string GetChannelName(int no)
        {
            string r = string.Format("Channal{0}Value", no);
            return r;
        }
        #endregion //GetChannelName

        #region OnProcessUpload
        /// <summary>
        /// 
        /// </summary>
        /// <param name="device"></param>
        /// <param name="parseResult"></param>
        public override void OnProcessUpload(IDevice device, IParseResult pr)
        {
            //throw new NotImplementedException();
        }
        #endregion //OnProcessUpload
    }

}
