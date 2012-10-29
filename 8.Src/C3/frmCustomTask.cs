using System;
using System.Windows.Forms;
using Xdgk.Common;
using C3.Communi;

namespace C3
{
    public partial class frmCustomTask : NUnit.UiKit.SettingsDialogBase
    {
        /// <summary>
        /// 
        /// </summary>
        public frmCustomTask()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            //this.DialogResult = DialogResult.OK;
            this._device.TaskManager.Tasks.Add(this.Task);
            this.Close();
        }

        /// <summary>
        /// 
        /// </summary>
        public byte[] SendBytes
        {
            get
            {
                byte[] bs = Xdgk.Common.HexStringConverter.HexStringToBytes(this.txtSend.Text);
                return bs;
            }
            set
            {
                string txt = HexStringConverter.BytesToHexString(value);
                this.txtSend.Text = txt;
            }
        }

        #region Device
        /// <summary>
        /// 
        /// </summary>
        public IDevice Device
        {
            get
            {
                if (_device == null)
                {
                }
                return _device;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Device");
                }
                _device = value;
            }
        } private IDevice _device;
        #endregion //Device

        /// <summary>
        /// 
        /// </summary>
        ITask Task
        {
            get
            {
                TimeSpan timeout  = TimeSpan .FromMilliseconds ( 
                    this._device.Station.CommuniPortConfig.TimeoutMilliSecond );

                ITask task = new Task(
                    this._device,
                    this.Op,
                    Strategy.CreateImmediateStrategy(),
                   timeout);
                return task;
            }
        }

        IOpera Op
        {
            get
            {
                Opera o = new Opera(this._device.DeviceType.Type.Name, "noname");
                DataField df1 = new DataField("df1", 0, SendBytes.Length, null);
                o.SendPart.DataFieldManager.DataFields.Add(df1);
                return o;
            }
        }
    }
}
