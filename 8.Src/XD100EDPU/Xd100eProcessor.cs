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
                    string recuritEx;
                    bool success = GetRecruitValues(xd100eDevice, out ir, out sr, out recuritEx);

                    if (success)
                    {
                        data.IR = ir;
                        data.SR = sr;
                        data.REx = recuritEx;

                        data.DT = DateTime.Now;
                        xd100eDevice.DeviceDataManager.Last = data;

                        int deviceID = GuidHelper.ConvertToInt32(xd100eDevice.Guid);
                        Console.WriteLine("write xd100e id: " + deviceID);
                        DBI.Instance.InsertXd100eData(deviceID, data);
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xd100e"></param>
        /// <param name="ir"></param>
        /// <param name="sr"></param>
        /// <returns></returns>
        private bool GetRecruitValues(Xd100e xd100e, out double ir, out double sr, out string recuritEx)
        {
            ir = 0d;
            sr = 0d;
            recuritEx = string.Empty;

            IStation st = xd100e.Station;
            List<IFluxProvider> fluxProviderList = GetFluxProviderList(xd100e);

            if (fluxProviderList.Count == 0)
            {
                return true;
            }

            if (!IsFluxDataValid(fluxProviderList))
            {
                return false;
            }

            ir = CalcIR(fluxProviderList);
            sr = CalcSR(fluxProviderList);
            recuritEx = GetRecuritEx(fluxProviderList);
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fluxProviderList"></param>
        /// <returns></returns>
        private string GetRecuritEx(List<IFluxProvider> fluxProviderList)
        {
            StringBuilder sb = new StringBuilder();
            foreach (IFluxProvider fp in fluxProviderList)
            {
                string s = string.Format(
                    "{0},{1},{2},{3}|",
                    fp.FluxDataDT, fp.FluxPlace,
                    fp.InstantFlux, fp.Sum);
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xd100e"></param>
        /// <returns></returns>
        private List<IFluxProvider> GetFluxProviderList(Xd100e xd100e)
        {
            IStation st = xd100e.Station;

            List<IFluxProvider> list = new List<IFluxProvider>();
            foreach (IDevice device in st.Devices)
            {
                if (device is IFluxProvider)
                {
                    IFluxProvider fp = device as IFluxProvider;
                    if (fp.FluxPlace == FluxPlace.RecruitSide)
                    {
                        list.Add(fp);
                    }
                }
            }
            return list;
        }

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fp"></param>
        /// <returns></returns>
        private bool IsFluxDataValid(IFluxProvider fp)
        {
            return (fp.FluxDataDT != DateTime.MinValue);
        }


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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="device"></param>
        /// <param name="pr"></param>
        public override void OnProcessUpload(IDevice device, IParseResult pr)
        {

            //throw new NotImplementedException();
        }
    }

}
