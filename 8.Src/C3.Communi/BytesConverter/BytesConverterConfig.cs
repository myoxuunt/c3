
using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using NUnit.Core;
using Xdgk.Common;
using System.Collections ;


namespace C3.Communi
{
    public class BytesConverterConfig
    {
#region Name
        /// <summary>
        /// 
        /// </summary>
        public string Name
        {
            get
            {
                if (_name == null)
                {
                    _name = string.Empty;
                }
                return _name;
            }
            set
            {
                _name = value;
            }
        } private string _name;
#endregion //Name

#region HasInner
        /// <summary>
        /// 
        /// </summary>
        public bool HasInner
        {
            get
            {
                return _hasInner;
            }
            set
            {
                _hasInner = value;
            }
        } private bool _hasInner;
#endregion //HasInner

#region InnerBytesConverterConfig
        /// <summary>
        /// 
        /// </summary>
        public BytesConverterConfig InnerBytesConverterConfig
        {
            get
            {
                return _innerBytesConverterConfig;
            }
            set
            {
                _innerBytesConverterConfig = value;
            }
        } private BytesConverterConfig _innerBytesConverterConfig;
#endregion //InnerBytesConverterConfig

        /// <summary>
        /// 
        /// </summary>
        public Hashtable Propertys
        {
            get
            {
                if (_hash == null)
                {
                    _hash = new Hashtable();
                }
                return _hash;
            }
            set { _hash = value; }
        } private Hashtable _hash;
    }

}
