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
    public partial class frmGroup2 : NUnit.UiKit.SettingsDialogBase 
    {
        public frmGroup2()
        {
            InitializeComponent();
        }

        private void frmGroup2_Load(object sender, EventArgs e)
        {

        }

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
                    foreach (IController c in this.Controllers)
                    {
                        TabPage page = Create((GroupController)c);
                        this.tabControl1.TabPages.Add(page);
                        //this.Controls.Add(c.Viewer.UC);
                        c.UpdateViewer();
                    }
                }
            }
        } private GroupCollection _groups;
        #endregion //Groups

        private TabPage Create(GroupController gc)
        {
            TabPage page = new TabPage();
            page.Text = gc.Group.Text;
            page.Controls.Add(gc.Viewer.UC);

            return page;
        }

        private ControllerCollection Controllers
        {
            get
            {
                if (_controllers == null)
                {
                    _controllers = new ControllerCollection();
                    foreach (IGroup g in this.Groups)
                    {
                        IController c = ControllerFactory.Create(g);
                        _controllers.Add(c);
                    }
                }
                return _controllers;
            }
        } private ControllerCollection _controllers;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void okButton_Click(object sender, EventArgs e)
        {
            bool b = true;
            foreach (IController c in this.Controllers)
            {
                if (!c.Verify())
                {
                    b = false;
                    break;
                }
            }

            if (b)
            {
                foreach (IController c in this.Controllers)
                {
                    c.UpdateModel();
                }
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
