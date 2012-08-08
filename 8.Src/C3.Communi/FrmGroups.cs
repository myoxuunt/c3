using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Xdgk.Common;

namespace C3.Communi
{
    public partial class FrmGroups :NUnit.UiKit.SettingsDialogBase 
    {

        #region FrmGroups
        public FrmGroups()
        {
            InitializeComponent();
        }
        #endregion //FrmGroups

        #region AdeStatus
        /// <summary>
        /// 
        /// </summary>
        public ADEStatus AdeStatus
        {
            get { return _adeStatus; }
            set { _adeStatus = value; }
        } private ADEStatus _adeStatus;
        #endregion //AdeStatus

        #region Groups
        /// <summary>
        /// 
        /// </summary>
        public GroupCollection Groups
        {
            get
            {
                return _groups;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("ParameterGroups");
                }

                if (_groups != value)
                {
                    _groups = value;
                    //Fill();
                }
            }
        } private GroupCollection _groups;
        #endregion //Groups

        #region Fill
        /// <summary>
        /// 
        /// </summary>
        protected virtual void Fill()
        {
            if ( this.Groups != null )
            {
                foreach (Group item in this.Groups)
                {
                    TabPage tp = new TabPage(item.Text);
                    tp.Controls.Add(item.GroupUI.Control);
                    tabControl1.TabPages.Add(tp);
                }
            }
        }
        #endregion //Fill

        #region FrmParameterGroups_Load
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmParameterGroups_Load(object sender, EventArgs e)
        {
            Fill();
        }
        #endregion //FrmParameterGroups_Load


        #region okButton_Click
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void okButton_Click(object sender, EventArgs e)
        {
            foreach (IGroup item in this.Groups)
            {
                item.GroupUI.ApplyNewValue();
            }

            if (this.Verify())
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
        #endregion //okButton_Click

        /// <summary>
        /// verify input new parameter and display error msg
        /// </summary>
        /// <returns></returns>
        virtual protected bool Verify()
        {
            throw new NotImplementedException("Verify");
        }

        #region cancelButton_Click
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        #endregion //cancelButton_Click
    }

}
