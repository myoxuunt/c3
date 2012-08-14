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
            Debug.Assert(this.Parameter != null);
            Debug.Assert(this._currentUIControl != null);
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
        }
        #endregion //rbNull_Click

        #region rbSerialPort_Click
        private void rbSerialPort_Click(object sender, EventArgs e)
        {
            VisibleSetting(this.ucSerialPortSetting1);
        }
        #endregion //rbSerialPort_Click

        #region rbSocket_Click
        private void rbSocket_Click(object sender, EventArgs e)
        {
            VisibleSetting(this.ucNetSetting1);
        }
        #endregion //rbSocket_Click

        #region IParameterUIControl 成员
        public ICommuniPortConfig CommuniPortConfig
        {
            get 
            {
                ICommuniPortConfig r = null;
                if (this.rbNull.Checked)
                {
                    r = NullCommuniPortConfig.Default;
                }
                if (this.rbSerialPort.Checked)
                {
                    r = new SerialCommuniPortConfig(this.ucSerialPortSetting1.SerialPortSetting);
                }
                if (this.rbSocket.Checked)
                {
                    r = this.ucNetSetting1.CommuniPortConfig;
                }

                return r;
            }
            set
            {
                ICommuniPortConfig communiPortConfig = value;
                if (communiPortConfig is INetCommuniPortConfig)
                {
                    this.ucNetSetting1.CommuniPortConfig = communiPortConfig;

                    //CheckRadio ( this.rbSocket );
                    CheckRadio(this.rbSocket);
                    VisibleSetting(this.ucNetSetting1);
                    //this._currentUIControl = this.ucNetSetting1;
                }
                else if (communiPortConfig is NullCommuniPortConfig)
                {
                    CheckRadio(this.rbNull);
                    VisibleSetting(null);
                    this.ucNetSetting1.CommuniPortConfig = communiPortConfig;
                    //this._currentUIControl = null;
                }
                else if (communiPortConfig is SerialCommuniPortConfig)
                {
                    CheckRadio(this.rbSerialPort);
                    VisibleSetting(this.ucSerialPortSetting1);
                    //this._currentUIControl = this.ucSerialPortSetting1;
                    this.ucSerialPortSetting1.SerialPortSetting = ((SerialCommuniPortConfig)communiPortConfig).SerialPortSetting;
                }
            }
        }
        #region Parameter
        /// <summary>
        /// 
        /// </summary>
        public IParameter Parameter
        {
            get
            {
                return _parameter;
            }
            set
            {
                //if (value == null)
                //{
                //    throw new ArgumentNullException("Parameter");
                //}
                //_parameter = value;

                //ICommuniPortConfig communiPortConfig = (ICommuniPortConfig)value.Value;
                //if (communiPortConfig is INetCommuniPortConfig)
                //{
                //    this.ucNetSetting1.CommuniPortConfig = communiPortConfig;

                //    //CheckRadio ( this.rbSocket );
                //    CheckRadio(this.rbSocket);
                //    VisibleSetting(this.ucNetSetting1);
                //    this._currentUIControl = this.ucNetSetting1;
                //}
                //else if (communiPortConfig is NullCommuniPortConfig)
                //{
                //    CheckRadio(this.rbNull);
                //    VisibleSetting(null);
                //    this._currentUIControl = null;
                //}
                //else if (communiPortConfig is SerialCommuniPortConfig)
                //{
                //    CheckRadio(this.rbSerialPort);
                //    VisibleSetting(this.ucSerialPortSetting1);
                //    //this._currentUIControl = this.ucSerialPortSetting1;
                //}
                //this.CommuniPortConfig = (ICommuniPortConfig)_parameter.Value;
            }
        } private IParameter _parameter;
        #endregion //Parameter

        #region Verify
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool Verify()
        {
            throw new NotImplementedException();
        }
        #endregion //Verify

        #region ApplyNewValue
        ///// <summary>
        ///// 
        ///// </summary>
        //public void ApplyNewValue()
        //{
        //    if (rbSocket.Checked)
        //    {
        //        // TODO:
        //        //
        //        // check input 
        //        //
        //        this.ucNetSetting1.ApplyNewValue();
        //        this.Parameter.Value = this.ucNetSetting1.CommuniPortConfig;
        //    }
        //    else if (rbSerialPort.Checked)
        //    {
        //        // TODO:
        //        //
        //        //this.ucSerialPortSetting1.ApplyNewValue();
        //        //this.communiPortConfig = this.ucSerialPortSetting1 .communi
        //    }
        //    else if (rbNull.Checked)
        //    {
        //        this.Parameter.Value = NullCommuniPortConfig.Default;
        //    }
        //}
        #endregion //ApplyNewValue
        #endregion

        /// <summary>
        /// 
        /// </summary>
        private IParameterUIControl _currentUIControl;
    }
}
