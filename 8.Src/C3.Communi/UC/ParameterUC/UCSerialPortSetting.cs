using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Windows.Forms;

namespace C3.Communi
{
    using KV = KeyValuePair<string, object>;
    using KVList = List<KeyValuePair<string, object>>;

    /// <summary>
    /// 
    /// </summary>
    public partial class UCSerialPortSetting : UserControl
    {
        public UCSerialPortSetting()
        {
            InitializeComponent();

            BindDataSource();
        }

        /// <summary>
        /// 
        /// </summary>
        private void BindDataSource()
        {
            BindBaudRateDataSource();
            BindDataBitsDataSource();
            BindStopBitsDataSource();
            BindParityBitsDataSource();
        }

        #region ParityDataSource
        /// <summary>
        /// 
        /// </summary>
        private KVList ParityDataSource
        {
            get
            {
                if (_parityDataSource == null)
                {
                    _parityDataSource = new KVList();
                    _parityDataSource.Add(new KV("无", Parity.None));
                    _parityDataSource.Add(new KV("奇校验", Parity.Even));
                    _parityDataSource.Add(new KV("偶校验", Parity.Odd));
                    _parityDataSource.Add(new KV("标记", Parity.Mark));
                    _parityDataSource.Add(new KV("空格", Parity.Space));
                }
                return _parityDataSource;
            }
        } private KVList _parityDataSource;
        #endregion //ParityDataSource

        #region StopBitsDataSource
        /// <summary>
        /// 
        /// </summary>
        private KVList StopBitsDataSource
        {
            get
            {
                if (_stopBitsDataSource == null)
                {
                    _stopBitsDataSource = new KVList();
                    _stopBitsDataSource.Add(new KV("1", StopBits.One));
                    _stopBitsDataSource.Add(new KV("1.5", StopBits.OnePointFive));
                    _stopBitsDataSource.Add(new KV("2", StopBits.Two));
                }
                return _stopBitsDataSource;
            }
        } private KVList _stopBitsDataSource;
        #endregion //StopBitsDataSource


        #region DataBitsDataSource
        /// <summary>
        /// 
        /// </summary>
        public KVList DataBitsDataSource
        {
            get
            {
                if (_dataBitsDataSource == null)
                {
                    _dataBitsDataSource = new KVList();
                    _dataBitsDataSource.Add(new KV("5", 5));
                    _dataBitsDataSource.Add(new KV("6", 6));
                    _dataBitsDataSource.Add(new KV("7", 7));
                    _dataBitsDataSource.Add(new KV("8", 8));
                }
                return _dataBitsDataSource;
            }
        } private KVList _dataBitsDataSource;
        #endregion //DataBitsDataSource

        #region BaudRateDataSource
        /// <summary>
        /// 
        /// </summary>
        public KVList BaudRateDataSource
        {
            get
            {
                if (_baudRateDataSource == null)
                {
                    _baudRateDataSource = new KVList();
                    _baudRateDataSource.Add(new KV("1200", 1200));
                    _baudRateDataSource.Add(new KV("2400", 2400));
                    _baudRateDataSource.Add(new KV("4800", 4800));
                    _baudRateDataSource.Add(new KV("9600", 9600));
                    _baudRateDataSource.Add(new KV("19200", 19200));
                    _baudRateDataSource.Add(new KV("38400", 38400));
                    _baudRateDataSource.Add(new KV("57600", 57600));
                    _baudRateDataSource.Add(new KV("115200", 115200));
                }
                return _baudRateDataSource;
            }
        } private KVList _baudRateDataSource;
        #endregion //BaudRateDataSource


        #region BindParityBitsDataSource
        /// <summary>
        /// 
        /// </summary>
        private void BindParityBitsDataSource()
        {
            BindComboBoxDataSource(this.cbParity, this.ParityDataSource);
        }
        #endregion //BindParityBitsDataSource

