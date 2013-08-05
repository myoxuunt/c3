using System;
using System.Collections;
using System.Collections.Generic;
using Xdgk.GR.Common;

namespace LYR001DPU
{
    internal class LYR001StatusDataParser
    {
        /// <summary>
        /// 
        /// </summary>
        private LYR001StatusDataParser()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bs"></param>
        /// <returns></returns>
        static internal LYR001StatusData Parse(byte[] bs)
        {
            if (bs == null)
            {
                throw new ArgumentNullException("bs");
            }

            if (bs.Length < 3)
            {
                throw new ArgumentException("bs.Length must >= 3");
            }

            BitArray list = new BitArray(bs);

            PumpStatus cm1 = GetPumpStatus(list, 0, 3);
            PumpStatus cm2 = GetPumpStatus(list, 1, 4);
            PumpStatus cm3 = GetPumpStatus(list, 2, 5);

            PumpStatus rm1 = GetPumpStatus(list, 6, 8);
            PumpStatus rm2 = GetPumpStatus(list, 7, 9);

            ManualAutomatic ma = GetManualAutomatic(list, 10);
            List<string> alarmList = GetAlarmList(list, 12);
            //BitArray list

            LYR001StatusData r = new LYR001StatusData(cm1, cm2, cm3, rm1, rm2, ma, alarmList);
            return r;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <param name="beginIdx"></param>
        /// <returns></returns>
        private static List<string> GetAlarmList(BitArray list, int beginIdx)
        {
            List<string> r = new List<string>();

            string[] alarms = new string[] { 
                "二次供温高",
                    "二次供压高",
                    "二次回压高",
                    "二次回压低",
                    "水箱液位低" 
            };

            for (int i = 0; i < alarms.Length; i++)
            {
                if (list[beginIdx])
                {
                    r.Add(alarms[i]);
                }
            }

            return r;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <param name="idx"></param>
        /// <returns></returns>
        private static ManualAutomatic GetManualAutomatic(BitArray list, int idx)
        {
            return list[idx] ? ManualAutomatic.Manual : ManualAutomatic.Automatic;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <param name="runStopIdx"></param>
        /// <param name="faultIdx"></param>
        /// <returns></returns>
        static private PumpStatus GetPumpStatus(BitArray list, int runStopIdx, int faultIdx)
        {
            if (list[faultIdx])
            {
                return PumpStatus.Fault;
            }
            else
            {
                if (list[runStopIdx])
                {
                    return PumpStatus.Run;
                }
                else
                {
                    return PumpStatus.Stop;
                }
            }
        }
    }
}
