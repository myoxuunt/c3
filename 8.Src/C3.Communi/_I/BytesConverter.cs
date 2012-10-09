
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;


namespace C3.Communi
{
    abstract public class BytesConverter : IBytesConverter
    {

        /// <summary>
        /// 
        /// </summary>
        protected BytesConverter()
            : this(true)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isLittleEndian"></param>
        protected BytesConverter(bool isLittleEndian)
        {
            this.IsLittleEndian = isLittleEndian;
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsLittleEndian
        {
            get { return _isLittleEndian; }
            set { _isLittleEndian = value; }
        } private bool _isLittleEndian;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        virtual public object ConvertToObject(byte[] bytes)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        virtual public byte[] ConvertToBytes(object obj)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        public byte[] ReverseWithIsLittleEndian(byte[] bs)
        {
            if (bs == null)
                throw new ArgumentNullException("bs");
            if (!this.IsLittleEndian)
            {
                byte[] bsclone = (byte[])bs.Clone();
                Array.Reverse(bsclone);
                return bsclone;
            }
            return bs;
        }


        #region IBytesConverter ≥…‘±


        /// <summary>
        /// 
        /// </summary>
        public IBytesConverter InnerBytesConverter
        {
            get
            {
                return _innerBytesConverter;
            }
            set
            {
                _innerBytesConverter = value;
            }
        } private IBytesConverter _innerBytesConverter;

        #endregion
    }

}
