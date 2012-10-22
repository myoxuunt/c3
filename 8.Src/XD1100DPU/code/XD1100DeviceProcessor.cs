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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        private IFluxProvider GetFluxProvider(IDevice device)
        {
            IFluxProvider r = null;
            DeviceCollection devices = device.Station.Devices;
            foreach (IDevice item in devices)
            {
                if (item is IFluxProvider && item != device)
                {
                    r = (IFluxProvider)item;
                    break;
                }
            }
            return r;
        }

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
                    XD1100Device d = (XD1100Device)task.Device;
                    IFluxProvider fluxProvider = GetFluxProvider(d);
                    ProcessReadReal(d, pr, fluxProvider);
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
        /// <param name="d"></param>
        /// <param name="pr"></param>
        private void ProcessReadReal(XD1100Device d, IParseResult pr, IFluxProvider fluxProvider)
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

            //
            //
            if (fluxProvider != null)
            {
                switch (fluxProvider.FluxPlace)
                {
                    case FluxPlace.FirstSide:
                        data.I1 = Convert.ToSingle(fluxProvider.InstantFlux);
                        data.S1 = Convert.ToInt32(fluxProvider.Sum);
                        break;

                    case FluxPlace.SecondSide:
                        data.I2 = Convert.ToSingle(fluxProvider.InstantFlux);
                        data.S2 = Convert.ToInt32(fluxProvider.Sum);
                        break;

                    case FluxPlace.RecruitSide:
                        data.IR = Convert.ToSingle(fluxProvider.InstantFlux);
                        data.SR = Convert.ToInt32(fluxProvider.Sum);
                        break;

                    default:
                        break;
                }
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
