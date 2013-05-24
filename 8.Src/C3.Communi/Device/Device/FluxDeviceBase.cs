
using System;
using System.Diagnostics;
using Xdgk.Common;
using C3.Communi.SimpleDPU;
using NLog;

namespace C3.Communi
{
    //abstract public class HeatDeviceBase : DeviceBase, IPlace 
    //{
    //    #region IPlace 成员
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public FluxPlace Place
    //    {
    //        get
    //        {
    //            return _fluxPlace;
    //        }
    //        set
    //        {
    //            _fluxPlace = value;
    //        }
    //    } private FluxPlace _fluxPlace = FluxPlace.Unknown;

    //    #endregion
    //}

    /// <summary>
    /// 
    /// </summary>
    //abstract public class PlaceDeviceBase : DeviceBase, IFluxProvider
    abstract public class PlaceDeviceBase : DeviceBase, IPlace  
    {
        static Logger log = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// 
        /// </summary>
        protected PlaceDeviceBase()
        {
            // init
            //
            object obj = GetFluxPlaceParameter();
        }


        #region IFluxProvider 成员
        ///// <summary>
        ///// 
        ///// </summary>
        //public DateTime FluxDataDT
        //{
        //    get
        //    {
        //        IData last = this.DeviceDataManager.Last;
        //        if (last != null)
        //        {
        //            return last.DT;
        //        }
        //        else
        //        {
        //            return DateTime.MinValue;
        //        }
        //    }
        //}

        //public double InstantFlux
        //{
        //    get
        //    {
        //        IData last = this.DeviceDataManager.Last;
        //        if (last != null)
        //        {
        //            FlowmeterData d = last as FlowmeterData;
        //            Debug.Assert(d != null, "is not flowmeter data");
        //            if (d != null)
        //            {
        //                return d.InstantFlux;
        //            }
        //        }
        //        return 0f;
        //    }
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        //public double Sum
        //{
        //    get
        //    {
        //        IData last = this.DeviceDataManager.Last;
        //        if (last != null)
        //        {
        //            FlowmeterData d = last as FlowmeterData;
        //            Debug.Assert(d != null, "is not flowmeter data");
        //            if (d != null)
        //            {
        //                return d.Sum;
        //            }
        //        }
        //        return 0f;
        //    }
        //}

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
                p = new EnumParameter(PN_FLUXPLACE, typeof(FluxPlace), FluxPlace.Unknown, PO_FLUXPLACE);
                p.Text = "位置";
                this.GeneralGroup.Parameters.Add(p);
            }

            return p;
        }
        #endregion

        #region IPlace 成员

        public FluxPlace Place
        {
            get
            {
                return this.FluxPlace;
            }
            set
            {
                this.FluxPlace = value;
            }
        }

        #endregion
    }

    /// <summary>
    /// 
    /// </summary>
    abstract public class PlaceDeviceFactoryBase : DeviceFactoryBase
    {
        static NLog.Logger log = NLog.LogManager.GetCurrentClassLogger();

        public PlaceDeviceFactoryBase(IDPU dpu)
            : base(dpu)
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fluxDevice"></param>
        /// <param name="deviceSource"></param>
        protected override void  SetDeviceProperties(IDevice fluxDevice, IDeviceSource deviceSource)
        {
            PlaceDeviceBase s = (PlaceDeviceBase)fluxDevice;
            base.SetDeviceProperties(fluxDevice, deviceSource);

            DeviceSourceBase source = (DeviceSourceBase)deviceSource;
            s.FluxPlace = GetFluxPlace(source.DeviceExtendParameters);
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
            if (ssd.ContainsKey(PlaceDeviceBase.PN_FLUXPLACE))
            {
                string place = ssd[PlaceDeviceBase.PN_FLUXPLACE];

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
