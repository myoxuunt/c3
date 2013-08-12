
using System;
using System.IO.Ports;
using Xdgk.Common;


namespace C3.Communi
{
    /// <summary>
    /// 
    /// </summary>
    public class SerialCommuniPortConfig : CommuniPortConfigBase, ICommuniPortConfig
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
        override public bool CanCreate
        {
            get { return true; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        override public ICommuniPort Create()
        {
            SerialPort sp = new SerialPort(
                    this.SerialPortSetting.PortName,
                    this.SerialPortSetting.BaudRate,
                    this.SerialPortSetting.Parity,
                    this.SerialPortSetting.DataBits,
                    this.SerialPortSetting.StopBits);

            sp.Open();
            //return sp;
            ICommuniPort cp = new SerialCommuniPort(sp);
            return cp;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cp"></param>
        /// <returns></returns>
        override public bool IsMatch(ICommuniPort cp)
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
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            //return base.Equals(obj);
            if (obj == null)
            {
                return false;
            }

            if (obj.GetType() != typeof(SerialCommuniPortConfig))
            {
                return false;
            }

            SerialCommuniPortConfig cfg = (SerialCommuniPortConfig)obj;

            return this.SerialPortSetting.Equals(cfg.SerialPortSetting);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.SerialPortSetting.ToString();
        }
    }
}
