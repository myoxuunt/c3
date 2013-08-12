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

            if (!IsDeviceConnected())
            {
                NUnit.UiKit.UserMessage.DisplayFailure(Strings.StationNotConnected);
                return;
            }

            if (_task != null)
            {
                NUnit.UiKit.UserMessage.DisplayFailure("正在执行");
                return;
            }

            if (!VerifySendBytes())
            {
                return;
            }

            this.txtReceived.Clear();

            ITask task = this.CreateTask();
            task.Ended += new EventHandler(task_Ended);

            this._device.TaskManager.Tasks.Add(task);
            _task = task;

            string status = string.Format("执行 '{0}' ...", task.Opera.Name);
            SetStatusText(status);
            
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        private void SetStatusText(string text)
        {
            this.tssStatus.Text = string.Format("{0} {1}", DateTime.Now, text);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool IsDeviceConnected()
        {
            bool r = false;
            ICommuniPort cp = this._device.Station.CommuniPort;
            if (cp != null )
            {
                r = cp.IsOpened;
            }
            return r;
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

            string status = string.Format("执行结束");
            SetStatusText(status);
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
                    NUnit.UiKit.UserMessage.DisplayFailure("发送数据无效");
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
            ITask task = new Task(
                this._device,
                this.CreateOpera(),
                Strategy.CreateImmediateStrategy(),
                 1);

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
