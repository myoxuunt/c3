using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace C3.Communi
{
    public partial class UCCommuniPortConfigUI : UserControl
    {
        private void Assert()
        {
            //Debug.Assert(this.Parameter != null);
            //Debug.Assert(this._currentUIControl != null);
        }

        #region UCCommuniPortConfigUI
        public UCCommuniPortConfigUI()
        {
            InitializeComponent();
        }
        #endregion //UCCommuniPortConfigUI

        #region UCCommuniPortConfig_Load
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UCCommuniPortConfig_Load(object sender, EventArgs e)
        {
            this.ucNetSetting1.Location = SettingControlLocal;
            //this._currentUIControl
            Assert();
        }
        #endregion //UCCommuniPortConfig_Load

        //#region CommuniPortConfig
        ///// <summary>
        ///// 
        ///// </summary>
        //private ICommuniPortConfig CommuniPortConfig
        //{
        //    get { return (ICommuniPortConfig)this.Parameter.Value; }
        //    set
        //    {
        //        if (value == null)
        //        {
        //            throw new ArgumentNullException("CommuniPortConfig");
        //        }

        //        ICommuniPortConfig communiPortConfig = value;
        //        if (communiPortConfig is INetCommuniPortConfig)
        //        {
        //            this.ucNetSetting1.CommuniPortConfig = communiPortConfig;

        //            //CheckRadio ( this.rbSocket );
        //            CheckRadio(this.rbSocket);
        //            VisibleSetting(this.ucNetSetting1);
        //        }
        //        else if (communiPortConfig is NullCommuniPortConfig)
        //        {
        //            CheckRadio(this.rbNull);
        //            VisibleSetting(null);
        //        }
        //        else if( communiPortConfig  is SerialCommuniPortConfig )
        //        {
        //            CheckRadio(this.rbSerialPort);
        //            VisibleSetting(this.ucSerialPortSetting1);
        //        }
        //    }
        //} //private ICommuniPortConfig communiPortConfig;
        //#endregion //CommuniPortConfig

        #region CheckRadio
        private void CheckRadio(RadioButton rdo)
        {
            rdo.Checked = true;
        }
        #endregion //CheckRadio

        #region VisibleSetting
        /// <summary>
        /// 
        /// </summary>
        /// <param name="c"></param>
        private void VisibleSetting(Control c)
        {
            Control[] cs = new Control[] { this.ucNetSetting1, this.ucSerialPortSetting1 };
            foreach (Control item in cs)
            {
                item.Visible = c == item;
            }
        }
        #endregion //VisibleSetting

        #region SettingControlLocal
        /// <summary>
        /// 
        /// </summary>
        private Point SettingControlLocal
        {
            get { return this.ucSerialPortSetting1.Location; }
        }
        #endregion //SettingControlLocal

        #region rbNull_CheckedChanged
        private void rbNull_CheckedChanged(object sender, EventArgs e)
        {

        }
        #endregion //rbNull_CheckedChanged

        #region rbNull_Click
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbNull_Click(object sender, EventArgs e)
        {
            VisibleSetting(null);
            VisibleTimeout(false);
        }
        #endregion //rbNull_Click

        /// <summary>
        /// 
        /// </summary>
        /// <param name="b"></param>
        private void VisibleTimeout(bool b)
        {
            this.lblTimeout.Visible = b;
            this.numTimeout.Visible = b;
            this.lblTimeoutUnit.Visible = b;
        }

        #region rbSerialPort_Click
        private void rbSerialPort_Click(object sender, EventArgs e)
        {
            VisibleSetting(this.ucSerialPortSetting1);
            VisibleTimeout(true);
        }
        #endregion //rbSerialPort_Click

        #region rbSocket_Click
        private void rbSocket_Click(object sender, EventArgs e)
        {
            VisibleSetting(this.ucNetSetting1);
            VisibleTimeout(true);
        }
        #endregion //rbSocket_Click

        private uint GetTimeoutValue()
        {
            return (uint)(this.numTimeout.Value * 1000);
        }

        #region IParameterUIControl 成员
        public ICommuniPortConfig CommuniPortConfig
        {
            get 
            {
                ICommuniPortConfig r = null;
                if (this.rbNull.Checked)
                {
                    r = NullCommuniPortConfig.Default;
                    r.TimeoutMilliSecond = GetTimeoutValue();
                }
                if (this.rbSerialPort.Checked)
                {
                    r = new SerialCommuniPortConfig(this.ucSerialPortSetting1.SerialPortSetting);
                    r.TimeoutMilliSecond = GetTimeoutValue();
                }
                if (this.rbSocket.Checked)
                {
                    r = this.ucNetSetting1.CommuniPortConfig;
                    r.TimeoutMilliSecond = GetTimeoutValue();
                }
                Debug.Assert(r != null,"r == null");
                return r;
            }
            set
            {
                ICommuniPortConfig communiPortConfig = value;
                this.numTimeout.Value = communiPortConfig.TimeoutMilliSecond / 1000;

                if (communiPortConfig is INetCommuniPortConfig)
                {
                    this.ucNetSetting1.CommuniPortConfig = communiPortConfig;

                    //CheckRadio ( this.rbSocket );
                    CheckRadio(this.rbSocket);
                    VisibleSetting(this.ucNetSetting1);
                    VisibleTimeout(true);
                    //this._currentUIControl = this.ucNetSetting1;
                }
                else if (communiPortConfig is NullCommuniPortConfig)
                {
                    CheckRadio(this.rbNull);
                    VisibleSetting(null);
                    VisibleTimeout(false);
                    //this.ucNetSetting1.CommuniPortConfig = communiPortConfig;
                    //this._currentUIControl = null;
                }
                else if (communiPortConfig is SerialCommuniPortConfig)
                {
                    CheckRadio(this.rbSerialPort);
                    VisibleSetting(this.ucSerialPortSetting1);
                    VisibleTimeout(true);
                    //this._currentUIControl = this.ucSerialPortSetting1;
                    this.ucSerialPortSetting1.SerialPortSetting = ((SerialCommuniPortConfig)communiPortConfig).SerialPortSetting;
                }
            }
        }

        #region Verify
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool Verify()
        {
            bool r = false;

            if (this.rbSerialPort.Checked)
            {
                r = (this.ucSerialPortSetting1.VerifyPortName());
            }
            else if (this.rbSocket.Checked)
            {
                r = this.ucNetSetting1.Verify();
            }
            else if (this.rbNull.Checked)
            {
                r = true;
            }
            return r;
        }
        #endregion //Verify

        #endregion
    }
}
