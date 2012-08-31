using System;
using System.Reflection;
using System.Windows.Forms;
using Xdgk.Common;

namespace C3.Communi
{
    public partial class UCEnumParameterUI : UserControl
    {
        public UCEnumParameterUI()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        public Type ValueType
        {
            get { return _valueType; }
            set
            {
                _valueType = value;

                KeyValueCollection kvs = new KeyValueCollection();
                Array array = Enum.GetValues(_valueType);
                foreach (object val in array)
                {
                    FieldInfo fi = _valueType.GetField (val.ToString ());
                    object[] atts = fi.GetCustomAttributes(typeof(EnumTextAttribute), false);
                    if (atts.Length > 0)
                    {
                        EnumTextAttribute ta = (EnumTextAttribute)atts[0];
                        KeyValue kv = new KeyValue(ta.Text, val);
                        kvs.Add(kv);
                    }
                    else
                    {
                        KeyValue kv = new KeyValue(val.ToString(), val);
                        kvs.Add(kv);
                    }
                }

                this.cmbValue.DisplayMember = "Key";
                this.cmbValue.ValueMember = "Value";
                this.cmbValue.DataSource = kvs;
            }
        } private Type _valueType;

        /// <summary>
        /// 
        /// </summary>
        public string ParameterName
        {
            get { return this.lblName.Text; }
            set { this.lblName.Text = value; }
        }

        public object EnumValue
        {
            get { return this.cmbValue.SelectedValue; }
            set { this.cmbValue.SelectedValue = value; }
        }

        public string Unit
        {
            get { return this.lblUnit.Text; }
            set { this.lblUnit.Text = value; }
        }
    }
}
