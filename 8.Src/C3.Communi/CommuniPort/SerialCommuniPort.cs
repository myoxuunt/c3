using System;
using System.IO.Ports;

namespace C3.Communi
{
    /// <summary>
    /// 
    /// </summary>
    public class SerialCommuniPort : CommuniPortBase 
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sp"></param>
        public SerialCommuniPort(SerialPort sp)
        {
            if (sp == null) throw new ArgumentNullException("sp");
            this.SerialPort = sp;
        }
        #region SerialPort
        /// <summary>
        /// 
        /// </summary>
        public SerialPort SerialPort
        {
            get
            {
                return _serialPort;
            }
            private set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("SerialPort");
                }
                _serialPort = value;
            }
        } private SerialPort _serialPort;
        #endregion //SerialPort
        protected override void OnClose()
        {
            if (this.SerialPort.IsOpen)
            {
                this.SerialPort.Close();
            }
        }

        protected override bool OnWrite(byte[] bytes)
        {
            if (bytes != null && bytes.Length > 0)
            {
                this.SerialPort.Write(bytes, 0, bytes.Length);
            }
            return true;
        }

        protected override byte[] OnRead()
        {
            int n = this.SerialPort.BytesToRead;
            byte[] bs = new byte[n];
            int readCount = this.SerialPort.Read(bs, 0, n);
            if (readCount < n)
            {
                byte[] r = new byte[readCount];
                Array.Copy(bs, 0, r, 0, readCount);
                return r;
            }
            else
            {
                return bs;
            }
        }

        protected override bool OnGetIsOpened()
        {
            return this.SerialPort.IsOpen;
        }
    }
}