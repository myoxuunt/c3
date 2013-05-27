using System;
using System.Collections.Generic;
using System.Text;
using C3.Communi;


namespace LY.Converters
{

    abstract public class A : BytesConverter
    {
        public A()
        {
            this.IsLowWordFirst = true;
            this.IsLowByteFirst = true;
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsLowWordFirst
        {
            get { return _isLowWordFirst; }
            set { _isLowWordFirst = value; }
        } private bool _isLowWordFirst;

        /// <summary>
        /// 
        /// </summary>
        public bool IsLowByteFirst
        {
            get { return _isLowByteFirst; }
            set { _isLowByteFirst = value; }
        } private bool _isLowByteFirst;
    }


    public class Int32Converter : A  
    {
        /// <summary>
        /// 
        /// </summary>
        public Int32Converter()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override byte[] ConvertToBytes(object obj)
        {
            Int32 n = Convert.ToInt32(obj);
            byte[] bs = BitConverter.GetBytes(n);
            return BytesOrderAdjustor.AdjustWordOrder(bs, true, true, this.IsLowWordFirst, this.IsLowByteFirst);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public override object ConvertToObject(byte[] bytes)
        {
            byte[] bs = BytesOrderAdjustor.AdjustWordOrder(bytes, this.IsLowWordFirst, this.IsLowByteFirst,
                true, true);
            return BitConverter.ToInt32(bs, 0);
        }
    }

    public class UInt32Converter : A
    {
        public override byte[] ConvertToBytes(object obj)
        {
            UInt32 n = Convert.ToUInt32(obj);
            byte[] bs = BitConverter.GetBytes(n);
            return BytesOrderAdjustor.AdjustWordOrder(bs, true, true, this.IsLowWordFirst, this.IsLowByteFirst);
        }
        public override object ConvertToObject(byte[] bytes)
        {
            byte[] bs = BytesOrderAdjustor.AdjustWordOrder(bytes, this.IsLowWordFirst, this.IsLowByteFirst,
                true, true);
            return BitConverter.ToUInt32(bs, 0);
        }
    }

    public class BytesOrderAdjustor
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bs"></param>
        /// <param name="srcWordFirst"></param>
        /// <param name="srcByteFirst"></param>
        /// <param name="destWordFirst"></param>
        /// <param name="destByteFirst"></param>
        /// <returns></returns>
        static public byte[] AdjustWordOrder(byte[] bs, bool srcWordFirst, bool srcByteFirst, bool destWordFirst, bool destByteFirst)
        {
            if (bs == null)
            {
                throw new ArgumentNullException("bs");
            }

            if (bs.Length % 2 != 0)
            {
                throw new ArgumentException("bs.Length % 2 != 0");
            }

            if (srcWordFirst != destWordFirst)
            {
                List<byte> list = new List<byte>();
                for (int i = 0; i < bs.Length; i += 2)
                {
                    list.InsertRange(0, new byte[] { bs[i], bs[i + 1] });
                }
                bs = list.ToArray();
            }

            if (srcByteFirst != destByteFirst)
            {
                for (int i = 0; i < bs.Length; i += 2)
                {
                    byte t = bs[i];
                    bs[i] = bs[i + 1];
                    bs[i + 1] = t;
                }
            }
            return bs;
        }
    }
}

namespace Xdgk.Communi
{

    /// <summary>
    /// 
    /// </summary>
    public class Int32ModbusConverter : C3.Communi.BytesConverter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override byte[] ConvertToBytes(object obj)
        {
            Int32 n = Convert.ToInt32(obj);
            byte[] bs = BitConverter.GetBytes(n);

            byte[] bsResult = new byte[] { bs[1], bs[0], bs[3], bs[2] };
            return bsResult;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public override object ConvertToObject(byte[] bytes)
        {
            byte[] bs = new byte[] { bytes[2], bytes[3], bytes[0], bytes[1]};
            Array.Reverse(bs);
            return BitConverter.ToInt32(bs, 0);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class UInt32ModbusConverter : C3.Communi.BytesConverter 
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override byte[] ConvertToBytes(object obj)
        {
            UInt32 u = Convert.ToUInt32(obj);
            byte[] bs = BitConverter.GetBytes(u);
            //Console.WriteLine(BitConverter.ToString(bs));
            byte[] bsResult = new byte[] { bs[1], bs[0], bs[3], bs[2] };
            return bsResult;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public override object ConvertToObject(byte[] bytes)
        {
            byte[] bs = new byte[] { bytes[2], bytes[3], bytes[0], bytes[1]};
            //Console.WriteLine(BitConverter.ToString(bs));
            Array.Reverse(bs);
            return BitConverter.ToUInt32(bs, 0);
        }
    }


}
