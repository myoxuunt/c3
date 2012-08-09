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
    public partial class frmStationType : NUnit.UiKit.SettingsDialogBase
    {
        public frmStationType()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        private object DataSource
        {
            get { return C3App.App.Soft.SPUs; }
        }

        private void frmStationType_Load(object sender, EventArgs e)
        {

            //
            //
            this.Text = Strings.StationType;

            //
            this.lstStationType.DisplayMember = "StationType";
            this.lstStationType.ValueMember = "StationType";
            this.lstStationType.DataSource = this.DataSource;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        /// <summary>
        /// 
        /// </summary>
        public StationType SelectedStationType
        {
            get
            {
                StationType r = (StationType)this.lstStationType.SelectedItem;
                return r;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void okButton_Click(object sender, EventArgs e)
        {
            if (this.lstStationType.Items.Count > 0)
            {
                if (this.lstStationType.SelectedIndex >= 0)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
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
