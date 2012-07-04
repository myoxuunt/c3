
using System;
using Xdgk.Common;

namespace C3.Communi
{
    public class KeyValue
    {
        public KeyValue(string key, object value)
        {
            this.Key = key;
            this.Value = value;
        }

#region Key
        /// <summary>
        /// 
        /// </summary>
        public string Key
        {
            get
            {
                if (_key == null)
                {
                    _key = string.Empty;
                }
                return _key;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("key is null or empty");
                }
                _key = value;
            }
        } private string _key;
#endregion //Key

#region Value
        /// <summary>
        /// 
        /// </summary>
        public object Value
        {
            get
            {
                if (_value == null)
                {
                    _value = new object();
                }
                return _value;
            }
            set
            {
                _value = value;
            }
        } private object _value;
#endregion //Value

    }

}
