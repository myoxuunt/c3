
using System;
using System.Collections.Generic;
using System.Text;
using C3.Communi;
using Xdgk.GR.Common;
using Xdgk.Common;


namespace LYR001DPU
{
    internal class LYR001Device : DeviceBase, IOutside
    {
        internal enum StatusAndAlarmEnum
        {
            AlaramPower,
        }

        private const string PN_HEATTRANSFERMODE = "heatTransferMode";
        private const int PO_HEATTRANSFERMODE = 1;

        #region LYR001Device
        /// <summary>
        /// 
        /// </summary>
        public LYR001Device()
        {
            // init 
            //
            IParameter p = null;
            p = GetHeatTransferModeParameter();
        }
        #endregion //LYR001Device

        ///// <summary>
        ///// 
        ///// </summary>
        //public bool IsUseExternalFlux
        //{
        //    get
        //    {
        //    }
        //    set
        //    {

        //    }
        //}

        //private IParameter GetIsUseExternalFluxParameter()
        //{

        //}

        #region HtmMode
        /// <summary>
        /// 
        /// </summary>
        public HeatTransferMode HtmMode
        {
            get
            {
                ModeValue mv = (ModeValue)GetHeatTransferModeParameter().Value;
                return HeatTransferMode.Parse(mv);
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("HtmMode");
                }
                IParameter p = GetHeatTransferModeParameter();
                p.Value = value.ModeValue;
            }
        }
        #endregion //HtmMode

        #region GetHeatTransferModeParameter
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private IParameter GetHeatTransferModeParameter()
        {
            IParameter p = this.GeneralGroup.Parameters[PN_HEATTRANSFERMODE];

            if (p == null)
            {
                p = new EnumParameter(
                        PN_HEATTRANSFERMODE,
                        typeof(ModeValue),
                        ModeValue.Mixed,
                        PO_HEATTRANSFERMODE);

                p.Text = "ªª»»∑Ω Ω";
                this.GeneralGroup.Parameters.Add(p);
            }
            return p;
        }
        #endregion //GetHeatTransferModeParameter

        #region GetLazyDataFieldValue
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public override object GetLazyDataFieldValue(string name)
        {
            if (StringHelper.Equal(name, "dt"))
            {
                return DateTime.Now;
            }
            else if (StringHelper.Equal(name, "ot"))
            {
                return OutsideTemperatureProviderManager.GetStandardOutsideTemperature(this);
            }
            else if (StringHelper.Equal(name, "mode"))
            {
                // outside temperature mode value
                //
                // 0 - local
                // 1 - remote
                //
                int modeValue = 1;
                IOutsideTemperatureProvider provider = OutsideTemperatureProviderManager.Provider;
                if (provider is DeviceOTProvider)
                {
                    DeviceOTProvider deviceOtp = (DeviceOTProvider)provider;
                    if (deviceOtp.Outside == this)
                    {
                        modeValue = 0;
                    }
                }
                return modeValue;
            }

            return base.GetLazyDataFieldValue(name);
        }
        #endregion //GetLazyDataFieldValue


        #region OutsideTemperature
        /// <summary>
        /// 
        /// </summary>
        public float OutsideTemperature
        {
            get
            {
                float r = 0f;
                IData last = this.DeviceDataManager.Last;
                if (last != null)
                {
                    GRData data = (GRData)last;
                    r = data.OT;
                }
                return r;
            }
        }
        #endregion //OutsideTemperature

        /// <summary>
        /// 
        /// </summary>
        public Dictionary<StatusAndAlarmEnum, bool> StatusAndAlarmDictionary
        {
            get
            {
                if (_statusAndAlaramDictionary == null)
                {
                    _statusAndAlaramDictionary = new Dictionary<StatusAndAlarmEnum, bool>();
                }
                return _statusAndAlaramDictionary;
            }
        }
        private Dictionary<StatusAndAlarmEnum, bool> _statusAndAlaramDictionary;

        /// <summary>
        /// 
        /// </summary>
        internal LYR001DataCacheManager DataCacheManager
        {
            get
            {
                return _dataCacheManager;
            }
        } private LYR001DataCacheManager _dataCacheManager = new LYR001DataCacheManager();
    }

}
