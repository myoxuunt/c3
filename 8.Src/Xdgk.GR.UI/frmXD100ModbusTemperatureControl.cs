using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Xdgk.Common;
using Xdgk.GR.Data;
using Xdgk.GR.UI;

namespace Xdgk.GR.UI
{
    public partial class frmXD100ModbusTemperatureControl : NUnit.UiKit.SettingsDialogBase
    {

        #region Members
        /// <summary>
        /// 
        /// </summary>
        private TimeSpan _executeTimeOut = TimeSpan.FromSeconds(10);

        /// <summary>
        /// 
        /// </summary>
        private IExecuteController _controller;

        /// <summary>
        /// 
        /// </summary>
        private DateTime _lastExecuteDateTime;

        /// <summary>
        /// 
        /// </summary>
        private bool _isExecuting;

        /// <summary>
        /// 
        /// </summary>
        private bool _canWrite = false;


        #endregion //


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool IsExecuting()
        {
            TimeSpan ts = DateTime.Now - _lastExecuteDateTime;
            bool b = ts >= _executeTimeOut;

            return _isExecuting && (!b);
        }

        #region DeviceID
        /// <summary>
        /// 
        /// </summary>
        public int DeviceID
        {
            get
            {
                return _deviceID;
            }
        } private int _deviceID;
        #endregion //DeviceID


