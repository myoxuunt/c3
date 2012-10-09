using System;
using System.Collections.Generic;
using System.Text;
using C3.Communi;

namespace Xdgk.Communi
{
    public class RateConverter : BytesConverter 
    {
        #region Rate
        /// <summary>
        /// 
        /// </summary>
        public double Rate
        {
            get
            {
                return _rate;
            }
            set
            {
                if (value == 0d)
                {
                    throw new ArgumentException("Rate cannot zero");
                }
                _rate = value;
            }
        } private double _rate = 1.0d;
        #endregion //Rate

        /// <summary>
        /// 
        /// </summary>
        private void VerifyInnerBytesConverter()
        {
            if (InnerBytesConverter == null)
            {
                throw new InvalidProgramException("InnerBytesConverter is null");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override byte[] ConvertToBytes(object obj)
        {
            VerifyInnerBytesConverter();
            double value = Convert.ToDouble(obj);
            value *= Rate;
            byte[] bs = this.InnerBytesConverter.ConvertToBytes(value);
            return bs;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public override object ConvertToObject(byte[] bytes)
        {
            VerifyInnerBytesConverter();
            object obj = this.InnerBytesConverter.ConvertToObject(bytes);
            double dbl = Convert.ToDouble(obj);
            dbl = dbl / Rate;
            return dbl;
        }
    }
}
