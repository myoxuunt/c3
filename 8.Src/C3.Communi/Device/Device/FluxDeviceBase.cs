
using System;
using System.Diagnostics;
using Xdgk.Common;
using C3.Communi.SimpleDPU;
using NLog;

namespace C3.Communi
{
    /// <summary>
    /// 
    /// </summary>
    abstract public class FluxDeviceBase : DeviceBase, IFluxProvider
    {
        static Logger log = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// 
        /// </summary>
        protected FluxDeviceBase()
        {
            // init
            //
            object obj = GetFluxPlaceParameter();
        }


        #region IFluxProvider ≥…‘±
        /// <summary>
        /// 
        /// </summary>
        public DateTime FluxDataDT
        {
            get
            {
                IData last = this.DeviceDataManager.Last;
                if (last != null)
                {
                    return last.DT;
                }
                else
                {
                    return DateTime.MinValue;
                }
            }
        }

        public double InstantFlux
        {
            get
            {
                IData last = this.DeviceDataManager.Last;
                if (last != null)
                {
                    FlowmeterData d = last as FlowmeterData;
                    Debug.Assert(d != null, "is not flowmeter data");
                    if (d != null)
                    {
                        return d.InstantFlux;
                    }
                }
                return 0f;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public double Sum
        {
            get
            {
                IData last = this.DeviceDataManager.Last;
                if (last != null)
                {
                    FlowmeterData d = last as FlowmeterData;
                    Debug.Assert(d != null, "is not flowmeter data");
                    if (d != null)
                    {
                        return d.Sum;
                    }
                }
                return 0f;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public FluxPlace FluxPlace
        {
            get
            {
                IParameter p = GetFluxPlaceParameter();
                FluxPlace fp = (FluxPlace)p.Value;
                return fp;
            }
            set
            {
                IParameter p = GetFluxPlaceParameter();
                p.Value = value;
            }
        }

        internal const string PN_FLUXPLACE = "fluxPlace";
        private const int PO_FLUXPLACE = 10;

        private IParameter GetFluxPlaceParameter()
        {
            IParameter p = this.GeneralGroup.Parameters[PN_FLUXPLACE];
            if (p == null)
            {
                p = new EnumParameter(PN_FLUXPLACE, typeof(FluxPlace), FluxPlace.FirstSide, PO_FLUXPLACE);
                p.Text = "Œª÷√";
                this.GeneralGroup.Parameters.Add(p);
            }

            return p;
        }
        #endregion
    }

    /// <summary>
    /// 
    /// </summary>
    abstract public class FluxDeviceFactoryBase : DeviceFactoryBase
    {
        static NLog.Logger log = NLog.LogManager.GetCurrentClassLogger();

        public FluxDeviceFactoryBase(IDPU dpu)
            : base(dpu)
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fluxDevice"></param>
        /// <param name="deviceSource"></param>
        protected void SetDeviceProperties(FluxDeviceBase fluxDevice, IDeviceSource deviceSource)
        {
            SimpleDeviceSource source = (SimpleDeviceSource)deviceSource;
            fluxDevice.Address = source.Address;
            fluxDevice.Name = source.DeviceName;
            fluxDevice.DeviceSource = deviceSource;
            fluxDevice.DeviceType = this.Dpu.DeviceType;
            fluxDevice.Dpu = this.Dpu;
            fluxDevice.Guid = source.Guid;
            fluxDevice.StationGuid = source.StationGuid;
            fluxDevice.FluxPlace = GetFluxPlace(source.DeviceExtendParameters);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="deviceExtendParameters"></param>
        /// <returns></returns>
        private FluxPlace GetFluxPlace(string deviceExtendParameters)
        {
            FluxPlace p = FluxPlace.Unknown;
            StringStringDictionary ssd = StringStringDictionaryConverter.Parse(deviceExtendParameters);
            if (ssd.ContainsKey(FluxDeviceBase.PN_FLUXPLACE))
            {
                string place = ssd[FluxDeviceBase.PN_FLUXPLACE];

                try
                {
                    p = (FluxPlace)Enum.Parse(typeof(FluxPlace), place);
                }
                catch (Exception ex)
                {
                    log.Error(ex.ToString());
                }
            }
            return p;
        }
    }

     

}
