using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Xdgk.Common;

namespace Xdgk.UI.Forms
{
    public partial class UCCondition : UserControl
    {
        /// <summary>
        /// 
        /// </summary>
        public event EventHandler QueryEvent;

        /// <summary>
        /// 
        /// </summary>
        public UCCondition()
        {
            InitializeComponent();

            this.Begin = DateTime.Now.Date;
            this.End = DateTime.Now.Date + TimeSpan.FromDays(1d);
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

        ///// <summary>
        ///// 
        ///// </summary>
        //public int SelectedStationID
        //{
        //    get
        //    {
        //        return (int)this.cmbStationName.SelectedValue;
        //    }
        //}
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

        /// <summary>
        /// 
        /// </summary>
        public DateTime Begin
        {
            get
            {
                return this.dtpBeginDate.Value.Date + this.dtpBeginTime.Value.TimeOfDay;
            }
            set {
                this.dtpBeginDate.Value = value;
                this.dtpBeginTime.Value = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime End
        {
            get
            {
                return this.dtpEndDate.Value.Date + this.dtpEndTime.Value.TimeOfDay;
            }
            set
            {
                this.dtpEndDate.Value = value;
                this.dtpEndTime.Value = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuery_Click(object sender, EventArgs e)
        {
            if (this.QueryEvent != null)
            {
                QueryEvent(this, EventArgs.Empty);
            }
        }
    }
}
