using System;
using System.Collections.Generic;
using System.Text;

namespace C3.Communi
{
    public class SourceConfig
    {
        public SourceConfig(string key, string value)
        {
            this.Key = key;
            this.Value = value;
        }
        #region ElementType
        /// <summary>
        /// 
        /// </summary>
        public ElementType ElementType
        {
            get
            {
                return _elementType;
            }
            set
            {
                _elementType = value;
            }
        } private ElementType _elementType;
        #endregion //ElementType

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
                _key = value;
            }
        } private string _key;
        #endregion //Key

        #region Value
        /// <summary>
        /// 
        /// </summary>
        public string Value
        {
            get
            {
                if (_value == null)
                {
                    _value = string.Empty;
                }
                return _value;
            }
            set
            {
                _value = value;
            }
        } private string _value;
        #endregion //Value

    }

}
