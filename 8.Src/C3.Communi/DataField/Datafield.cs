using System;
using System.Collections;
using System.Collections.Generic ;
using System.Diagnostics;
using Xdgk.Common;

namespace C3.Communi
{
    #region DataFieldOption
    /// <summary>
    /// 数据域配置选项
    /// </summary>
    [Flags]
    public enum DataFieldOption
    {
        /// <summary>
        /// 指示数据域的Value可变
        /// </summary>
        ValueVolatile = 0x01,

        /// <summary>
        /// 指示数据域的Bytes可变
        /// </summary>
        BytesVolatile = 0x02,

        /// <summary>
        /// 指示数据域为校验数据
        /// </summary>
        CRC = 0x04,

        /// <summary>
        /// 
        /// </summary>
        MatchCheck = 0x08,

        /// <summary>
        /// 
        /// </summary>
        Address = 0x10,

        /// <summary>
        /// 
        /// </summary>
        Ignore = 0x20,

        /// <summary>
        /// 
        /// </summary>
        Lazy = 0x40,
    }
    #endregion //

    #region DataField
    /// <summary>
    /// 数据域代表发送或接收比特数组中的，有具体含义的单位数据
    /// </summary>
    /// <example>

    /// </example>
    public class DataField
    { 
        //public const int UNSURENESS = -1;

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="beginPosition"></param>
        /// <param name="dataLength"></param>
        public DataField(string name, int beginPosition, int dataLength, IBytesConverter bytesConverter)
            : this ( name, beginPosition, dataLength, bytesConverter, DataFieldOption.BytesVolatile | DataFieldOption.ValueVolatile )
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="beginPosition"></param>
        /// <param name="dataLength"></param>
        public DataField(string name, int beginPosition, int dataLength, IBytesConverter bytesConverter, DataFieldOption option )
        {
            this.Name = name.Trim();
            this.BeginPosition = beginPosition;
            this.DataLength = dataLength;

            this.DataFieldOption = option;
            this.BytesConverter = bytesConverter;
        }
        #endregion //

        #region BytesConverter
        public IBytesConverter BytesConverter 
        {
            get { return _bytesConverter; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("bytesConverter");
                this._bytesConverter = value;
            }
        } private IBytesConverter _bytesConverter;
        #endregion //

        #region DataFieldOption
        public DataFieldOption DataFieldOption
        {
            get { return _dataFieldOption; }
            set { _dataFieldOption = value; }
        } private DataFieldOption _dataFieldOption;
        #endregion //


