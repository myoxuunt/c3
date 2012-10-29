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
        private ITask _task;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void okButton_Click(object sender, EventArgs e)
        {
            //this.DialogResult = DialogResult.OK;
            //this.Close();
            if (_task != null)
            {
                NUnit.UiKit.UserMessage.DisplayFailure("executing");
                return;
            }

            if (!VerifySendBytes())
            {
                return;
            }

            ITask task = this.CreateTask();
            task.Ended += new EventHandler(task_Ended);

            this._device.TaskManager.Tasks.Add(task);
            _task = task;
            
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void task_Ended(object sender, EventArgs e)
        {
            ITask task = sender as ITask;
            byte[] bs = task.LastReceivedBytes;
            string txt = Xdgk.Common.BytesConverter.BytesToString(bs, false);
            this.txtReceived.Text = txt;

            // clear task
            //
            this._task = null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool VerifySendBytes()
        {
            try
            {
                byte[] bs = Xdgk.Common.HexStringConverter.HexStringToBytes(this.txtSend.Text);
                if (bs.Length == 0)
                {
                    NUnit.UiKit.UserMessage.DisplayFailure("send bytes len == 0");
                    return false;
                }
            }
            catch (Exception ex)
            {
                NUnit.UiKit.UserMessage.DisplayFailure(ex.Message);
                return false;
            }
            return true;
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
        private ITask CreateTask()
        {
            TimeSpan timeout = TimeSpan.FromMilliseconds(
                this._device.Station.CommuniPortConfig.TimeoutMilliSecond);

            ITask task = new Task(
                this._device,
                this.CreateOpera(),
                Strategy.CreateImmediateStrategy(),
                timeout);

            return task;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string GetTaskName()
        {
            string name = txtName.Text.Trim();
            if (name.Length == 0)
            {
                name = "CustomTask";
            }
            return name;
        }
        /// <summary>
        /// 
        /// </summary>
        private IOpera CreateOpera()
        {
            Opera o = new Opera(this._device.DeviceType.Type.Name, this.GetTaskName());

            DataField df1 = new DataField("df1", 0, SendBytes.Length, new OriginalConverter());
            df1.Bytes = this.SendBytes;

            SendPart sendPart = new SendPart();
            sendPart.IsNeedAddress = false;
            sendPart.DataFieldManager.DataFields.Add(df1);

            o.SendPart = sendPart;
            return o;
        }

        /// <summary>
        /// 
        /// </summary>
        internal class OriginalConverter : C3.Communi.BytesConverter
        {
            /// <summary>
            /// 
            /// </summary>
            /// <param name="obj"></param>
            /// <returns></returns>
            override public byte[] ConvertToBytes(object obj)
            {
                return (byte[])obj;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="bytes"></param>
            /// <returns></returns>
            override public object ConvertToObject(byte[] bytes)
            {
                return bytes;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmCustomTask_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (this._task != null)
            {
                this._task.Ended -= new EventHandler(task_Ended);
            }
        }

    }
}
