using System;
using System.Text;
using System.Collections.Generic;
using C3.Communi;
using Xdgk.Common;

namespace XD100EDPU
{
    internal class Xd100eProcessor : TaskProcessorBase
    {

        private string
            READ_REAL = "ReadReal",
            READ_REAL_DI = "ReadRealDI";

        #region OnProcess
        /// <summary>
        /// 
        /// </summary>
        /// <param name="task"></param>
        /// <param name="pr"></param>
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

                if (data.IsComplete())
                {
                    double ir, sr;
                    bool hasRecuritFluxDevice;
                    string recuritEx;
                    bool success = GetFluxValues(xd100eDevice, FluxPlace.RecruitSide, out hasRecuritFluxDevice,
                        out ir, out sr, out recuritEx);

                    double if1, sf1;
                    bool hasFirstFluxDevice;
                    string firstEx;
                    bool success2 = GetFluxValues(xd100eDevice, FluxPlace.FirstSide, out hasFirstFluxDevice,
                        out if1, out sf1, out firstEx);

                    if (success && success2)
                    {
                        data.IR = ir;
                        data.SR = sr;
                        data.REx = recuritEx;

                        data.IF1 = if1;
                        data.SF1 = sf1;

                        if (hasFirstFluxDevice)
                        {
                            // xd100e ai5 == if1
                            //
                            data.AI5 = Convert.ToSingle(if1);
                            data.AI1 = Convert.ToSingle(GetGT1FromCRL_G(xd100eDevice));
                        }

                        data.DT = DateTime.Now;
                        xd100eDevice.DeviceDataManager.Last = data;

                        int deviceID = GuidHelper.ConvertToInt32(xd100eDevice.Guid);
                        Console.WriteLine("write xd100e id: " + deviceID);
                        DBI.Instance.InsertXd100eData(deviceID, data);
                    }
                }
            }
        }
        #endregion //OnProcess

        /// <summary>
        /// get crl-g gt1 property
        /// </summary>
        /// <param name="xd100eDevice"></param>
        /// <returns></returns>
        private double GetGT1FromCRL_G(Xd100e xd100eDevice)
        {
            double r = 0d;
            foreach (IDevice device in xd100eDevice.Station.Devices)
            {
                if (device is IGT1Provider)
                {
                    IGT1Provider gt1Pro = device as IGT1Provider;
                    if (gt1Pro.GT1DataDT != DateTime.MinValue)
                    {
                        r = gt1Pro.GT1;
                    }
                }
            }
            return r;
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

        #region GetFluxValues
        /// <summary>
        /// 
        /// </summary>
        /// <param name="xd100e"></param>
        /// <param name="ir"></param>
        /// <param name="sr"></param>
        /// <returns></returns>
        private bool GetFluxValues(Xd100e xd100e, FluxPlace fluxPlace, out bool hasFluxProviderDevice, 
            out double ir, out double sr, out string recuritEx)
        {
            hasFluxProviderDevice = false;
            ir = 0d;
            sr = 0d;
            recuritEx = string.Empty;

            IStation st = xd100e.Station;
            List<IFluxProvider> fluxProviderList = GetFluxProviderList(st, fluxPlace);

            if (fluxProviderList.Count == 0)
            {
                return true;
            }

            hasFluxProviderDevice = true;
            if (!IsFluxDataValid(fluxProviderList))
            {
                return false;
            }

            ir = CalcIR(fluxProviderList);
            sr = CalcSR(fluxProviderList);
            recuritEx = GetRecuritEx(fluxProviderList);
            return true;
        }
        #endregion //GetFluxValues

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
        private string GetRecuritEx(List<IFluxProvider> fluxProviderList)
        {
            StringBuilder sb = new StringBuilder();
            foreach (IFluxProvider fp in fluxProviderList)
            {
                string s = string.Format(
                    "{0},{1},{2},{3}|",
                    fp.FluxDataDT,
                    ((IDevice)fp).Name,
                    fp.InstantFlux, 
                    fp.Sum
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

        #region CalcIR
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fluxProviderList"></param>
        /// <returns></returns>
        private double CalcIR(List<IFluxProvider> fluxProviderList)
        {
            double r = 0d;
            foreach (IFluxProvider fp in fluxProviderList)
            {
                r += fp.InstantFlux;
            }
            return r;
        }
        #endregion //CalcIR

        #region CalcSR
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fluxProviderList"></param>
        /// <returns></returns>
        private double CalcSR(List<IFluxProvider> fluxProviderList)
        {
            double r = 0d;
            foreach (IFluxProvider fp in fluxProviderList)
            {
                r += fp.Sum;
            }
            return r;
        }
        #endregion //CalcSR

        #region GetFluxProviderList
        /// <summary>
        /// 
        /// </summary>
        /// <param name="xd100e"></param>
        /// <returns></returns>
        //private List<IFluxProvider> GetFluxProviderList(Xd100e xd100e)
        private List<IFluxProvider> GetFluxProviderList(IStation st, FluxPlace fluxPlace)
        {
            //IStation st = xd100e.Station;

            List<IFluxProvider> list = new List<IFluxProvider>();
            //foreach (IDevice device in st.Devices)
            foreach (IDevice device in st.Devices)
            {
                if (device is IFluxProvider)
                {
                    IFluxProvider fp = device as IFluxProvider;
                    //if (fp.FluxPlace == FluxPlace.RecruitSide)
                    if ( fp.FluxPlace == fluxPlace )
                    {
                        list.Add(fp);
                    }
                }
            }
            return list;
        }
        #endregion //GetFluxProviderList

        #region IsFluxDataValid
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fluxProviderList"></param>
        /// <returns></returns>
        private bool IsFluxDataValid(List<IFluxProvider> fluxProviderList)
        {
            // all fp is valid then return true
            //
            bool r = true;
            foreach (IFluxProvider fp in fluxProviderList)
            {
                if (!IsFluxDataValid(fp))
                {
                    r = false;
                    break;
                }
            }
            return r;
        }
        #endregion //IsFluxDataValid

        #region IsFluxDataValid
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fp"></param>
        /// <returns></returns>
        private bool IsFluxDataValid(IFluxProvider fp)
        {
            return (fp.FluxDataDT != DateTime.MinValue);
        }
        #endregion //IsFluxDataValid


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
        /// <param name="pr"></param>
        public override void OnProcessUpload(IDevice device, IParseResult pr)
        {
            //throw new NotImplementedException();
        }
        #endregion //OnProcessUpload
    }

}
