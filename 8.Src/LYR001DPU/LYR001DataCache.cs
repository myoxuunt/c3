
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using C3.Communi;
using Xdgk.Common;
using Xdgk.GR.Common;


namespace LYR001DPU
{
    internal class LYR001DataCache
    {
        private DateTime _createDT = DateTime.Now;

        #region AnalogData
        /// <summary>
        /// 
        /// </summary>
        internal LYR001AnalogData AnalogData
        {
            get
            {
                return _analogData;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("AnalogData");
                }

                _analogData = value;
                this._isSetAnalog = true;
            }
        } private LYR001AnalogData _analogData;
        #endregion //AnalogData

        #region PumpStatusData
        /// <summary>
        /// 
        /// </summary>
        internal LYR001StatusData StatusData
        {
            get
            {
                return _statusData;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("PumpStatusData");
                }

                _statusData = value;
                this._isSetStatus = true;
            }
        } private LYR001StatusData _statusData;
        #endregion //PumpStatusData

        #region IsSetAnalog
        /// <summary>
        /// 
        /// </summary>
        internal bool IsSetAnalog
        {
            get
            {
                return _isSetAnalog;
            }
        } private bool _isSetAnalog;
        #endregion //IsSetAnalog

        #region IsSetStatus
        /// <summary>
        /// 
        /// </summary>
        internal bool IsSetStatus
        {
            get
            {
                return _isSetStatus;
            }
        } private bool _isSetStatus;
        #endregion //IsSetStatus

        internal bool IsComplete()
        {
            return IsSetAnalog && IsSetStatus;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal bool IsTimeout()
        {
            TimeSpan ts = DateTime.Now - this._createDT;
            bool b = (ts > TimeSpan.Zero) &&
                (ts < TimeSpan.FromMinutes(5d));

            return !b;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal GRData ToGRData()
        {
            //throw new NotImplementedException();
            GRData r = new GRData();
            r.DT = this.AnalogData.DT;
            r.BP1 = this.AnalogData.BP1;
            r.BP2 = this.AnalogData.BP2;
            r.BT1 = this.AnalogData.BT1;
            r.BT2 = this.AnalogData.BT2;
            r.GP1 = this.AnalogData.GP1;
            r.GP2 = this.AnalogData.GP2;
            r.GT1 = this.AnalogData.GT1;
            r.GT2 = this.AnalogData.GT2;
            r.I1 = this.AnalogData.I1;
            r.IR = this.AnalogData.IR;
            r.OD = Convert.ToInt32(this.AnalogData.OD);
            r.OT = this.AnalogData.OT;
            r.S1 = Convert.ToInt32(this.AnalogData.S1);
            r.SR = Convert.ToInt32(this.AnalogData.SR);
            r.WL = this.AnalogData.WL;


            r.CM1 = this.StatusData.CM1;
            r.CM2 = this.StatusData.CM2;
            r.CM3 = this.StatusData.CM3;

            r.RM1 = this.StatusData.RM1;
            r.RM2 = this.StatusData.RM2;
            //this.StatusData.AlarmList = 
            r.Warn = new WarnWrapper(this.StatusData.AlarmList);

            return r;
        }
    }

}
