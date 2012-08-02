using System;
using System.Reflection;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace C3.Communi
{
    public partial class FrmDeviceUI : NUnit.UiKit.SettingsDialogBase
    {

        /// <summary>
        /// 
        /// </summary>
        public FrmDeviceUI()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        public IDevice Device
        {
            get { return _device; }
            set { _device = value; }
        } private IDevice _device;

        private Type _deviceType;
        private IStation _station;
        private ADEStatus _adeStatus;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="deviceType"></param>
        /// <param name="station"></param>
        /// <param name="device"></param>
        /// <returns></returns>
        static public DialogResult Add(Type deviceType, IStation station, out IDevice newDevice)
        {
            newDevice = null;
            FrmDeviceUI f = new FrmDeviceUI();

            f._deviceType = deviceType;
            f._device = (IDevice)Activator.CreateInstance(f._deviceType);
            f._station = station;
            f._adeStatus = ADEStatus.Add;

            DialogResult dr = f.ShowDialog();
            if (dr == DialogResult.OK)
            {
                newDevice = f.Device;
            }
            return dr;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        static public DialogResult Edit(IDevice device)
        {
            FrmDeviceUI f = new FrmDeviceUI();

            f._device = device;
            f._station = device.Station;
            f._deviceType = device.GetType();
            f._adeStatus = ADEStatus.Edit;

            DialogResult dr = f.ShowDialog();
            return dr;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmDeviceUI_Load(object sender, EventArgs e)
        {
            FillDeviceInfo();
        }

        /// <summary>
        /// 
        /// </summary>
        private void FillDeviceInfo()
        {
            string s = string.Empty;
            foreach (DeviceInfoAttribute item in this.DeviceInfoAttributes)
            {
                PropertyInfo pi = item.PropertyInfo;
                object value = pi.GetValue(this._device, null);
                s += string.Format("{0}:{1}\r\n", item.Name, value);
            }
            this.richTextBox1.Text = s;
        }


        #region DeviceInfoAttributes


        /// <summary>
        /// 
        /// </summary>
        private DeviceInfoAttributeCollection DeviceInfoAttributes
        {
            get
            {
                if (_deviceInfoAttributes == null)
                {
                    _deviceInfoAttributes = new DeviceInfoAttributeCollection();

                    PropertyInfo[] propertyInfos = this._device.GetType().GetProperties();
                    foreach (PropertyInfo pi in propertyInfos)
                    {
                        object[] atts = pi.GetCustomAttributes(typeof(DeviceInfoAttribute), false);
                        if (atts.Length > 0)
                        {
                            DeviceInfoAttribute att = (DeviceInfoAttribute)atts[0];
                            att.PropertyInfo = pi;

                            _deviceInfoAttributes.Add(att);
                        }
                    }

                    // sort
                    //
                    _deviceInfoAttributes.Sort();
                }
                return _deviceInfoAttributes;
            }
        } private DeviceInfoAttributeCollection _deviceInfoAttributes;
        #endregion //DeviceInfoAttributes
    }
}