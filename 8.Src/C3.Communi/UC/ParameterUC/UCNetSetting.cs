using System;
using System.Diagnostics;
using System.Net;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace C3.Communi.UC.Parameter
{
    public partial class UCNetSetting : UserControl
    {
        public UCNetSetting()
        {
            InitializeComponent();

            // 
            //
            this.panelByIPAddress.Location = PanelLocal;
            this.panelByLocalPort.Location = PanelLocal;
            this.panelBylRemotePort.Location = PanelLocal;

            //
            //
            FillDiscriminateMode();
        }

        private Point PanelLocal
        {
            get
            {
                return this.panelByLocalPort.Location;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UCNetSetting_Load(object sender, EventArgs e)
        {

        }

        public ICommuniPortConfig CommuniPortConfig
        {
            get
            {
                ICommuniPortConfig r = null;
                DiscriminateMode dm = (DiscriminateMode)this.cmbDiscriminateMode.SelectedValue;
                switch (dm)
                {
                    case DiscriminateMode.ByIPAddress:
                        r = new RemoteIPAddressConfig(this.IPAddress);
                        break;

                    case DiscriminateMode.ByRemotePort:
                        r = new RemotePortConfig((int)this.numRemotePort.Value);
                        break;

                    case DiscriminateMode.ByLocalPort:
                        r = new LocalPortConfig((int)this.numLocalPort.Value);
                        break;

                    case DiscriminateMode.ByPhoneNumber:
                        break;

                    default:
                        break;
                }
                Debug.Assert(r != null);
                return r;
            }
            set
            {

                if (value == null)
                {
                    return;
                    throw new ArgumentNullException("CommuniPortConfig");
                }
                if (!(value is INetCommuniPortConfig))
                {
                    throw new ArgumentException("CommuniPortConfig is not INetCommuniPortConfig");
                }

                //_communiPortConfig = value;
                DiscriminateMode dm = StationDiscriminateMode.Find(value.GetType());
                this.cmbDiscriminateMode.SelectedValue = dm;

                // set value
                //
                SetValue((INetCommuniPortConfig)value);

            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cfg"></param>
        private void SetValue(INetCommuniPortConfig cfg)
        {
            if (cfg is RemoteIPAddressConfig)
            {
                RemoteIPAddressConfig ipCfg = cfg as RemoteIPAddressConfig;
                this.txtStationIP.Text = ipCfg.RemoteIPAddress.ToString();
            }
            else if (cfg is RemotePortConfig)
            {
                RemotePortConfig rpCfg = cfg as RemotePortConfig;
                this.numRemotePort.Value = rpCfg.RemotePort;
            }
            else if (cfg is LocalPortConfig)
            {
                LocalPortConfig lpCfg = cfg as LocalPortConfig;
                this.numLocalPort.Value = lpCfg.LocalPort;
            }
            else
            {
                throw new NotImplementedException(cfg.ToString());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private IPAddress IPAddress
        {
            get { return IPAddress.Parse(this.txtStationIP.Text); }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool CheckIPAddress()
        {
            IPAddress ip;
            bool b = IPAddress.TryParse(this.txtStationIP.Text, out ip);
            if (!b)
            {
                NUnit.UiKit.UserMessage.DisplayFailure(strings.InvalidIPAddress);
                return false;
            }
            return true;
        }
        /// <summary>
        /// 
        /// </summary>
        private void FillDiscriminateMode()
        {
            this.cmbDiscriminateMode.DataSource = StationDiscriminateMode.Items;
            this.cmbDiscriminateMode.DisplayMember = "Text";
            this.cmbDiscriminateMode.ValueMember = "DiscriminateMode";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dm"></param>
        private void VisiblePanel(DiscriminateMode dm)
        {
            panelByIPAddress.Visible = dm == DiscriminateMode.ByIPAddress;
            panelByLocalPort.Visible = dm == DiscriminateMode.ByLocalPort;
            panelBylRemotePort.Visible = dm == DiscriminateMode.ByRemotePort;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbDiscriminateMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            object obj = this.cmbDiscriminateMode.SelectedItem;
            StationDiscriminateMode.Item item = obj as StationDiscriminateMode.Item;
            DiscriminateMode dm = item.DiscriminateMode;
            VisiblePanel(dm);
        }

        #region IParameterUIControl 成员

        public bool Verify()
        {
            DiscriminateMode dm = (DiscriminateMode)this.cmbDiscriminateMode.SelectedValue;
            if (dm == DiscriminateMode.ByIPAddress)
            {
                IPAddress ip;
                bool r = IPAddress.TryParse(this.txtStationIP.Text, out ip);
                if (!r)
                {
                    NUnit.UiKit.UserMessage.DisplayFailure(strings.InvalidIPAddress);
                }

                return r;
            }
            else
            {
                return true;
            }
        }

        #endregion
    }
}
