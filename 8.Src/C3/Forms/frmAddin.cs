using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using C3.Communi ;

namespace C3
{
    public partial class frmAddin : NUnit.UiKit.FixedDialogBase
    {
        public frmAddin()
        {
            InitializeComponent();
            Init();
        }

        /// <summary>
        /// 
        /// </summary>
        private Soft Soft
        {
            get { return C3App.App.Soft; }
        }

        /// <summary>
        /// 
        /// </summary>
        private OptionTreeView OptionTreeView
        {
            get 
            {
                if (_optionTreeView==null)
                {
                    DisplayArea displayArea = new DisplayArea(this.label1, this.panel1);
                    _optionTreeView = new OptionTreeView(this.Soft, displayArea);
                    _optionTreeView.Dock = DockStyle.Fill;
                }
                return _optionTreeView;
            }
        } private OptionTreeView _optionTreeView;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmM_Load(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        private void Init()
        {
            this.splitContainer1.Panel1.Controls.Add(this.OptionTreeView);
        }

    }
}
