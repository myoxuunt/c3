using System;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Text;

namespace Xdgk.Common
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public struct Length : IComparable 
    {
        #region Members
        /// <summary>
        /// 
        /// </summary>
        /// 
        private const char LeftParenthesis = '(';
        private const char RightParenthesis = ')';

        static private Dictionary<LengthType, double> _factors = new Dictionary<LengthType, double>();
        static private Length _allowableDeviation;

        /// <summary>
        /// 
        /// </summary>
        private double _value;

        /// <summary>
        /// 
        /// </summary>
        private LengthType _lengthType;
        #endregion //Members

        #region Length
        /// <summary>
        /// 
        /// </summary>
        static Length()
        {
            _factors = new Dictionary<LengthType, double>();
            _factors.Add(LengthType.mm, 1d);
            _factors.Add(LengthType.cm, 10d);
            _factors.Add(LengthType.dm, 100d);
            _factors.Add(LengthType.m, 1000d);
            _factors.Add(LengthType.km, 1000 * 1000d);

            _allowableDeviation = new Length(0.001, LengthType.mm);
        }
        #endregion //Length

        public Length(Length rhs)
            : this(rhs._value, rhs._lengthType)
        {
            
        }
        #region Length
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="lengthType"></param>
        public Length(double value, LengthType lengthType)
        {
            _value = value;
            _lengthType = lengthType;
        }
        #endregion //Length

        #region AllowableDeviation
        /// <summary>
        /// 
        /// </summary>
        static public Length AllowableDeviation
        {
            get { return _allowableDeviation; }
            set
            {
                if (value.IsNegative)
                {
                    throw new ArgumentException("AllowableDeviation can not negative");
                }
                _allowableDeviation = value;
            }
        }
        #endregion //AllowableDeviation

        #region Value
        public double Value
        {
            get { return _value; }
        }
        #endregion //Value

        #region LengthType
        public LengthType LengthType
        {
            get { return _lengthType; }
        }
        #endregion //LengthType

        #region IsNegative
        public bool IsNegative
        {
            get { return this._value < 0; }
        }
        #endregion //IsNegative

        #region Abs
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Length Abs()
        {
            if (this.IsNegative)
            {
                return new Length(0 - this._value, this._lengthType);
            }
            else
            {
                return this;
            }
        }
        #endregion //Abs


        #region FromMM
        static public Length FromMM(double value)
        {
            return new Length(value, LengthType.mm);
        }
        #endregion //FromMM

        #region FromCM
        static public Length FromCM(double value)
        {
            return new Length(value, LengthType.cm);
        }
        #endregion //FromCM

        #region FromM
        static public Length FromM(double value)
        {
            return new Length(value, LengthType.m);
        }
        #endregion //FromM

        #region FromString
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        static public Length FromString(string value)
        {
            // ("7.56cm")
            // ("2.33(cm)")

            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException("value");
            }

            int begin = value.IndexOfAny(GetUnitSplitCharArray());

            if (begin < 0)
            {
                throw new ArgumentException("has not unit: " + value);
            }

            string valPart = value.Substring(0, begin).Trim();
            string unitPart = value.Substring(begin).Trim();

            if (valPart.Length == 0)
            {
                throw new ArgumentException("has not value: " + value);
            }

            while (unitPart.Length >= 2 && unitPart[0] == LeftParenthesis )
            {
                if (unitPart[unitPart.Length - 1] != RightParenthesis )
                {
                    throw new ArgumentException("unit format error: " + value);
                }

                unitPart = unitPart.Substring(1, unitPart.Length - 2).Trim();
            }

            double v = double.Parse(valPart);
            LengthType t = (LengthType)Enum.Parse(typeof(LengthType), unitPart, true);

            return new Length(v, t);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        static private char[] GetUnitSplitCharArray()
        {
            if (_unitSplitCharArray == null)
            {
                List<char> list = new List<char>();
                list.Add(LeftParenthesis);
                for( char c = 'a'; c <= 'z'; c++)
                {
                    list.Add(c);
                    list.Add(char.ToUpper(c));
                }
                _unitSplitCharArray = list.ToArray();
            }
            return _unitSplitCharArray;
        } static private char[] _unitSplitCharArray;

        #endregion //FromString



        #region ToString
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0}({1})", _value, _lengthType);
        }
        #endregion //ToString

        #region ToString
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string ToString(string valueFormat)
        {
            string format = "{0:" + valueFormat + "}({1})";
            return string.Format(format, _value, _lengthType);
        }
        #endregion //ToString

        #region ToMM
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Length ToMM()
        {
            return ConvertTo(LengthType.mm);
        }
        #endregion //ToMM

        #region ToCM
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Length ToCM()
        {
            return ConvertTo(LengthType.cm);
        }
        #endregion //ToCM

        #region ToM
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Length ToM()
        {
            return ConvertTo(LengthType.m);
        }
        #endregion //ToM

        #region ToKM
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Length ToKM()
        {
            return ConvertTo(LengthType.km);
        }
        #endregion //ToKM

        #region ConvertTo
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lengthType"></param>
        /// <returns></returns>
        public Length ConvertTo(LengthType destType)
        {
            if (this._lengthType == destType)
            {
                return this;
            }
            else
            {
                double r = _value * GetFactor(this._lengthType) / GetFactor(destType);
                return new Length(r, destType);
            }
        }
        #endregion //ConvertTo

        #region Equals
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (!(obj is Length))
            {
                return false;
            }

            Length dest = (Length)obj;
            double diffOfmm = this.ToMM().Value - dest.ToMM().Value;
            double allowOfmm = AllowableDeviation.ToMM().Value;
            return Math.Abs(diffOfmm) < allowOfmm;
        }
        #endregion //Equals

        #region operator
        #region -
        /// <summary>
        /// 
        /// </summary>
        /// <param name="l1"></param>
        /// <param name="l2"></param>
        /// <returns></returns>
        static public Length operator -(Length l1, Length l2)
        {
            double valOfmm = l1.ToMM().Value - l2.ToMM().Value;
            Length r = new Length(valOfmm, LengthType.mm);
            return r.ConvertTo(l1.LengthType);
        }
        #endregion //-

        #region +
        static public Length operator +(Length l1, Length l2)
        {
            double valOfmm = l1.ToMM().Value + l2.ToMM().Value;
            Length r = new Length(valOfmm, LengthType.mm);
            return r.ConvertTo(l1.LengthType);

        }
        #endregion //+

        #region !=
        static public bool operator !=(Length l1, Length l2)
        {
            return !(l1 == l2);
        }
        #endregion //!=

        #region ==
        static public bool operator ==(Length l1, Length l2)
        {
            return l1.Equals(l2);
        }
        #endregion //==

        #region <
        static public bool operator <(Length l1, Length l2)
        {
            return (l1 - l2).IsNegative;
        }
        #endregion //<

        #region <=
        static public bool operator <=(Length l1, Length l2)
        {
            return (l1 < l2) || (l1 == l2);
        }
        #endregion //<=

        #region >
        static public bool operator >(Length l1, Length l2)
        {
            return (l2 - l1).IsNegative;
        }
        #endregion //>

        #region >=
        static public bool operator >=(Length l1, Length l2)
        {
            return (l1 > l2) || (l1 == l2);
        }
        #endregion //>=

        #region *
        static public Length operator *(Length l1, int n)
        {
            return l1 * (double)n;
        }
        #endregion //*

        #region *
        static public Length operator *(Length l1, float f)
        {
            return l1 * (double)f;
        }
        #endregion //*

        #region *
        static public Length  operator *(Length l1, double d)
        {
            return new Length(l1.Value * d, l1.LengthType);
        }

        static public Length operator *(double d, Length l1)
        {
            return l1 * d;
        }
        #endregion //*

        #region /
        static public Length operator /(Length l1, int n)
        {
            return l1 / (double)n;
        }
        #endregion ///

        #region /
        static public Length operator /(Length l1, float f)
        {
            return l1 / (double)f;
        }
        #endregion ///

        #region /
        static public Length operator /(Length l1, double d)
        {
            return new Length(l1.Value / d, l1.LengthType);
        }
        #endregion ///
        #endregion //operator

        #region GetHashCode
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            //return base.GetHashCode();
            if (AllowableDeviation.Value == 0)
            {
                return this.Value.GetHashCode();
            }

            long longValue = (long)(this.ToMM().Value / AllowableDeviation.ToMM().Value);
            return longValue.GetHashCode();
        }
        #endregion //GetHashCode

        #region GetFactor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lengthType"></param>
        /// <returns></returns>
        private double GetFactor(LengthType lengthType)
        {
            return _factors[lengthType];
        }
        #endregion //GetFactor

        #region CompareTo
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int CompareTo(object obj)
        {
            if (obj == null)
            {
                return 1;
            }

            if (!(obj is Length))
            {
                throw new ArgumentException(
                    string.Format("obj[{0}] is not Length type", obj.GetType().FullName));
            }

            Length r = this - (Length)obj;

            if (r.Value == 0.0d)
            {
                return 0;
            }
            else
            {
                return r.IsNegative ? -1 : 1;
            }
        }
        #endregion //CompareTo
    }

}
