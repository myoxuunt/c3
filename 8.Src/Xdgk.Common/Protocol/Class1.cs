using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace Xdgk.Common.Protocol
{
    public enum ResponseStatusEnum : byte
    {
        [EnumText("成功")]
        Success = 0,

        [EnumText("名称不存在")]
        NotExistName = 1,

        [EnumText("没有新数据")]
        NotNewDatas = 2,
    }
    abstract public class BaseOpera
    {
        #region BaseOpera
        /// <summary>
        /// 
        /// </summary>
        /// <param name="address"></param>
        /// <param name="commandType"></param>
        public BaseOpera(byte address, byte commandType)
        {
            this.Address = address;
            this.CommandType = commandType;
        }
        #endregion //BaseOpera

        #region Address
        /// <summary>
        /// 
        /// </summary>
        public byte Address
        {
            get
            {
                return _address;
            }
            set
            {
                _address = value;
            }
        } private byte _address;
        #endregion //Address

        #region CommandNO
        /// <summary>
        /// 
        /// </summary>
        public byte CommandNO
        {
            get { return _commandNO; }
            set { _commandNO = value; }
        } private byte _commandNO;
        #endregion //CommandNO

        #region CommandType
        /// <summary>
        /// 
        /// </summary>
        public byte CommandType
        {
            get { return _commandType; }
            set { _commandType = value; }
        } private byte _commandType;
        #endregion //CommandType

        #region ToBytes
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public byte[] ToBytes()
        {
            MemoryStream ms = new MemoryStream();
            byte[] h = new byte[] { 0x5b, 0x5b };
            byte[] t = new byte[] { 0x5d, 0x5d };

            ms.Write(h, 0, h.Length);
            ms.WriteByte(this.Address);
            ms.WriteByte(this.CommandType);
            ms.WriteByte(this.CommandNO);

            byte[] bs = OnToBytes();
            ms.Write(bs, 0, bs.Length);
            ms.Write(t, 0, t.Length);

            return ms.ToArray();
        }
        #endregion //ToBytes

        #region OnToBytes
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        abstract public byte[] OnToBytes();
        #endregion //OnToBytes
    }
   
}
