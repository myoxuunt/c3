using System;

namespace C3.Communi
{
    public class NumberParameter : ParameterBase
    {
        public NumberParameter(string name, Type valueType, object value, int orderNumber)
            : base(name, valueType, value, orderNumber)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        protected override void VerifyValue(object value)
        {
            // check value type
            // 
            // is byte int32 short long float double ...
            object r = Convert.ChangeType(value, this.ValueType);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class EnumParameter : ParameterBase
    {
        public EnumParameter(string name, Type valueType, object value, int orderNumber)
            : base(name, valueType, value, orderNumber)
        {
            if (!valueType.IsEnum)
            {
                throw new ArgumentException("valueType must is enum");
            }
        }
    }

}
