using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using C3.Communi;

namespace C3
{
    public partial class frmDeviceType : NUnit.UiKit.SettingsDialogBase 
    {
        /// <summary>
        /// 
        /// </summary>
        public frmDeviceType()
        {
            InitializeComponent();
        }

        private object DataSource
        {
            get { return C3App.App.Soft.DPUs; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmDeviceType_Load(object sender, EventArgs e)
        {
            //
            //
            SetFormText();

            this.lstDeviceType.DisplayMember = "DeviceType";
            this.lstDeviceType.ValueMember = "DeviceType";
            this.lstDeviceType.DataSource = this.DataSource;

            if (this.SelectedDeviceType != null)
            {
                foreach (object obj in this.lstDeviceType.Items)
                {
                    IDPU dpu = (IDPU)obj;
                    if (dpu.DeviceType == SelectedDeviceType)
                    {
                        this.lstDeviceType.SelectedItem = obj;
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void SetFormText()
        {
            // TODO:
            //
        }

        /// <summary>
        /// 
        /// </summary>
        public DeviceType SelectedDeviceType
        {
            get { return _selectedDeviceType; }
            set 
            { 
                _selectedDeviceType = value; 
            }
        } private DeviceType _selectedDeviceType;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void okButton_Click(object sender, EventArgs e)
        {
            if (this.lstDeviceType.Items.Count > 0)
            {
                if (this.lstDeviceType.SelectedIndex >= 0)
                {
                    this.SelectedDeviceType = (DeviceType)this.lstDeviceType.SelectedValue;
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    NUnit.UiKit.UserMessage.DisplayFailure("select first");
                }
            }
            else
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }
    }
}
