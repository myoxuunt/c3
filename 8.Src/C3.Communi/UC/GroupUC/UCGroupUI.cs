﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace C3.Communi
{
    public partial class UCGroupUI : UserControl
    {
        public UCGroupUI()
        {
            InitializeComponent();

            this.tableLayoutPanel1.RowCount = 0;
            this.tableLayoutPanel1.Controls.Clear();
            this.tableLayoutPanel1.RowStyles.Clear();
        }

        #region Group
        /// <summary>
        /// 
        /// </summary>
        public IGroup Group
        {
            get
            {
                return _group;
            }
            set
            {
                _group = value;
            }
        } private IGroup _group;
        #endregion //Group

        /// <summary>
        /// 
        /// </summary>
        /// <param name="c"></param>
        public void AddControl(Control c)
        {
            this.tableLayoutPanel1.RowCount += 1;
            this.tableLayoutPanel1.Controls.Add(c, 0, this.tableLayoutPanel1.RowCount - 1);
            RowStyle style = new RowStyle(SizeType.Absolute, c.Height);
            this.tableLayoutPanel1.RowStyles.Add(style);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UCGroupUI_Load(object sender, EventArgs e)
        {
        }
    }
}
