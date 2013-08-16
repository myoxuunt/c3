using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text;
using C3.Communi;
using Xdgk.GR.Common;
using Xdgk.Common;

namespace LYR001DPU
{
    internal enum AdjusterControlMode
    {
        ManualODMode = 0,
        GT2Mode= 1,
        GT2AndLineMode = 2,
        BT1Mode = 3,
        BT2Mode = 4,

        MIN_VALUE = ManualODMode,
        MAX_VALUE = BT2Mode,
    }

    /// <summary>
    /// 
    /// </summary>
    internal class AdjusterContorlInfo
    {
        internal AdjusterControlMode ControlMode;
        internal float
                ODSet,
                GT2Set,
                BT1Set,
                BT2Set,
                P,
                I,
                ODMax,
                ODMin,
                SW1,
                SW2,
                SW3,
                SW4,
                SW5,
                SW6,
                SW7,
                SW8,
                SD1,
                SD2,
                SD3,
                SD4,
                SD5,
                SD6,
                SD7,
                SD8,
                TimeValue1,
                TimeValue2,
                TimeValue3,
                TimeValue4,
                TimeValue5,
                TimeValue6,
                TimeValue7,
                TimeValue8,
                TimeValue9,
                TimeValue10,
                TimeValue11,
                TimeValue12,
                OTSet;

    }

    internal class AdjusterControlInfoReader
    {
        private ITask _task;
        private IParseResult _pr;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="task"></param>
        /// <param name="pr"></param>
        internal AdjusterControlInfoReader(ITask task, IParseResult pr)
        {
            Debug.Assert(pr.IsSuccess);

            _task = task;
            _pr = pr;
        }

        /// <summary>
        /// 
        /// </summary>
        internal bool Read()
        {
            AdjusterContorlInfo info = new AdjusterContorlInfo();

            int modeValue = Convert.ToInt32(_pr.Results["ControlMode"]);
            if (!(modeValue >= (int)AdjusterControlMode.MIN_VALUE &&
               modeValue <= (int)AdjusterControlMode.MAX_VALUE))
            {
                return false;
            }

            info.ControlMode = (AdjusterControlMode)modeValue;

            info.ODSet = Convert.ToSingle(_pr.Results["ODSet"]);
            info.GT2Set = Convert.ToSingle(_pr.Results["GT2Set"]);
            info.BT1Set = Convert.ToSingle(_pr.Results["BT1Set"]);
            info.BT2Set = Convert.ToSingle(_pr.Results["BT2Set"]);
            info.P = Convert.ToSingle(_pr.Results["P"]);
            info.I = Convert.ToSingle(_pr.Results["I"]);
            info.ODMax = Convert.ToSingle(_pr.Results["ODMax"]);
            info.ODMin = Convert.ToSingle(_pr.Results["ODMin"]);
            info.SW1 = Convert.ToSingle(_pr.Results["SW1"]);
            info.SW2 = Convert.ToSingle(_pr.Results["SW2"]);
            info.SW3 = Convert.ToSingle(_pr.Results["SW3"]);
            info.SW4 = Convert.ToSingle(_pr.Results["SW4"]);
            info.SW5 = Convert.ToSingle(_pr.Results["SW5"]);
            info.SW6 = Convert.ToSingle(_pr.Results["SW6"]);
            info.SW7 = Convert.ToSingle(_pr.Results["SW7"]);
            info.SW8 = Convert.ToSingle(_pr.Results["SW8"]);
            info.SD1 = Convert.ToSingle(_pr.Results["SD1"]);
            info.SD2 = Convert.ToSingle(_pr.Results["SD2"]);
            info.SD3 = Convert.ToSingle(_pr.Results["SD3"]);
            info.SD4 = Convert.ToSingle(_pr.Results["SD4"]);
            info.SD5 = Convert.ToSingle(_pr.Results["SD5"]);
            info.SD6 = Convert.ToSingle(_pr.Results["SD6"]);
            info.SD7 = Convert.ToSingle(_pr.Results["SD7"]);
            info.SD8 = Convert.ToSingle(_pr.Results["SD8"]);
            info.TimeValue1 = Convert.ToSingle(_pr.Results["TimeValue1"]);
            info.TimeValue2 = Convert.ToSingle(_pr.Results["TimeValue2"]);
            info.TimeValue3 = Convert.ToSingle(_pr.Results["TimeValue3"]);
            info.TimeValue4 = Convert.ToSingle(_pr.Results["TimeValue4"]);
            info.TimeValue5 = Convert.ToSingle(_pr.Results["TimeValue5"]);
            info.TimeValue6 = Convert.ToSingle(_pr.Results["TimeValue6"]);
            info.TimeValue7 = Convert.ToSingle(_pr.Results["TimeValue7"]);
            info.TimeValue8 = Convert.ToSingle(_pr.Results["TimeValue8"]);
            info.TimeValue9 = Convert.ToSingle(_pr.Results["TimeValue9"]);
            info.TimeValue10 = Convert.ToSingle(_pr.Results["TimeValue10"]);
            info.TimeValue11 = Convert.ToSingle(_pr.Results["TimeValue11"]);
            info.TimeValue12 = Convert.ToSingle(_pr.Results["TimeValue12"]);
            info.OTSet = Convert.ToSingle(_pr.Results["OTSet"]);

            _adjusterControlInfo = info;

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        internal AdjusterContorlInfo AdjusterControlInfo
        {
            get { return _adjusterControlInfo; }
        } private AdjusterContorlInfo _adjusterControlInfo = null;
    }
}