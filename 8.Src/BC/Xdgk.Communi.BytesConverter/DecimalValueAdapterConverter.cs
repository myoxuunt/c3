using System;
using System.Collections.Generic;
using System.Text;
using C3.Communi;

namespace Xdgk.Communi
{
    /// <summary>
    /// 
    /// </summary>
    public class SyntaxSemantemeValueConverter
    {
        /// <summary>
        /// 
        /// </summary>
        private SyntaxSemantemeValueConverter()
        {
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="semantemeValue"></param>
        /// <param name="syntaxSemantemeValueRate"></param>
        static public double ToSyntaxValue(double semantemeValue, double syntaxSemantemeValueRate)
        {
            return semantemeValue * syntaxSemantemeValueRate;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="syntaxValue"></param>
        /// <param name="syntaxSemantemeRate"></param>
        /// <returns></returns>
        static public double ToSemantemeValue(double syntaxValue, double syntaxSemantemeValueRate)
        {
            return syntaxValue / syntaxSemantemeValueRate;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class DecimalValueAdapterConverter : BytesConverter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override byte[] ConvertToBytes(object obj)
        {
            if (this.InnerBytesConverter  == null)
            {
                throw new InvalidOperationException("InnerConverter is null");
            }

            double dblObj = Convert.ToDouble(obj);
            double dblSyntax = SyntaxSemantemeValueConverter.ToSyntaxValue(dblObj, this.SyntaxSemantemeValueRate);
            return InnerBytesConverter.ConvertToBytes(dblSyntax);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public override object ConvertToObject(byte[] bytes)
        {
            if (this.InnerBytesConverter == null)
            {
                throw new InvalidOperationException("InnerConverter is null");
            }

            object r = this.InnerBytesConverter.ConvertToObject(bytes);
            double dblValue = Convert.ToDouble(r);
            return SyntaxSemantemeValueConverter.ToSemantemeValue(dblValue, this.SyntaxSemantemeValueRate);
        }

        /// <summary>
        /// 
        /// </summary>
        public double SyntaxSemantemeValueRate
        {
            get { return _syntaxSemantemeValueRate; }
            set
            {
                if (value == 0)
                {
                    throw new ArgumentException("RateFactor cannot is 0");
                }
                _syntaxSemantemeValueRate = value;
            }
        } private double _syntaxSemantemeValueRate = 1;

        /// <summary>
        /// 
        /// </summary>
        public double Rate
        {
            get { return this.SyntaxSemantemeValueRate; }
            set { this.SyntaxSemantemeValueRate = value; }
        }
    }
}