        #region BindComboBoxDataSource
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cb"></param>
        /// <param name="ds"></param>
        private void BindComboBoxDataSource(ComboBox cb, KVList ds)
        {
            cb.DataSource = ds;
            cb.DisplayMember = "Key";
            cb.ValueMember = "Value";
        }
        #endregion //BindComboBoxDataSource

        #region BindStopBitsDataSource
        private void BindStopBitsDataSource()
        {
            BindComboBoxDataSource(this.cbStopBits, this.StopBitsDataSource);
        }
        #endregion //BindStopBitsDataSource

        #region BindDataBitsDataSource
        private void BindDataBitsDataSource()
        {
            BindComboBoxDataSource(this.cbDataBits, DataBitsDataSource);
            this.cbDataBits.SelectedValue = 8;
        }
        #endregion //BindDataBitsDataSource

        #region BindBaudRateDataSource
        /// <summary>
        /// 
        /// </summary>
        private void BindBaudRateDataSource()
        {
            BindComboBoxDataSource(this.cbBaudRate, this.BaudRateDataSource);
            this.cbBaudRate.SelectedValue = 9600;
        }
        #endregion //BindBaudRateDataSource

        /// <summary>
        /// 
        /// </summary>
        private void Fill()
        {
            //SerialCommuniPortConfig t = (SerialCommuniPortConfig)this.Parameter.Value ;
            //SerialPortSetting value = t.SerialPortSetting;

            //this.cbBaudRate.SelectedValue = value.BaudRate;
            //this.cbParity.SelectedValue = value.Parity;
            //this.cbDataBits.SelectedValue = value.DataBits;
            //this.cbStopBits.SelectedValue = value.StopBits;
        }

        //#region SerialCommuniType
        ///// <summary>
        ///// 
        ///// </summary>
        //public SerialCommuniType SerialCommuniType
        //{
        //    get
        //    {
        //        SerialPortSetting setting = this.SerialPortSetting ;

        //        SerialCommuniType ct = new SerialCommuniType(
        //                this.txtSerialPortName.Text.Trim(),
        //                setting.BaudRate,
        //                setting.Parity,
        //                setting.DataBits,
        //                setting.StopBits
        //                );
        //        return ct;
        //    }
        //    set
        //    {
        //        this.txtSerialPortName.Text = value.PortName;

        //        SerialPortSetting setting = new SerialPortSetting( value.BaudRate,
        //                value.Parity,
        //                value.DataBits,
        //                value.StopBits);

        //        this.SerialPortSetting = setting;
        //    }
        //}
        //#endregion //SerialCommuniType

        #region SerialPortSetting
        /// <summary>
        /// 
        /// </summary>
        public SerialPortSetting SerialPortSetting
        {
            get
            {
                string portName = this.txtSerialPortName.Text;
                int baudRate = (int)this.cbBaudRate.SelectedValue;
                Parity p = (Parity)this.cbParity.SelectedValue;
                int databits = (int)this.cbDataBits.SelectedValue;
                StopBits stopbits = (StopBits)this.cbStopBits.SelectedValue;

                SerialPortSetting t = new SerialPortSetting(
                    portName,
                    baudRate,
                    p,
                    databits,
                    stopbits);

                return t;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("SerialPortSetting");
                }

                this.txtSerialPortName.Text = value.PortName;
                this.cbBaudRate.SelectedValue = value.BaudRate;
                this.cbParity.SelectedValue = value.Parity;
                this.cbDataBits.SelectedValue = value.DataBits;
                this.cbStopBits.SelectedValue = value.StopBits;
            }
        }
        #endregion //SerialPortSetting

        #region UCSerialPortSetting_Load
        private void UCSerialPortSetting_Load(object sender, EventArgs e)
        {

        }
        #endregion //UCSerialPortSetting_Load

        #region VerifyPortName
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool VerifyPortName()
        {
            string s = this.txtSerialPortName.Text.Trim();
            if (s.Length > 0)
            {
                return true;
            }
            else
            {
                NUnit.UiKit.UserMessage.DisplayFailure(strings.InvalidSerialPortName);
                return false;
            }
        }
        #endregion //VerifyPortName
    }
}
