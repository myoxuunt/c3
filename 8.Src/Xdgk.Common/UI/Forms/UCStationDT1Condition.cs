using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Xdgk.Common.UI.Forms
{
    /// <summary>
    /// 
    /// </summary>
    public partial class UCStationDT1Condition : UserControl
    {
        /// <summary>
        /// 
        /// </summary>
        public event EventHandler QueryEvent;

        /// <summary>
        /// 
        /// </summary>
        public UCStationDT1Condition()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        public string SelectedStationName
        {
            get
            {
                return this.cmbStationName.Text;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void BindStationName(KeyValueCollection kvs)
        {
            if ( kvs == null )
            {
                throw new ArgumentNullException("kvs");
            }

            this.cmbStationName.DisplayMember = "Key";
            this.cmbStationName.ValueMember = "Value";
            this.cmbStationName.DataSource = kvs;
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            if (this.QueryEvent != null)
            {
                QueryEvent(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string QueryButtonText
        {
            get { return this.btnQuery.Text; }
            set { this.btnQuery.Text = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UCStationMonthCondition_Load(object sender, EventArgs e)
        {
            this.dtpDT1.Value = DateTime.Now;
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime SelectedDT1
        {
            get { return this.dtpDT1.Value; }
            set { this.dtpDT1.Value = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTimePicker DT1DateTimePicker
        {
            get { return this.dtpDT1; }
        }

        /// <summary>
        /// 
        /// </summary>
        public Label DT1Label
        {
            get { return this.lblDT1; }
        }

        private const int ButtonSpace = 6;

        private List<Button> _extendButtonList = new List<Button>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="btn"></param>
        public void AddExtendButton(Button btn)
        {
            if (btn == null)
            {
                throw new ArgumentNullException("btn");
            }

            btn.Location = GetButtonLocation();
            _extendButtonList.Add(btn);

            this.Controls.Add(btn);
        }

        private Point GetButtonLocation()
        {
            Button referBtn = null;
            if (_extendButtonList.Count == 0)
            {
                referBtn  = this.btnQuery;
            }
            else
            {
                referBtn = _extendButtonList[_extendButtonList.Count - 1];
            }
            Point pt = referBtn.Location;
            pt.Offset(ButtonSpace + referBtn.Size.Width, 0);
            return pt;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="btn"></param>
        public void RemoveExtendButton(Button btn)
        {
            if (_extendButtonList.Contains(btn))
            {
                _extendButtonList.Remove(btn);
                this.Controls.Remove(btn);
            }
        }
    }
}
