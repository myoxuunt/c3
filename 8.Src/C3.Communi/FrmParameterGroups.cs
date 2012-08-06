using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace C3.Communi
{
    public partial class FrmParameterGroups : Form
    {
        public FrmParameterGroups()
        {
            InitializeComponent();
        }


        #region ParameterGroups
        /// <summary>
        /// 
        /// </summary>
        public ParameterGroupCollection ParameterGroups
        {
            get
            {
                return _parameterGroups;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("ParameterGroups");
                }

                if (_parameterGroups != value)
                {
                    _parameterGroups = value;
                    Fill();
                }
            }
        } private ParameterGroupCollection _parameterGroups;
        #endregion //ParameterGroups

        /// <summary>
        /// 
        /// </summary>
        private void Fill()
        {
            foreach ( ParameterGroup item in this.ParameterGroups )
            {
                TabPage tp = new TabPage(item.Text);
                tp.Controls.Add(item.GroupUI.Control);
                tabControl1.TabPages.Add(tp);
            }
        }
    }
}
