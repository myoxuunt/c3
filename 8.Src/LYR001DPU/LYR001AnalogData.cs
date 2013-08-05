
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using C3.Communi;
using Xdgk.Common;
using Xdgk.GR.Common;

namespace LYR001DPU
{
    internal class LYR001AnalogData
    {
        /// <summary>
        /// 
        /// </summary>
        internal LYR001AnalogData()
        {
            _dt = DateTime.Now;
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime DT
        {
            get { return _dt; }
        }  private DateTime _dt;

        internal float GT1 = 0f;
        internal float BT1 = 0f;
        internal float GT2 = 0f;
        internal float BT2 = 0f;
        internal float OT = 0f;
        internal float GP1 = 0f;
        internal float BP1 = 0f;
        internal float WL = 0f;
        internal float GP2 = 0f;
        internal float BP2 = 0f;
        internal float I1 = 0f;
        internal float IR = 0f;
        internal float S1 = 0f;
        internal float SR = 0f;
        internal float OD = 0f;
    }

}
