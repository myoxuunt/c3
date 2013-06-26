using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Xdgk.Common;

namespace VPump100Common
{
    public class VPump100Data : DataBase , IToBytes 
    {
        #region InstantFlux
        /// <summary>
        /// 
        /// </summary>
        [DataItem("瞬时流量", 10, "m3/s")]
        public double InstantFlux
        {
            get
            {
                return _instantFlux;
            }
            set
            {
                _instantFlux = value;
            }
        } private double _instantFlux;
        #endregion //InstantFlux

        #region Efficiency
        /// <summary>
        /// 
        /// </summary>
        [DataItem("效率", 20, "%")]
        public double Efficiency
        {
            get
            {
                return _efficiency;
            }
            set
            {
                _efficiency = value;
            }
        } private double _efficiency;
        #endregion //Efficiency

        #region TotalAmount
        /// <summary>
        /// 
        /// </summary>
        [DataItem("累计流量", 30, "m3")]
        public double TotalAmount
        {
            get
            {
                return _totalAmount;
            }
            set
            {
                _totalAmount = value;
            }
        } private double _totalAmount;
        #endregion //TotalAmount

        #region RemianAmount
        /// <summary>
        /// 
        /// </summary>
        [DataItem("剩余水量", 40, "m3")]
        public double RemainAmount
        {
            get
            {
                return _remainAmount;
            }
            set
            {
                _remainAmount = value;
            }
        } private double _remainAmount;
        #endregion //RemianAmount

        #region PowerStatus
        /// <summary>
        /// 
        /// </summary>
        [DataItem("供电状态", 50, Unit.None)]
        public PumpPowerStatus PowerStatus
        {
            get
            {
                return _powerStatus;
            }
            set
            {
                _powerStatus = value;
            }
        } private PumpPowerStatus _powerStatus;
        #endregion //PowerStatus

        #region PumpStatus
        /// <summary>
        /// 
        /// </summary>
        [DataItem("泵站状态", 60, Unit.None)]
        public PumpStatus PumpStatus
        {
            get
            {
                return _pumpStatus;
            }
            set
            {
                _pumpStatus = value;
            }
        } private PumpStatus _pumpStatus;
        #endregion //PumpStatus

        #region VibrateStatus
        /// <summary>
        /// 
        /// </summary>
        [DataItem("震动状态", 70, Unit.None)]
        public VibrateStatus VibrateStatus
        {
            get
            {
                return _vibrateStatus;
            }
            set
            {
                _vibrateStatus = value;
            }
        } private VibrateStatus _vibrateStatus;
        #endregion //VibrateStatus

        #region ForceStartStatus
        /// <summary>
        /// 
        /// </summary>
        [DataItem("强启状态", 80, Unit.None)]
        public ForceStartStatus ForceStartStatus
        {
            get
            {
                return _forceStartStatus;
            }
            set
            {
                _forceStartStatus = value;
            }
        } private ForceStartStatus _forceStartStatus;
        #endregion //ForceStartStatus


        /// <summary>
        /// 
        /// </summary>
        /// <param name="bs"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        static public VPump100Data ToVPump100Data(byte[] bs,
            int startIndex)
        {
            int start = startIndex;
            Int64 ticks = BitConverter.ToInt64(bs, start);
            start += 8;

            float instantFlux = BitConverter.ToSingle(bs, start);
            start += 4;

            float eff = BitConverter.ToSingle(bs, start);
            start += 4;

            float total = BitConverter.ToSingle(bs, start);
            start += 4;

            float remain = BitConverter.ToSingle(bs, start);
            start += 4;

            PumpStatus pumpStatus = (PumpStatus)bs[start++];
            ForceStartStatus forceStartStatus = (ForceStartStatus)bs[start++];
            VibrateStatus vibrateStatus = (VibrateStatus)bs[start++];
            PumpPowerStatus powerStatus = (PumpPowerStatus)bs[start++];

            VPump100Data data = new VPump100Data();
            data.DT = new DateTime(ticks);
            data.InstantFlux = instantFlux;
            data.Efficiency = eff;
            data.ForceStartStatus = forceStartStatus;
            data.PowerStatus = powerStatus;
            data.PumpStatus = pumpStatus;
            data.RemainAmount = remain;
            data.TotalAmount = total;
            data.VibrateStatus = vibrateStatus;

            return data;

        }

        static public byte[] ToBytes(VPump100Data vPump100Data)
        {
            Int64 ticks = vPump100Data.DT.Ticks;
            float instantFlux = Convert.ToSingle(vPump100Data.InstantFlux);
            float eff = Convert.ToSingle(vPump100Data.Efficiency);
            float sum = Convert.ToSingle(vPump100Data.TotalAmount);
            float remain = Convert.ToSingle(vPump100Data.RemainAmount);
            
            MemoryStream ms = new MemoryStream();
            Write(ms, BitConverter.GetBytes(ticks));
            Write(ms, BitConverter.GetBytes(instantFlux));
            Write(ms, BitConverter.GetBytes(eff));
            Write(ms, BitConverter.GetBytes(sum));
            Write(ms, BitConverter.GetBytes(remain));

            ms.WriteByte((byte)vPump100Data.PumpStatus);
            ms.WriteByte((byte)vPump100Data.ForceStartStatus);
            ms.WriteByte((byte)vPump100Data.VibrateStatus);
            ms.WriteByte((byte)vPump100Data.PowerStatus);

            return ms.ToArray();
        }

        static private void Write(MemoryStream ms, byte[] bs)
        {
            ms.Write(bs, 0, bs.Length);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public byte[] ToBytes()
        {
            return VPump100Data.ToBytes(this);
        }

        static public int BytesCountOfVPumpData
        {
            get { return 28; }
        }

        #region IToBytes 成员


        public int BytesCountOfEmpty
        {
            get { return BytesCountOfVPumpData; }
        }

        #endregion
    }
}
