using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Xdgk.GR.UI
{
    public partial class frmOTGT2Value : NUnit.UiKit.SettingsDialogBase
    {
        /// <summary>
        /// 
        /// </summary>
        private int _otPrevious, _otNext;

        /// <summary>
        /// 
        /// </summary>
        public frmOTGT2Value( int ot, int gt2, int otPrevious, int otNext)
        {
            InitializeComponent();
            this.numOT.Minimum = Xdgk.GR.Data.OTControlLineDefines.OTMin;
            this.numOT.Maximum = Xdgk.GR.Data.OTControlLineDefines.OTMax;

            this.numGT2.Minimum = Xdgk.GR.Data.OTControlLineDefines.GT2Min;
            this.numGT2.Maximum = Xdgk.GR.Data.OTControlLineDefines.GT2Max;

            this.numOT.Value = ot;
            this.numGT2.Value = gt2;

            this._otPrevious = otPrevious;
            this._otNext = otNext;
        }

        /// <summary>
        /// 
        /// </summary>
        public int OT
        {
            get
            {
                return (int)this.numOT.Value;
            }
            set
            {
                if (value < this.numOT.Minimum)
                {
                    value = (int)this.numOT.Minimum;
                }
                if (value > this.numOT.Maximum)
                {
                    value = (int)this.numOT.Maximum;
                }
                this.numOT.Value = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int GT2
        {
            get { return (int)this.numGT2.Value; }
            set
            {
                if (value < this.numGT2.Minimum)
                {
                    value = (int)this.numGT2.Minimum;
                }
                if (value > this.numGT2.Maximum)
                {
                    value = (int)this.numGT2.Maximum;
                }
                this.numGT2.Value = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmOTGT2Value_Load(object sender, EventArgs e)
        {
        }

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
            if (!CheckOT())
            {
                string s = string.Format(XD100Strings.OTMustInMinMax,
                    _otPrevious, _otNext);
                NUnit.UiKit.UserMessage.DisplayFailure(s);
                return;
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        /// <summary>
        /// 
        /// </summary>
        private bool CheckOT()
        {
            int ot = (int)this.numOT.Value;
            return (ot >= _otPrevious) && (ot <= _otNext);
        }
    }
}