        #region frmXD100TemperatureControl
        /// <summary>
        /// 
        /// </summary>
        /// <param name="deviceID"></param>
        /// <param name="controller"></param>
        public frmXD100ModbusTemperatureControl(int deviceID, IExecuteController controller)
        {
            InitializeComponent();

            BindDatas();

            this.ucotControlLine1.Size = this.ucTimeControlLine21.Size;
            this.ucotControlLine1.Location = this.ucTimeControlLine21.Location;

            this.ucValveOpenDegree1.Size = this.ucTimeControlLine21.Size;
            this.ucValveOpenDegree1.Location = this.ucTimeControlLine21.Location;

            this._deviceID = deviceID;
            this._controller = controller;

            this._controller.ResultEvent += new EventHandler(_controller_ResultEvent);
        }
        #endregion //frmXD100TemperatureControl

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _controller_ResultEvent(object sender, EventArgs e)
        {
            ResultArgs args = this._controller.ResultArgs;

            if (args.IsSuccess)
            {
                this.Sync.Post(new SendOrPostCallback(StatusBarTarget), args.Message);
            }
            else
            {
                this.Sync.Post(new SendOrPostCallback(MessageBoxTarget), args.Message);
            }

            if (args.IsSuccess)
            {
                if (args.IsComplete)
                {
                    if (StringHelper.Equal(args.ExecuteArgs.ExecuteName, XD1100OperaNames.OPERA_READ))
                    {
                        this.Sync.Post(new SendOrPostCallback(ReadLineTarget), args.KeyValues);
                        this._canWrite = true;
                    }
                    else if (StringHelper.Equal(args.ExecuteArgs.ExecuteName, XD1100OperaNames.OPERA_WRITE))
                    {
                        this.Sync.Post(new SendOrPostCallback(MessageBoxTarget), 
                            XD100Strings.WriteControlModeSuccess);
                    }
                }
            }

            this._isExecuting = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        private void ReadLineTarget(object status)
        {
            ProcessReadLine((KeyValueCollection)status);
        }

        private void StatusBarTarget(object status)
        {
            this.SetStatusText(status.ToString());
        }

        private void MessageBoxTarget(object status)
        {
            NUnit.UiKit.UserMessage.DisplayInfo(status.ToString());
        }

        #region BindDatas
        /// <summary>
        /// 
        /// </summary>
        private void BindDatas()
        {
            this.cmbControlMode.DataSource = Xdgk.XD100Modbus.XD100ModbusDefines.TemperatureControlModeCollection;
            this.cmbControlMode.DisplayMember = "Name";
            this.cmbControlMode.ValueMember = "Value";
            //this.cmbControlMode.SelectedItem 

            this.cmbValveType.DataSource = Xdgk.GR.Data.XD100Defines.ValveTypeCollection;
            //Xdgk.XD100.XD100Defines.ValveTypeCollection;
            this.cmbValveType.DisplayMember = "Name";
            this.cmbValveType.ValueMember = "Value";
        }
        #endregion //BindDatas

        #region cancelButton_Click
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion //cancelButton_Click


        #region UC
        ///// <summary>
        ///// 
        ///// </summary>
        //public UCTimeControlLine UC
        //{
        //    // tt
        //    //get { return ucTimeControlLine1; }
        //    get { return null; }
        //}
        #endregion //UC

        #region TimeControlLine2
        /// <summary>
        /// 
        /// </summary>
        public UCTimeControlLine2 TimeControlLine2
        {
            get
            {
                return this.ucTimeControlLine21;
            }
        }
        #endregion //TimeControlLine2

        #region frmXD100TemperatureControl_Load
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmXD100TemperatureControl_Load(object sender, EventArgs e)
        {
        }
        #endregion //frmXD100TemperatureControl_Load


        #region okButton_Click
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void okButton_Click(object sender, EventArgs e)
        {
            return;
        }
        #endregion //okButton_Click


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool IsReady()
        {
            ExecuteArgs args = new ExecuteArgs();
            args.DeviceID = this.DeviceID;
            args.ExecuteName = DefineExecuteNames.IsReady;

            ExecuteResult r = this._controller.Doit(args);
            if (!r.IsSuccess)
            {
                NUnit.UiKit.UserMessage.DisplayFailure(r.FailMessage);
            }
            return r.IsSuccess;
        }

        #region btnRead_Click
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRead_Click(object sender, EventArgs e)
        {
            if (!IsReady())
            {
                return;
            }

            if (!IsExecuting())
            {
                ExecuteArgs args = new ExecuteArgs();
                args.DeviceID = this.DeviceID;
                args.ExecuteName = XD1100OperaNames.OPERA_READ;

                ExecuteResult r = this._controller.Doit(args);
                if (r.IsSuccess)
                {
                    this._isExecuting = true;
                    this._lastExecuteDateTime = DateTime.Now;

                    this.SetStatusText(string.Format(XD100Strings.ExecutingOpera, args.ExecuteName));
                }
                else
                {
                    NUnit.UiKit.UserMessage.DisplayFailure(r.FailMessage);
                }
            }
            else
            {
                NUnit.UiKit.UserMessage.DisplayFailure(XD100Strings.Executing);
            }
        }
        #endregion //btnRead_Click


        private void SetStatusText(string text )
        {
            this.statusBarPanel1.Text = DateTime.Now.ToString() + " " + text;
        }

        #region ProcessReadLine
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        private void ProcessReadLine(KeyValueCollection keyValues)
        {
            object obj = null;

            obj = keyValues["ControlMode"];
            int ctrlMode = Convert.ToInt16(obj);
            this.cmbControlMode.SelectedValue = ctrlMode;

            obj = keyValues["ValveType"];
            int valveType = Convert.ToInt32(obj);
            this.cmbValveType.SelectedValue = valveType;

            obj = keyValues["SettingValue"];
            int settingValue = Convert.ToInt32(obj);
            settingValue = (int)(settingValue / 10);
            this.ucTimeControlLine21.GTBase2 = settingValue;

            this.ucValveOpenDegree1.ValveOpenDegree = settingValue;
            

            obj = keyValues["TimeControlLine"];
            int[] adjustValues = obj as int[];

            KeyValuePair<int, int>[] timeControlLine = CreateTimeControlLineByAdjustValues(adjustValues);
            this.TimeControlLine2.TimeControlLine = timeControlLine;

            // ot-gt2 line
            //
            obj = keyValues["OTControlLine"];
            KeyValuePair<int, int>[] otControlLine = (KeyValuePair<int, int>[])obj;
            this.ucotControlLine1.OTControlLine = otControlLine;
        }
        #endregion //ProcessReadLine


        #region KeyValuePair
        /// <summary>
        /// 
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        private KeyValuePair<int, int>[] CreateTimeControlLineByAdjustValues(int[] values)
        {
            KeyValuePair<int, int>[] r = new KeyValuePair<int, int>[values.Length];
            for (int i = 0; i < values.Length; i++)
            {
                r[i] = new KeyValuePair<int, int>(i * 2, values[i]);
            }
            return r;
        }
        #endregion //KeyValuePair

        #region CreateAdjustValuesByTimeControlLine
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private int[] CreateAdjustValuesByTimeControlLine(KeyValuePair<int, int>[] timeControlLine)
        {
            int[] r = new int[timeControlLine.Length];
            for (int i = 0; i < timeControlLine.Length; i++)
            {
                r[i] = timeControlLine[i].Value;
            }
            return r;
        }
        #endregion //CreateAdjustValuesByTimeControlLine


        #region GetSettingValue
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private int GetSettingValue()
        {
            int value = 0;
            Xdgk.XD100Modbus.TemperatureControlMode mode = this.cmbControlMode.SelectedItem as Xdgk.XD100Modbus.TemperatureControlMode;
            if (mode.Mode == Xdgk.XD100Modbus.TemperatureControlModeEnum.ValveOpenDegree)
            {
                value = this.ucValveOpenDegree1.ValveOpenDegree;
            }
            //else if (mode.Mode == Xdgk.XD100Modbus.TemperatureControlModeEnum.SettingAndBT2 ||
            //    mode.Mode == Xdgk.XD100Modbus.TemperatureControlModeEnum.SettingAndDiffT2 ||
            //    mode.Mode == Xdgk.XD100Modbus.TemperatureControlModeEnum.SettingAndGT2)
            else
            {
                value = (int)ucTimeControlLine21.GTBase2;
            }

            value *= 10;
            return value;
        }
        #endregion //GetSettingValue

        #region ProcessWriteMode
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="e"></param>
        //private void ProcessWriteMode(TaskExecutedEventArgs e)
        //{
        //    if (e.ParseResult.Success)
        //    {
        //        // tt
        //        //KeyValuePair<int, int>[] tcLine = this.ucTimeControlLine1.TimeControlLine;
        //        //int[] adjustValues = CreateAdjustValuesByTimeControlLine(tcLine);
        //        //WriteLine(adjustValues);
        //        //this.SetState(State.WriteLine);

        //        //KeyValuePair<int, int>[] tcLine = this.TimeControlLine2.TimeControlLine;
        //        //int[] adjustValues = CreateAdjustValuesByTimeControlLine(tcLine);
        //        //WriteLine(adjustValues);
        //        //this.SetState(State.WriteLine);

        //    }
        //    else
        //    {
        //        ShowError();
        //    }
        //    this.SetState(State.None);
        //}
        #endregion //ProcessWriteMode

        #region WriteLine
        /// <summary>
        /// 
        /// </summary>
        //private void WriteLine(KeyValuePair<int, int>[] timeControlLine)
        private void WriteLine(int[] adjustValues)
        {
            //Opera op = CreateOpera("WriteTempControlData");
            //op.SendPart["TimeControlLine"] = adjustValues;

            //// TODO: 2010-08-31 ot control line
            ////
            //op.SendPart["OTControlLine"] = this.ucotControlLine1.OTControlLine;
            //_task = new Task(this.Device, op, new ImmediateStrategy());
            //CZGRApp.Default.Soft.TaskManager.Tasks.AddToHead(_task);

            ExecuteArgs args = new ExecuteArgs();
            args.DeviceID = this.DeviceID ;
            args.ExecuteName = "WriteTempControlData";
            args.KeyValues["TimeControlLine"] = adjustValues;
            args.KeyValues["OTControlLine"] = this.ucotControlLine1.OTControlLine;

            _controller.Doit(args);
        }
        #endregion //WriteLine


        //#region ProcessWriteLine
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="e"></param>
        //private void ProcessWriteLine(TaskExecutedEventArgs e)
        //{
        //    //throw new NotImplementedException();
        //    if (e.ParseResult.Success)
        //    {
        //    }
        //    else
        //    {
        //        ShowError();
        //    }
        //    SetState(State.None);
        //}
        //#endregion //ProcessWriteLine


        #region btnWrite_Click
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnWrite_Click(object sender, EventArgs e)
        {
            if (!IsReady())
            {
                return;
            }

            if (!this._canWrite)
            {
                NUnit.UiKit.UserMessage.DisplayFailure(XD100Strings.FirstReadGRControlParams);
                return;
            }

            if (this.IsExecuting())
            {
                NUnit.UiKit.UserMessage.DisplayFailure(XD100Strings.Executing);
                return;
            }

            if (this.cmbControlMode.SelectedItem != null)
            {
                Xdgk.XD100Modbus.TemperatureControlMode mode = this.cmbControlMode.SelectedItem as Xdgk.XD100Modbus.TemperatureControlMode;
                if (mode != null)
                {
                    if (mode.Mode == Xdgk.XD100Modbus.TemperatureControlModeEnum.ValveOpenDegree)
                    {
                        //string s = string.Format(XD100ModbusStrings.NotSupportMode, mode.Name);
                        string s = string.Format(XD100Strings.NotSupportMode, mode.Name);
                        NUnit.UiKit.UserMessage.DisplayFailure(s);
                        return;
                    }
                }
            }

            ExecuteArgs args = new ExecuteArgs();
            KeyValueCollection hash = args.KeyValues;
            hash["ControlMode"] = (int)this.cmbControlMode.SelectedValue;
            hash["ValveType"] = (int)this.cmbValveType.SelectedValue;

            hash["SettingValue"] = GetSettingValue();
            hash["OTControlLine"] = this.ucotControlLine1.OTControlLine;

            KeyValuePair<int, int>[] tcLine = this.TimeControlLine2.TimeControlLine;
            int[] adjustValues = CreateAdjustValuesByTimeControlLine(tcLine);
            hash["TimeControlLine"] = adjustValues;

            args.DeviceID = this.DeviceID;
            args.ExecuteName = XD1100OperaNames.OPERA_WRITE;
            ExecuteResult r = _controller.Doit(args);

            if (r.IsSuccess)
            {
                this._isExecuting = true;
                this._lastExecuteDateTime = DateTime.Now;

                this.SetStatusText(string.Format(XD100Strings.ExecutingOpera, args.ExecuteName));
            }
            else
            {
                NUnit.UiKit.UserMessage.DisplayFailure(r.FailMessage);
            }

        }
        #endregion //btnWrite_Click

        #region frmXD100TemperatureControl_FormClosing
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmXD100TemperatureControl_FormClosing(object sender, FormClosingEventArgs e)
        {
        }
        #endregion //frmXD100TemperatureControl_FormClosing

        #region frmXD100TemperatureControl_FormClosed
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmXD100TemperatureControl_FormClosed(object sender, FormClosedEventArgs e)
        {
            this._controller.ResultEvent -= new EventHandler(_controller_ResultEvent);
            this._controller.Dispose();
        }
        #endregion //frmXD100TemperatureControl_FormClosed

        #region cmbControlMode_SelectedIndexChanged
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbControlMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            // TODO: local and resize uc
            //
            object obj = this.cmbControlMode.SelectedItem;
            Xdgk.XD100Modbus.TemperatureControlMode mode = obj as Xdgk.XD100Modbus.TemperatureControlMode;

            //if (mode.Mode == Xdgk.XD100.TemperatureControlModeEnum.OT_GT2)
            if (mode.Mode == Xdgk.XD100Modbus.TemperatureControlModeEnum.LineAndBT2 ||
                mode.Mode == Xdgk.XD100Modbus.TemperatureControlModeEnum.LineAndGT2 ||
                mode.Mode == Xdgk.XD100Modbus.TemperatureControlModeEnum.LineAndDiffT2)
            {
                //this.ucotControlLine1.Visible = true;
                //this.ucTimeControlLine21.Visible = false;
                ShowUCControl(this.ucotControlLine1);
            }
            //else if (mode.Mode == Xdgk.XD100.TemperatureControlModeEnum.Time_GT2)
            else if (mode.Mode == Xdgk.XD100Modbus.TemperatureControlModeEnum.SettingAndBT2 ||
                mode.Mode == Xdgk.XD100Modbus.TemperatureControlModeEnum.SettingAndDiffT2 ||
                mode.Mode == Xdgk.XD100Modbus.TemperatureControlModeEnum.SettingAndGT2)
            {
                //this.ucotControlLine1.Visible = false;
                //this.ucTimeControlLine21.Visible = true;
                ShowUCControl(this.ucTimeControlLine21);
            }
            else if (mode.Mode == Xdgk.XD100Modbus.TemperatureControlModeEnum.ValveOpenDegree)
            {
                //this.ucTimeControlLine21.Visible = false;
                //this.ucotControlLine1.Visible = false;
                ShowUCControl(ucValveOpenDegree1);
            }
            else
            {
                ShowUCControl(null);
            }
        }
        #endregion //cmbControlMode_SelectedIndexChanged

        #region ShowUCControl
        /// <summary>
        /// 
        /// </summary>
        /// <param name="control"></param>
        private void ShowUCControl(Control control)
        {
            foreach (Control c in UCControlList)
            {
                c.Visible = (control == c);
            }
        }
        #endregion //ShowUCControl

        #region UCControlList
        /// <summary>
        /// 获取用户空间列表, 每个用户控件对应一类控制方式
        /// </summary>
        private Control[] UCControlList
        {
            get
            {
                if (_ucControlList == null)
                {
                    _ucControlList = new Control[]
                    {
                        this.ucotControlLine1 ,
                        this.ucTimeControlLine21 ,
                        this.ucValveOpenDegree1
                    };
                }
                return _ucControlList;
            }
        } private Control[] _ucControlList;
        #endregion //UCControlList

        /// <summary>
        /// 
        /// </summary>
        private System.Threading.SynchronizationContext Sync
        {
            get { return System.Windows.Forms.WindowsFormsSynchronizationContext.Current; }
        }

    }
}


