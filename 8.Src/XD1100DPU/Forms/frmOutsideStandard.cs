﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Xdgk.Common;
using C3.Communi;
using Xdgk.GR.Common;

namespace XD1100DPU
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

            StationCollection stations = SoftManager.GetSoft().Hardware.Stations;
            foreach (IStation st in stations)
            {
                foreach (IDevice d in st.Devices)
                {
                    if (d is IOutside)
                    {
                        kvs.Add(new KeyValue(d.Station.Name, d));
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
                OutsideTemperatureProviderManager.Provider = new DeviceOTProvider(otDevice);

                int deviceid = GuidHelper.ConvertToInt32(((IDevice)otDevice).Guid);
                DBI.Instance.SetOutsideTemperatureProviderDevice(deviceid);
            }
            else
            {
                DBI.Instance.ClearOutsideTemperatureProviderDevice();

                OutsideTemperatureProviderManager.Provider = null;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
