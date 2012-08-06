using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace C3.Communi
{
    public partial class UCNumberParameterUI : UserControl
    {
        public UCNumberParameterUI()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        public IParameter Parameter
        {
            get { return _parameter; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Parameter");
                }
                if (_parameter != value)
                {
                    _parameter = value;
                }
            }
        } private IParameter _parameter;

        /// <summary>
        /// 
        /// </summary>
        private void Fill()
        {
            this.lblName.Text = this.Parameter.Text + ":";
            this.lblUnit.Text = this.Parameter.Unit.Text;
            this.txtValue.Text = this.Parameter.Value.ToString();
        }

        internal void ApplyNewValue()
        {
            object val = Convert.ChangeType(this.txtValue.Text, this.Parameter.ValueType);
            this.Parameter.Value = val;
        }

        private void UCNumberParameterUI_Load(object sender, EventArgs e)
        {
            Fill();
        }
    }
}
