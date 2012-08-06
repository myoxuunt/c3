using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace C3.Communi
{
    public partial class UCParameterUI : UserControl
    {
        public UCParameterUI()
        {
            InitializeComponent();
        }


        #region Parameter
        /// <summary>
        /// 
        /// </summary>
        public IParameter Parameter
        {
            get
            {
                //if (_deviceParameter == null)
                //{
                //_deviceParameter = new Parameter();
                //}
                return _deviceParameter;
            }
            set
            {
                _deviceParameter = value;
                if (_deviceParameter != null)
                {
                    this.lblName.Text = _deviceParameter.Text + ":";
                    this.lblUnit.Text = _deviceParameter.Unit.Text;
                    this.txtValue.Text = _deviceParameter.Value.ToString();
                }
            }
        } private IParameter _deviceParameter;
        #endregion //Parameter

        /// <summary>
        /// 
        /// </summary>
        internal void ApplyNewValue()
        {
            this.Parameter.SetValue(this.txtValue.Text);
        }

        private void UCParameterUI_Load(object sender, EventArgs e)
        {
            if (_deviceParameter != null)
            {
                this.lblName.Text = _deviceParameter.Text + ":";
                this.lblUnit.Text = _deviceParameter.Unit.Text;
                this.txtValue.Text = _deviceParameter.Value.ToString();
            }
        }
    }
}
