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
    public partial class frmGroups : NUnit.UiKit.SettingsDialogBase
    {

        #region frmGroups
        public frmGroups()
        {
            InitializeComponent();
        }
        #endregion //frmGroups

        #region frmGroup2_Load
        private void frmGroup2_Load(object sender, EventArgs e)
        {
            SetFormText();
        }
        #endregion //frmGroup2_Load

        #region SetFormText
        /// <summary>
        /// 
        /// </summary>
        virtual protected void SetFormText()
        {
        }
        #endregion //SetFormText

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

        #region Create
        private TabPage Create(GroupController gc)
        {
            TabPage page = new TabPage();
            page.Text = gc.Group.Text;
            page.Controls.Add(gc.Viewer.UC);

            return page;
        }
        #endregion //Create

        #region Controllers
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
        #endregion //Controllers

        #region okButton_Click
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void okButton_Click(object sender, EventArgs e)
        {
            bool b = true;
            bool b2 = true;

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
                b2 = Verify2();
            }

            if (b && b2)
            {
                foreach (IController c in this.Controllers)
                {
                    c.UpdateModel();
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
        #endregion //okButton_Click

        #region Verify2
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        virtual protected bool Verify2()
        {
            return true;
        }
        #endregion //Verify2
    }
}
