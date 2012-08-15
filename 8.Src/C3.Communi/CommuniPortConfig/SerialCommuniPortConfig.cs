
using System;
using System.IO.Ports;
using Xdgk.Common;


namespace C3.Communi
{
    public class SerialCommuniPortConfig : ICommuniPortConfig
    {

        public SerialCommuniPortConfig()
        {
        }

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

        public bool IsMatch(ICommuniPort cp)
        {
            // TODO:
            //
            throw new NotImplementedException();
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
    }

}
