using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Xdgk.GR.UI
{
    public partial class frmTimeValue : NUnit.UiKit.SettingsDialogBase 
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="time"></param>
        /// <param name="adjust"></param>
        /// <param name="gtbase2"></param>
        public frmTimeValue(int time, int adjust, int gtbase2)
        {
            InitializeComponent();

            this.numAdjust.Minimum = Xdgk.GR.Common.TimeControlLineDefines.MinAdjustValue;
            this.numAdjust.Maximum = Xdgk.GR.Common.TimeControlLineDefines.MaxAdjustValue;

            this.Time = time;


            this.Adjust = adjust;
            //this.GTBase2 = gtbase2;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmTimeValue_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblTime_Click(object sender, EventArgs e)
        {

        }


        #region Time
        /// <summary>
        /// 
        /// </summary>
        public int Time
        {
            get { return Convert.ToInt32(txtTime.Text); }
            set { this.txtTime.Text = value.ToString(); }
        } 
        #endregion //Time

        #region Adjust
        /// <summary>
        /// 
        /// </summary>
        public int Adjust
        {
            get { return Convert.ToInt32(this.numAdjust.Value ); }
            set
            {
                if (value < Xdgk.GR.Common.TimeControlLineDefines.MinAdjustValue)
                {
                    value = Xdgk.GR.Common.TimeControlLineDefines.MinAdjustValue;
                }
                if (value > Xdgk.GR.Common.TimeControlLineDefines.MaxAdjustValue)
                {
                    value = Xdgk.GR.Common.TimeControlLineDefines.MaxAdjustValue;
                }
                this.numAdjust.Value = value;
            }
        }
        #endregion //Adjust

        private void cancelButton_Click(object sender, EventArgs e)
        {
        
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void okButton_Click(object sender, EventArgs e)
        {
            if (CheckAdjust())
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool CheckAdjust()
        {
            if (!Xdgk.GR.Common.TimeControlLineDefines.CheckTimeAdjustValue(this.Adjust))
            {
                NUnit.UiKit.UserMessage.Display(XD100Strings.AdjustValueOutOfRange);
                return false;
            }
            return true;
        }

        //#region GTBase2
        ///// <summary>
        ///// 
        ///// </summary>
        //public int GTBase2
        //{
        //    get { return ; }
        //    set { _gTBase2 = value; }
        //}
        //#endregion //GTBase2

    }
}
