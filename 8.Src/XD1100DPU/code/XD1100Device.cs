using System;
using C3.Communi;
using Xdgk.Common;

namespace XD1100DPU
{
    internal class XD1100Device : DeviceBase , IOutside
    {
        private const string PN_HEATTRANSFERMODE = "heatTransferMode";
        private const int PO_HEATTRANSFERMODE = 1;

        #region XD1100Device
        /// <summary>
        /// 
        /// </summary>
        public XD1100Device()
        {
            // init 
            //
            IParameter p = null;
            p = GetHeatTransferModeParameter();
        }
        #endregion //XD1100Device

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
                ModeValue mv = (ModeValue)GetHeatTransferModeParameter().Value ;
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
                    if ( deviceOtp.Outside == this )
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
                    XD1100Data data = (XD1100Data)last;
                    r = data.OT;
                }
                return r;
            }
        }
        #endregion //OutsideTemperature
    }

}
