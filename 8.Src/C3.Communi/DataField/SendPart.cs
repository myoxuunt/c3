using System;

namespace C3.Communi
{

    /// <summary>
    /// 
    /// </summary>
    public class PartBase : ITag 
    {
        #region IsNeedAddress
        /// <summary>
        /// 
        /// </summary>
        public bool IsNeedAddress
        {
            get
            {
                return _isNeedAddress;
            }
            set
            {
                _isNeedAddress = value;
            }
        } private bool _isNeedAddress = true;
        #endregion //IsNeedAddress

        #region DataFieldManager
        /// <summary>
        /// 
        /// </summary>
        public DatafieldManager DataFieldManager
        {
            get
            {
                if (this._DatafieldManager == null)
                    this._DatafieldManager = new DatafieldManager();
                return _DatafieldManager;
            }
        } private DatafieldManager _DatafieldManager;
        #endregion //DataFieldManager

        #region Remove
        /// <summary>
        /// 
        /// </summary>
        /// <param name="df"></param>
        public void Remove(DataField df)
        {
            this.DataFieldManager.DataFields.Remove(df);
        }
        #endregion //Remove


        #region ITag ��Ա
        public object Tag
        {
            get
            {
                return _tag;
            }
            set
            {
                _tag = value;
            }
        } private object _tag;
        #endregion
    }

    /// <summary>
    /// �������ݲ���
    /// </summary>
    public class SendPart : PartBase 
    {
        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        public SendPart()
        {

        }
        #endregion //

        #region this
        /// <summary>
        /// ��ȡ����������Ϊname��dataField��ֵ
        /// </summary>
        /// <returns></returns>
        public object this[string name]
        {
            get
            {
                DataField df = this.DataFieldManager[name];
                if (df == null)
                {
                    return null;
                }
                else
                {
                    return df.Value;
                }
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("value");

                DataField df = this.DataFieldManager[name];
                if (df != null)
                {
                    df.Value = value;
                }
                else
                {
                    throw new InvalidOperationException("not find DataField: " + name);
                }

            }
        }
        #endregion //this

        #region ToBytes
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public byte[] ToBytes(DataFieldValueProvider valueProvider)
        {
            if (valueProvider == null)
            {
                throw new ArgumentNullException("valueProvider");
            }

            return this.DataFieldManager.ToBytes(valueProvider);
        }
        #endregion //ToBytes

        #region Add
        /// <summary>
        /// 
        /// </summary>
        /// <param name="df"></param>
        public void Add(DataField df)
        {
            if (!df.IsValueVolatile)
                throw new ArgumentException("dataField.IsValueVolatile must be true" );

            this.DataFieldManager.DataFields.Add(df);
        }
        #endregion //Add

    }
}
