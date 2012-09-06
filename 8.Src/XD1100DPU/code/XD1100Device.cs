
using System;
using System.Data.SqlClient;
using System.Collections;
using System.Data;
using System.Collections.Generic;
using System.Windows.Forms;
using C3.Communi;
using Xdgk.Common;
using NLog;
using C3.Data;


namespace XD1100DPU
{
    internal class XD1100Device : DeviceBase , IOutside
    {
        private const string PN_HEATTRANSFERMODE = "heatTransferMode";
        private const int PO_HEATTRANSFERMODE = 1;

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

                p.Text = "htm text";
                this.GeneralGroup.Parameters.Add(p);
            }
            return p;
        }

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

#region IOutside ≥…‘±

        /// <summary>
        /// 
        /// </summary>
        public float OutsideTemperature
        {
            get
            {
                float r = 0f;
                IDeviceData last = this.DeviceDataManager.Last;
                if (last != null)
                {
                    XD1100Data data = (XD1100Data)last;
                    r = data.OT;
                }
                return r;
            }
        }

#endregion
    }

}
