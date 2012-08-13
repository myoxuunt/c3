using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Xdgk.Common;
using C3.Communi.P;

namespace C3.Communi.P
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
                        this.Controls.Add(c.Viewer.UC);
                    }
                }
            }
        } private GroupCollection _groups;
        #endregion //Groups

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
    }
}
