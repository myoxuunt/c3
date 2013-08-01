using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Xdgk.Common;
using C3.Communi;

namespace Xdgk.GR.Common
{
    /// <summary>
    /// 
    /// </summary>
    public partial class frmOutsideStandard :  NUnit.UiKit.SettingsDialogBase 
    {
        public frmOutsideStandard()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmOutsideStandard_Load(object sender, EventArgs e)
        {
            Fill();
        }

        /// <summary>
        /// 
        /// </summary>
        KeyValueCollection kvs = new KeyValueCollection();

        /// <summary>
        /// 
        /// </summary>
        private void Fill()
        {
            int no = 1;

            StationCollection stations = SoftManager.GetSoft().Hardware.Stations;
            foreach (IStation st in stations)
            {
                foreach (IDevice d in st.Devices)
                {
                    if (d is IOutside)
                    {
                        string keyName = string.Format("{0}: {1}({2})", no++, d.Station.Name, d.DeviceType.Text);
                        KeyValue kv = new KeyValue(keyName, d);
                        kvs.Add(kv);
                    }
                }
            }

            //
            //
            kvs.Insert(0, new KeyValue("<无>", null));

            this.cmbStandard.DisplayMember = "Key";
            this.cmbStandard.ValueMember = "Value";
            this.cmbStandard.DataSource = kvs;


            IOutsideTemperatureProvider p = OutsideTemperatureProviderManager.Provider;
            if (p is DeviceOTProvider)
            {
                this.cmbStandard.SelectedValue = ((DeviceOTProvider)p).Outside;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void okButton_Click(object sender, EventArgs e)
        {
            object sel = this.cmbStandard.SelectedValue;
            if (sel != null)
            {
                IOutside otDevice = (IOutside)sel;
                //OutsideTemperatureProviderManager.Provider = new DeviceOTProvider(otDevice);

                //int deviceid = GuidHelper.ConvertToInt32(((IDevice)otDevice).Guid);
                ////DBI.Instance.SetOutsideTemperatureProviderDevice(deviceid);
                //this._selectedOutSideTemperatureDeviceID = deviceid;
                this._selectedOutSide = otDevice;
            }
            else
            {
                //DBI.Instance.ClearOutsideTemperatureProviderDevice();

                //OutsideTemperatureProviderManager.Provider = null;
                //this._selectedOutSideTemperatureDeviceID = 0;
                this._selectedOutSide = null;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        /// <summary>
        /// 
        /// </summary>
        public IOutside SelectedOutSide
        {
            get { return _selectedOutSide; }
        } private IOutside _selectedOutSide;

        ///// <summary>
        ///// 0 is not selected device
        ///// </summary>
        //public int SelectedOutSideTemperatureDeviceID
        //{
        //    get { return _selectedOutSideTemperatureDeviceID; }
        //} private int _selectedOutSideTemperatureDeviceID;
    }
}
