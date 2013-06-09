
using System;
using System.IO;
using System.Diagnostics;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Windows.Forms;
using Xdgk.Common;
using C3.Communi;


namespace S
{
    abstract public class BaseOpera
    {
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

        /// <summary>
        /// 
        /// </summary>
        public byte CommandNO
        {
            get { return _commandNO; }
            set { _commandNO = value; }
        } private byte _commandNO;

        /// <summary>
        /// 
        /// </summary>
        public byte CommandType
        {
            get { return _commandType; }
            set { _commandType = value; }
        } private byte _commandType;

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
        abstract public byte[] OnToBytes();
    }

}
