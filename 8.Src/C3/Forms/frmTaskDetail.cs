using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using C3.Communi;

namespace C3
{
    public partial class frmTaskDetail : NUnit.UiKit.SettingsDialogBase
    {
        /// <summary>
        /// 
        /// </summary>
        public frmTaskDetail()
        {
            InitializeComponent();
        }

        #region Task
        /// <summary>
        /// 
        /// </summary>
        public ITask Task
        {
            get
            {
                return _task;
            }
            set
            {
                _task = value;
                if (_task != null)
                {
                    Fill();
                }
            }
        } private ITask _task;
        #endregion //Task

        /// <summary>
        /// 
        /// </summary>
        private void Fill()
        {
            this.txtStation.Text = this.Task.Device.Station.Name;
            this.txtDevice.Text = this.Task.Device.Text;
            this.txtTask.Text = this.Task.Opera.Text;
            this.txtLastExecute.Text = this.Task.LastExecute.ToString();
            if (this.Task.Strategy is CycleStrategy)
            {
                this.lblCycle.Visible = true;
                this.dtpTaskCycle.Visible = true;
                CycleStrategy cycStrategy = this.Task.Strategy as CycleStrategy;
                this.dtpTaskCycle.Value = DateTimePicker.MinimumDateTime + cycStrategy.Cycle;
            }
            else
            {
                this.lblCycle.Visible = false;
                this.dtpTaskCycle.Visible = false;
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void okButton_Click(object sender, EventArgs e)
        {
            if (this.Task.Strategy is CycleStrategy)
            {
                TimeSpan ts = dtpTaskCycle.Value.TimeOfDay;
                if (!CycleStrategy.CheckCycleTimeSpan(ts))
                {
                    NUnit.UiKit.UserMessage.DisplayFailure("无效的数值");
                    return;
                }
                CycleStrategy cycStrategy = (CycleStrategy)this.Task.Strategy;
                //if ( CycleStrategy.MinCycle 
                cycStrategy.Cycle = ts;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void frmTaskDetail_Load(object sender, EventArgs e)
        {

        }
    }
}
