using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Xdgk.Common;

namespace C3.Communi
{
    public partial class UCComboBoxParameterUI : UserControl
    {
        public UCComboBoxParameterUI()
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
                return _deviceParameter;
            }
            set
            {
                _deviceParameter = value;
                if (_deviceParameter != null)
                {
                    RefreshParameter();
                }
            }
        }

        private void RefreshParameter()
        {
            this.lblName.Text = _deviceParameter.Text + ":";

            this.cmbValue.DisplayMember = "Key";
            this.cmbValue.ValueMember = "Value";
            this.cmbValue.DataSource = this.DataSource;
            this.cmbValue.SelectedValue = _deviceParameter.Value;

            this.lblUnit.Text = _deviceParameter.Unit.Text;
        } private IParameter _deviceParameter;
        #endregion //Parameter

        public KeyValueCollection NameTexts
        {
            get
            {
                if (_nameTexts == null)
                {
                    _nameTexts = new KeyValueCollection();
                }
                return _nameTexts;
            }
            set
            {
                _nameTexts = value;
            }
        } private KeyValueCollection _nameTexts;

        /// <summary>
        /// 
        /// </summary>
        private object DataSource
        {
            get
            {
                KeyValueCollection r = new KeyValueCollection();

                Debug.Assert(this.Parameter.ValueType.IsEnum);
                Array list = Enum.GetValues (this.Parameter .ValueType );
                foreach (object item in list)
                {
                    KeyValue kv = this.NameTexts.Find(item.ToString());
                    //Debug.Assert(kv != null);
                    if (kv != null)
                    {
                        r.Add(new KeyValue(kv.Value.ToString(), item));
                    }
                    else
                    {
                        r.Add(new KeyValue(item.ToString(), item));
                    }
                }
                return r;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        internal void ApplyNewValue()
        {
            this.Parameter.Value = this.cmbValue.SelectedValue;
        }

        private void UCComboBoxParameterUI_Load(object sender, EventArgs e)
        {
            RefreshParameter();
        }
    }
}
