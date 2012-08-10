using System;
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
        public UCCommuniPortConfigUI()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UCCommuniPortConfig_Load(object sender, EventArgs e)
        {
            this.ucNetSetting1.Location = SettingControlLocal;
        }

        public ICommuniPortConfig CommuniPortConfig
        {
            get { return _communiPortConfig; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("CommuniPortConfig");
                }

                _communiPortConfig = value;
                if (_communiPortConfig is INetCommuniPortConfig)
                {
                    this.ucNetSetting1.CommuniPortConfig = _communiPortConfig;

                    CheckRadio ( this.rbSocket );
                    VisibleSetting(this.ucNetSetting1);
                }
                else if (_communiPortConfig is NullCommuniPortConfig)
                {
                }
                //else if _communiPortConfig is Serialprotconfig
                else
                {
                }
            }
        } private ICommuniPortConfig _communiPortConfig;

        /// <summary>
        /// 
        /// </summary>
        public void ApplyNewValue()
        {
            if (rbSocket.Checked )
            {
                this.ucNetSetting1.ApplyNewValue();
                this._communiPortConfig = this.ucNetSetting1.CommuniPortConfig;
            }
            else if (rbSerialPort.Checked)
            {
                // TODO:
                //
                //this.ucSerialPortSetting1.ApplyNewValue();
                //this._communiPortConfig = this.ucSerialPortSetting1 .communi

            }


        }

        private void CheckRadio(RadioButton rdo)
        {
            rdo.Checked = true;
        }

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

        /// <summary>
        /// 
        /// </summary>
        private Point SettingControlLocal
        {
            get { return this.ucSerialPortSetting1.Location; }
        }
    }
}
