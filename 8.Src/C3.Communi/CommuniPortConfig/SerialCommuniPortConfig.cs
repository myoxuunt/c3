
using System;
using System.IO.Ports;
using Xdgk.Common;


namespace C3.Communi
{
    internal static class TimeoutDefauleValues
    {
        public const uint
            MinTimeoutMillsSencond = 100,
            MaxTimeoutMillsSecond = 60 * 1000,
            DefaultTimeoutMillsSecond = 10 * 1000;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="timeoutValue"></param>
        static public void Verify(uint timeoutValue)
        {
            if( timeoutValue < MinTimeoutMillsSencond || timeoutValue > MaxTimeoutMillsSecond )
            {
                string s = string.Format(
                    "Timeout value out of range, timeout value must between [{0}, {1}] millsSencond, current is {2}", 
                    MinTimeoutMillsSencond,
                    MaxTimeoutMillsSecond,
                    timeoutValue);
                throw new ArgumentOutOfRangeException(s);
            }
        }
    }

    public class SerialCommuniPortConfig : ICommuniPortConfig
    {

        /// <summary>
        /// 
        /// </summary>
        public SerialCommuniPortConfig()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="setting"></param>
        public SerialCommuniPortConfig(SerialPortSetting setting)
        {
            this.SerialPortSetting = setting;
        }
        /// <summary>
        /// 
        /// </summary>
        public bool CanCreate
        {
            get { return true; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ICommuniPort Create()
        {
            SerialPort sp = new SerialPort(
                    this.SerialPortSetting.PortName,
                    this.SerialPortSetting.BaudRate,
                    this.SerialPortSetting.Parity,
                    this.SerialPortSetting.DataBits,
                    this.SerialPortSetting.StopBits);
            //return sp;
            ICommuniPort cp = new SerialCommuniPort(sp);
            return cp;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cp"></param>
        /// <returns></returns>
        public bool IsMatch(ICommuniPort cp)
        {
            bool r = false;
            SerialCommuniPort scp = cp as SerialCommuniPort;
            if (scp != null)
            {
                bool isSameName = StringHelper.Equal(
                    scp.SerialPort.PortName, 
                    this.SerialPortSetting.PortName);
                r = isSameName;
            }
            return r;
        }

        /// <summary>
        /// 
        /// </summary>
        public SerialPortSetting SerialPortSetting
        {
            get
            {
                if (_serialPortSetting == null)
                {
                    _serialPortSetting = new SerialPortSetting(
                        "Com1",
                        9600,
                        Parity.None,
                        8,
                        StopBits.One);
                }
                return _serialPortSetting; 
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("SerialPortSetting");
                }
                _serialPortSetting = value;
            }
        } private SerialPortSetting _serialPortSetting;



        /// <summary>
        /// 
        /// </summary>
        public uint TimeoutMilliSecond
        {
            get
            {
                return _timeoutMillsSecond;
            }
            set
            {
                TimeoutDefauleValues.Verify(value);
                this._timeoutMillsSecond = value;
            }
        } private uint _timeoutMillsSecond = TimeoutDefauleValues.DefaultTimeoutMillsSecond;

    }

}
