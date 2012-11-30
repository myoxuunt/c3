using System;
using System.Reflection;
using System.Collections.Generic;
using System.Text;

namespace Xdgk.Common
{
    /// <summary>
    /// 
    /// </summary>
    public class ReflectionHelper
    {
        private ReflectionHelper()
        {

        }

        static public object GetPropertyValue(object obj, string propertyName)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }

            if (string.IsNullOrEmpty(propertyName))
            {
                throw new ArgumentException("propertyName");
            }

            Type type = obj.GetType();
            PropertyInfo pi = type.GetProperty(propertyName);
            if (pi == null)
            {
                string msg = string.Format("not find property by name '{0}' from type '{1}'",
                    propertyName, obj.GetType().Name);
                throw new ArgumentException(msg);
            }
            object r = pi.GetValue(obj, null);
            return r;
        }

        static public bool HasProperty(object obj, string propertyName)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }

            if (string.IsNullOrEmpty(propertyName))
            {
                throw new ArgumentException("propertyName");
            }


            Type type = obj.GetType();
            PropertyInfo pi = type.GetProperty(propertyName);
            return pi != null;
        }
    }


}

namespace Xdgk.Common.Calc
{
    public enum CalcType
    {
        Max,
        Min,
        Avg,
        Sum,
    }

    public class Calculator
    {
        public Calculator(CalcType calcType)
        {
            this.CalcType = calcType;
        }
        List<double> _list = new List<double>();

        #region CalcType
        /// <summary>
        /// 
        /// </summary>
        public CalcType CalcType
        {
            get
            {
                return _calcType;
            }
            set
            {
                _calcType = value;
            }
        } private CalcType _calcType;
        #endregion //CalcType

        //#region Result
        ///// <summary>
        ///// 
        ///// </summary>
        //public double Result
        //{
        //    get
        //    {
        //        return _result;
        //    }
        //    set
        //    {
        //        _result = value;
        //    }
        //} private double _result;
        //#endregion //Result

        public double Calc()
        {
            if ( this._list.Count == 0 )
            {
                throw new InvalidOperationException ("has not value to calc");
            }

            double r = 0d;
            switch (this.CalcType)
            {
                case CalcType.Max:
                    {
                        double max = this._list[0];
                        foreach (double val in this._list)
                        {
                            if (val > max)
                            {
                                max = val;
                            }
                        }
                        r = max;
                    }
                    break;

                case CalcType.Min:
                    {
                        double min = this._list[0];
                        foreach (double val in this._list)
                        {
                            if (val < min)
                            {
                                min = val;
                            }
                        }
                        r = min;
                    }
                    break;

                case CalcType.Avg:
                    {
                        double sum = 0d;
                        foreach (double val in this._list)
                        {
                            sum += val;
                        }
                        r = sum / this._list.Count;
                    }
                    break;

                case CalcType.Sum:
                    {
                        double sum = 0d;
                        foreach (double val in this._list)
                        {
                            sum += val;
                        }
                        r = sum;
                    }
                    break;
                default :
                    throw new NotImplementedException(this.CalcType.ToString());
            }
            return r;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public void Add(double value)
        {
            _list.Add(value);
        }
    }
}
