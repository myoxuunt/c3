using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Text;
using C3.Communi;
using Xdgk.Common;
using C3.Communi.SimpleDPU;

namespace VGATE100DPU
{
    /// <summary>
    /// 
    /// </summary>
    public class VGate100Data : DataBase
    {
        #region BeforeWL
        /// <summary>
        /// 
        /// </summary>
        [DataItem("闸前水位", 20, "m")]
        public double BeforeWL
        {
            get
            {
                return _beforeWL;
            }
            set
            {
                _beforeWL = value;
            }
        } private double _beforeWL;
        #endregion //BeforeWL

        #region BehindWL
        /// <summary>
        /// 
        /// </summary>
        [DataItem("闸后水位", 30, "m")]
        public double BehindWL
        {
            get
            {
                return _behindWL;
            }
            set
            {
                _behindWL = value;
            }
        } private double _behindWL;
        #endregion //BehindWL

        #region Height
        /// <summary>
        /// 
        /// </summary>
        [DataItem("闸高", 40, "m")]
        public double Height
        {
            get
            {
                return _height;
            }
            set
            {
                _height = value;
            }
        } private double _height;
        #endregion //Height

        #region InstantFlux
        /// <summary>
        /// 
        /// </summary>
        [DataItem("瞬时流量", 50, "m3/s")]
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

        #region TotalAmount
        /// <summary>
        /// 
        /// </summary>
        [DataItem("累计流量", 60, "m3")]
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
        [DataItem("剩余水量", 70, "m3")]
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


        public byte[] ToBytes()
        {
            return ToBytes(this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        static public byte[] ToBytes(VGate100Data data)
        {
            Int64 ticks = data.DT.Ticks;
            float lwBefore = (float)data.BeforeWL;
            float lwBehind = (float)data.BehindWL;
            float height = (float)data.Height;
            float flux = (float)data.InstantFlux;
            float sum = (float)data.TotalAmount;
            float remain = (float)data.RemainAmount;

            MemoryStream ms = new MemoryStream();
            Write(ms, BitConverter.GetBytes(ticks));
            Write(ms, BitConverter.GetBytes(lwBefore));
            Write(ms, BitConverter.GetBytes(lwBehind));
            Write(ms, BitConverter.GetBytes(height));
            Write(ms, BitConverter.GetBytes(flux));
            Write(ms, BitConverter.GetBytes(sum));
            Write(ms, BitConverter.GetBytes(remain));
            return ms.ToArray();
        }

        static void Write(MemoryStream ms, byte[] bs)
        {
            ms.Write(bs, 0, bs.Length);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bs"></param>
        /// <returns></returns>
        static public VGate100Data ToVGate100Data(byte[] bs, int beginOffset)
        {
            int start = beginOffset;

            long ticks = BitConverter.ToInt64(bs, start);
            start += 8;

            float lwBefore = BitConverter.ToSingle(bs, start);
            start += 4;

            float lwBehind = BitConverter.ToSingle(bs, start);
            start += 4;

            float height = BitConverter.ToSingle(bs, start);
            start += 4;

            float flux = BitConverter.ToSingle(bs, start);
            start += 4;

            float sum = BitConverter.ToSingle(bs, start);
            start += 4;

            float remain = BitConverter.ToSingle(bs, start);
            start += 4;

            DateTime dt = new DateTime(ticks);

            VGate100Data r = new VGate100Data();
            r.DT = dt;
            r.BeforeWL = lwBefore;
            r.BehindWL = lwBehind;
            r.Height = height;
            r.InstantFlux = flux;
            r.RemainAmount = remain;
            r.TotalAmount = sum;

            return r;
        }

        static public int BytesCountOfVGateData
        {
            get
            {
                return 8 + 4 * 6;
            }
        }
    }
}