        #region BeginPosition
        /// <summary>
        /// 
        /// </summary>
        public int BeginPosition
        {
            get { return _beginPosition; }
            set 
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("beginPosition: " + value);
                }
                _beginPosition = value;
            }
        } private int _beginPosition;
        #endregion //

        #region DataLength
        /// <summary>
        /// 
        /// </summary>
        public int DataLength
        {
            get { return _dataLength; }
            set
            {
                if ( value <= 0)
                    throw new ArgumentOutOfRangeException("dataLength: " + value );
                _dataLength = value ;
            }
        } private int _dataLength;
        #endregion

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
                if (value == null || value.Trim().Length == 0)
                    throw new ArgumentException("DataField name null or empty");
                _name = value; 
            }
        } private string _name;
        #endregion

       


        #region Bytes
        /// <summary>
        /// 
        /// </summary>
        public byte[] Bytes
        {
            get { return _bytes; }
            set 
            {
                if ((this.DataFieldOption & DataFieldOption.BytesVolatile) > 0)
                {
                    this._bytes = value;
                    this._value = this._bytesConverter.ConvertToObject(this._bytes);
                }
                else
                {
                    throw new InvalidOperationException("cannot change Bytes");
                }
            }
        } private byte[] _bytes;
        #endregion    

        #region Value
        /// <summary>
        /// 
        /// </summary>
        public object Value
        {
            get { return _value; }
            set
            {
                if ((this.DataFieldOption & DataFieldOption.ValueVolatile) > 0)
                {
                    _value = value;
                    byte[] bs = this._bytesConverter.ConvertToBytes(_value);
                    if (bs.Length != this.DataLength)
                    {
                        string errmsg = "转换后的数值长度与期望长度不符。" + Environment.NewLine;
                        errmsg += string.Format("期望: {0}, 实际: {1}", this.DataLength, bs.Length);
                        throw new ArgumentException(errmsg);
                    }
                    this._bytes = bs;
                }
                else
                {
                    throw new InvalidOperationException("Cannot set Value");
                }
            }
        } private object _value;

        #endregion //


        #region IsCRC
        /// <summary>
        /// 
        /// </summary>
        public bool IsCRC
        {
            get { return (this.DataFieldOption & DataFieldOption.CRC) > 0; }
            set 
            {
                if (value)
                {
                    this.DataFieldOption |= DataFieldOption.CRC;
                }
                else
                {
                    this.DataFieldOption &= (~DataFieldOption.CRC);
                }
            }
        }
        #endregion //

        #region
        /// <summary>
        /// 
        /// </summary>
        public bool IsBytesVolatile
        {
            get { return (DataFieldOption & DataFieldOption.BytesVolatile) > 0; }
            set 
            {
                if (value)
                {
                    this.DataFieldOption |= DataFieldOption.BytesVolatile;
                }
                else
                {
                    this.DataFieldOption &= (~DataFieldOption.BytesVolatile);
                }
            }
        }
        #endregion //

        #region
        public bool IsValueVolatile
        {
            get { return ( DataFieldOption & DataFieldOption.ValueVolatile ) > 0; }
            set
            {
                if (value)
                {
                    this.DataFieldOption |= DataFieldOption.ValueVolatile;
                }
                else
                {
                    this.DataFieldOption &= (~DataFieldOption.ValueVolatile);
                }
            }
        }
        #endregion //


        #region IsMatchCheck
        /// <summary>
        /// 指示是否需要匹配
        /// </summary>
        public bool IsMatchCheck
        {
            get { return (this.DataFieldOption & DataFieldOption.MatchCheck) > 0; }
            set 
            {
                if (value)
                {
                    this.DataFieldOption |= DataFieldOption.MatchCheck;
                }
                else
                {
                    this.DataFieldOption &= (~DataFieldOption.MatchCheck);
                }
            }
        }
        #endregion //IsMatchCheck


        #region IsAddress
        /// <summary>
        /// 
        /// </summary>
        public bool IsAddress
        {
            get { return (this.DataFieldOption & DataFieldOption.Address) > 0; }
            set
            {
                if (value)
                {
                    this.DataFieldOption |= DataFieldOption.Address;
                }
                else
                {
                    this.DataFieldOption &= (~DataFieldOption.Address);
                }
            }
        }
        #endregion //IsAddress

        #region IsLazy
        /// <summary>
        /// 
        /// </summary>
        public bool IsLazy
        {
            get { return (this.DataFieldOption & DataFieldOption.Lazy) > 0; }
            set
            {
                if (value)
                {
                    this.DataFieldOption |= DataFieldOption.Lazy;
                }
                else
                {
                    this.DataFieldOption &= (~DataFieldOption.Lazy);
                }
            }
        }
        #endregion //IsLazy

        #region IsIgnore
        /// <summary>
        /// 
        /// </summary>
        public bool IsIgnore
        {
            get { return (this.DataFieldOption & DataFieldOption.Ignore) > 0; }
            set {
                if (value)
                {
                    this.DataFieldOption |= DataFieldOption.Ignore;
                }
                else
                {
                    this.DataFieldOption &= (~DataFieldOption.Ignore);
                }
            }
        }
        #endregion //IsIgnore

        #region IsMatch
        /// <summary>
        /// 检查dataField数据是否和datas得数据匹配。
        /// </summary>
        /// <param name="datas"></param>
        /// <param name="index">开始位置索引</param>
        /// <returns></returns>
        public bool Match(byte[] datas, int index)
        {

            
            if (!IsCRC && !IsMatchCheck)
                return true;

            if (datas == null)
                return false;
            //if (this._beginPosition == UNSURENESS ||
            //     this._dataLength == UNSURENESS ||
            //     this._bytes == null)
            //    return false;

            if (index < 0)
                throw new ArgumentOutOfRangeException("index out of range");

            if (index + this.DataLength > datas.Length)
                return false;

            //int b = index + _beginPosition;
            //int e = b + _dataLength;

            //if (b > datas.Length || e > datas.Length)
            //    return false;

            //for (int i = 0; i < _dataLength; i++)
            //{
            //    if (datas[b + i] != _bytes[i])
            //        return false;
            //}
            //return true;

            byte[] bs = this.GetMatch(datas, index);
            if (bs == null || bs.Length == 0)
            {
                string temp = string.Format("GetMatch(byte[], int ) == null, datas '{0}', index '{1}'",
                        BitConverter.ToString(datas), index);
                throw new InvalidOperationException(temp);
            }


            for (int i = 0; i < bs.Length; i++)
            {
                if (Bytes == null)
                {
                    string s = string.Format("DataField '{0}' Bytes == null", this.Name);
                    throw new InvalidOperationException(s);
                }
                if (bs[i] != this.Bytes[i])
                    return false;
            }
            return true;
        }
        #endregion //IsMatch

        /// <summary>
        /// 
        /// </summary>
        /// <param name="datas"></param>
        /// <returns></returns>
        public byte[] GetMatch(byte[] datas)
        {
            return GetMatch(datas, 0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="datas"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public virtual byte[] GetMatch(byte[] datas, int index)
        {
            //if (BeginPostion == BytesDataField.UNSURENESS)
            //    throw new Exception("BeginPostion == UNSURENESS");

            //if (DataLength == BytesDataField.UNSURENESS)
            //    throw new Exception("DataLength == UNSURENESS");

            Debug.Assert(DataLength > 0, "DataLength <= 0");

            if (index < 0)
                throw new ArgumentOutOfRangeException("index: " + index);
            if (datas == null)
                throw new ArgumentNullException("datas");

            if (index + this.DataLength > datas.Length)
                throw new InvalidOperationException("dataField out of datas range");

            int b = this.BeginPosition + index;
            int e = b + this.DataLength;

            int len = e - b;
            byte[] ans = new byte[len];
            Array.Copy(datas, b, ans, 0, len);

            return ans;
        }
    }
    #endregion //

    /// <summary>
    /// 
    /// </summary>
    public interface IDataFieldValueProvider
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        object GetValue(string name);
    }

    /// <summary>
    /// 
    /// </summary>
    public class DataFieldValueProviderCollection : Xdgk.Common.Collection<IDataFieldValueProvider>
    {
    }

    /// <summary>
    /// 
    /// </summary>
    public class DataFieldValueProvider : IDataFieldValueProvider 
    {
        public DataFieldValueProvider(IDevice device)
        {
            if (device==null)
            {
                throw new ArgumentNullException("device");
            }
            _device = device;
        }

        //public KeyValueCollection KeyValues
        //{
        //    get
        //    {
        //        return _keyValues;
        //    }
        //} private KeyValueCollection _keyValues = new KeyValueCollection();
        private IDevice _device;

        #region GetValue
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public object GetValue(string name)
        {
            object obj = _device.GetLazyDataFieldValue(name);
            if (obj == null)
            {
                throw new ArgumentException(string.Format("not find value by name '{0}'", name));
            }
            return obj;
            //object value = kv.Value;
            //if (value is GetValueDelegate)
            //{
            //    GetValueDelegate d = (GetValueDelegate)value;
            //    return d();
            //}
            //else
            //{
            //    return value;
            //}
        }
        #endregion //GetValue
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public delegate object GetValueDelegate();

    /// <summary>
    /// 
    /// </summary>
    public class DelegateDataFieldValueProvider : IDataFieldValueProvider
    {
        /// <summary>
        /// 
        /// </summary>
        private KeyValueCollection _keyValues = new KeyValueCollection();

        public void AddDelegate(string name, GetValueDelegate d)
        {
            if (d == null)
            {
                throw new ArgumentNullException("d");
            }

            KeyValue kv = _keyValues.Find(name);
            if (kv != null)
            {
                throw new ArgumentException(string.Format("exist name '{0}'", name));
            }

            _keyValues.Add(new KeyValue(name, d));
        }

        #region GetValue
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public object GetValue(string name)
        {
            KeyValue kv = this._keyValues.Find(name);
            if (kv == null)
            {
                throw new ArgumentException(string.Format("not find value by key '{0}'", name));
            }

            GetValueDelegate d = (GetValueDelegate)kv.Value;
            return d();
        }

        #endregion //GetValue
    }
}
