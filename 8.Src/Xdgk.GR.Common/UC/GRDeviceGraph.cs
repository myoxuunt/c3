using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using CZGRDBI;
using Xdgk.Communi;

namespace CZGR
{
    public partial class GRDeviceGraph : UserControl
    {

        Label[] _valuelabels;
        private Label[] _textlabels;


        #region GRDeviceGraph
        public GRDeviceGraph()
        {
            InitializeComponent();
            this.lblWLText.Visible = false;
            this.lblWLValue.Visible = false;
            _valuelabels = new Label[]
            {
                this.lblGT1Value, this.lblBT1Value, this.lblGP1Value, this.lblBP1Value ,
                this.lblGT2Value, this.lblBT2Value, this.lblGP2Value, this.lblBP2Value
                //,
                //this.lblCMValue, this.lblRMValue
            };  

            _units = new string[]
            {
                "℃", "℃","MPa","MPa",
                "℃", "℃","MPa","MPa"
            };
            
            _textlabels = new Label[]
            {
                this.lblGT1Text, this.lblBT1Text, this.lblGP1Text, this.lblBP1Text ,
                this.lblGT2Text, this.lblBT2Text, this.lblGP2Text, this.lblBP2Text ,
                this.lblCMText, this.lblRMText
            };

            SetTextLabelVisible(false);
            SetLableText();
            SetValueLabelInfo();
        }
        #endregion //GRDeviceGraph

        #region SetValueLabelInfo
        /// <summary>
        /// 
        /// </summary>
        private void SetValueLabelInfo()
        {
            foreach (Label lbl in _valuelabels)
            {
                lbl.ForeColor = Config.GRDeviceGraphConfig.ValueForeColor;
                lbl.BackColor = Config.GRDeviceGraphConfig.ValueBackColor;
                lbl.Font = Config.GRDeviceGraphConfig.ValueFont;
            }
            this.lblCMValue.ForeColor =Config.GRDeviceGraphConfig.ValueForeColor;
            this.lblCMValue.BackColor = Config.GRDeviceGraphConfig.ValueBackColor;
            this.lblCMValue.Font = Config.GRDeviceGraphConfig.ValueFont;

            this.lblRMValue.ForeColor =Config.GRDeviceGraphConfig.ValueForeColor;
            this.lblRMValue.BackColor = Config.GRDeviceGraphConfig.ValueBackColor;
            this.lblRMValue.Font = Config.GRDeviceGraphConfig.ValueFont;
        }
        #endregion //SetValueLabelInfo

        #region SetTextLabelVisible
        private string[] _units;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="v"></param>
        private void SetTextLabelVisible(bool v)
        {
            for (int i = 0; i < _textlabels.Length - 2; i++)
            {
                _textlabels[i].Visible = v;
            }
        }
        #endregion //SetTextLabelVisible

        #region SetLableText
        /// <summary>
        /// 
        /// </summary>
        private void SetLableText()
        {
            string[] texts = new string[]
            {
                strings.GT1,strings.BT1,strings.GP1,strings.BP1,
                strings.GT2,strings.BT2,strings.GP2,strings.BP2,
                strings.CM, strings.RM
            };

            for (int i = 0; i < _textlabels.Length; i++)
            {
                Label lbl = _textlabels[i];
                lbl.Text = texts[i];
            }
        }
        #endregion //SetLableText

        /// <summary>
        /// 
        /// </summary>

        #region UpdateGRDeviceData
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbgrdatalast"></param>
        internal void UpdateGRDeviceData(GRDevice grdevice, CZGRDBI.DBGRDataBase dbgrdata)
        {
            this.lblStationName.Text = DeviceNameDisplayer.GetDeviceDisplayName(
                grdevice, Config.DeviceNameDisplayMode);

            this.lblDateTime.Text = dbgrdata.DT.ToString();


            float[] texts = new float[]
            {
                dbgrdata.GT1,dbgrdata.BT1,dbgrdata.GP1,dbgrdata.BP1,
                dbgrdata.GT2,dbgrdata.BT2,dbgrdata.GP2,dbgrdata.BP2
                //,
                //dbgrdatalast.CM, dbgrdatalast.RM
            };

            string formatstring = "f" + Config.Digits;
            for (int i = 0; i < _valuelabels.Length; i++)
            {
                Label lbl = _valuelabels[i];
                lbl.Text = texts[i].ToString(formatstring) + " " + _units[i];
            }

            this.lblCMValue.Text = string.Format(strings.CyclePump, GetCMValue(dbgrdata));
            this.lblRMValue.Text = string.Format(strings.RecruitePump, GetRMValue(dbgrdata));
            SetValuesVisible(true);
        }
        #endregion //UpdateGRDeviceData

        #region GetRMValue
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbgrdata"></param>
        /// <returns></returns>
        private string GetRMValue(CZGRDBI.DBGRDataBase dbgrdata)
        {
            bool b = dbgrdata.CM1 || dbgrdata.CM2;
            return b ? strings.Run : strings.Stop;
        }
        #endregion //GetRMValue

        #region GetCMValue
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbgrdata"></param>
        /// <returns></returns>
        private string GetCMValue(CZGRDBI.DBGRDataBase dbgrdata)
        {
            bool b = dbgrdata.RM1 || dbgrdata.RM2;
            return b ? strings.Run : strings.Stop;
        }
        #endregion //GetCMValue

        #region NotHasGRData
        /// <summary>
        /// 
        /// </summary>
        /// <param name="grdevicename"></param>
        internal void NotHasGRData(string grdevicename)
        {
            this.lblStationName.Text = grdevicename;
            this.lblDateTime.Text = "没有最新数据";
            SetValuesVisible(false);
        }
        #endregion //NotHasGRData

        #region SetValuesVisible
        /// <summary>
        /// 
        /// </summary>
        /// <param name="v"></param>
        private void SetValuesVisible( bool v )
        {
            foreach (Label lbl in _valuelabels)
            {
                lbl.Visible = v;
            }
            this.lblCMValue.Visible = v;
            this.lblRMValue.Visible = v;
        }
        #endregion //SetValuesVisible

        #region GRDevice
        /// <summary>
        /// 
        /// </summary>
        public GRDevice GRDevice
        {
            get { return _grdevice; }
            set
            {
                if (_grdevice != value)
                {
                    if (_grdevice != null)
                    {
                        UnregisterEvents(_grdevice);
                    }
                    _grdevice = value;
                    if (_grdevice != null)
                    {
                        RegisterEvents(_grdevice);
                    }
                    this.SelectedTreeViewGRDeivce(this._grdevice);
                }
            }
        } private GRDevice _grdevice;
        #endregion //GRDevice

        #region RegisterEvents
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_grdevice"></param>
        private void RegisterEvents(GRDevice grdevice)
        {
            grdevice.DBGRDataLastChanged += new EventHandler(grdevice_DBGRDataLastChanged);
        }
        #endregion //RegisterEvents

        #region UnregisterEvents
        /// <summary>
        /// 
        /// </summary>
        /// <param name="grdevice"></param>
        private void UnregisterEvents(GRDevice grdevice)
        {
            grdevice.DBGRDataLastChanged -= new EventHandler(grdevice_DBGRDataLastChanged);
        }
        #endregion //UnregisterEvents

        #region grdevice_DBGRDataLastChanged
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdevice_DBGRDataLastChanged(object sender, EventArgs e)
        {
            GRDevice grdevice = sender as GRDevice;

            if (grdevice == null)
                return;

            DBGRDataBase dbgrdatalast = grdevice.DBGRDataLast;
            if (dbgrdatalast != null)
            {
                // TODO: updata graphic grdevice
                //
                this.UpdateGRDeviceData(grdevice, dbgrdatalast);
            }
            else
            {
                string grdevicename = DeviceNameDisplayer.GetDeviceDisplayName(
                    grdevice,
                    Config.DeviceNameDisplayMode);
                this.NotHasGRData(grdevicename);
            }
        }
        #endregion //grdevice_DBGRDataLastChanged

        #region SelectedTreeViewGRDeivce
        /// <summary>
        /// 
        /// </summary>
        /// <param name="grdeviceNode"></param>
        /// <param name="grdevice"></param>
        private void SelectedTreeViewGRDeivce(GRDevice grdevice)
        {
            if (grdevice == null)
                throw new ArgumentNullException("grdevice");
            this.GRDevice = grdevice;
            DBGRDataBase dbgrdatalast = grdevice.DBGRDataLast;
            if (dbgrdatalast != null)
            {
                // TODO: updata graphic grdevice
                //
                this.UpdateGRDeviceData(grdevice, dbgrdatalast);
            }
            else
            {
                string grdevicename = DeviceNameDisplayer.GetDeviceDisplayName(
                    grdevice,
                    Config.DeviceNameDisplayMode);
                this.NotHasGRData(grdevicename);
            }
        }
        #endregion //SelectedTreeViewGRDeivce

        #region GRDeviceGraph_Load
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GRDeviceGraph_Load(object sender, EventArgs e)
        {
            this.lblMark.Text = ""; 
        }
        #endregion //GRDeviceGraph_Load

        #region btnReadRealData_Click
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReadRealData_Click(object sender, EventArgs e)
        {
            if (this._grdevice != null)
            {
                if (!CheckStationConnected(_grdevice.Station))
                {
                    return;
                }

                CommuniSoft soft = CZGRApp.Default.Soft; 
                //Opera op = soft.OperaFactory.Create(
                //    CZGRCommon.DeviceTypes.GRDevice,
                //    GROperaNames.ReadReal);
                Opera op = GRDevice.DeviceDefine.CreateOpera(GROperaNames.ReadReal);
                //Task task = new Task(grdevice, op, new ImmediateStrategy());
                //soft.TaskManager.Tasks.AddToHead(task);
                this.AddTask(soft, _grdevice, op);
            }
        }
        #endregion //btnReadRealData_Click

        #region AddTask
        /// <summary>
        /// 
        /// </summary>
        /// <param name="soft"></param>
        /// <param name="task"></param>
        private void AddTask(CommuniSoft soft, Device device, Opera op)
        {
            Task task = new Task(device, op, new ImmediateStrategy());
            soft.TaskManager.Tasks.AddToHead(task);
            NUnit.UiKit.UserMessage.DisplayInfo(strings.CommandSubmit);
        }
        #endregion //AddTask

        #region CheckStationConnected
        /// <summary>
        /// 
        /// </summary>
        /// <param name="station"></param>
        /// <returns></returns>
        private bool CheckStationConnected(Station station)
        {
            if (station.CommuniPort == null)
            {
                string s = string.Format(strings.StationNotConnect, station.Name);
                NUnit.UiKit.UserMessage.DisplayFailure(s);
                return false;
            }
            return true;
        }
        #endregion //CheckStationConnected

        #region btnGRData_Click
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGRData_Click(object sender, EventArgs e)
        {
            if (CheckGRDevice())
            {
                CZGRQRCStarter.Start(this._grdevice,
                    CZGRCommon.DataValue.GR, CZGRCommon.AppearanceValue.Query);
            }
        }
        #endregion //btnGRData_Click

        #region btnGRAlarmData_Click
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGRAlarmData_Click(object sender, EventArgs e)
        {
            if (CheckGRDevice())
            {
                CZGRQRCStarter.Start(this._grdevice,
                    CZGRCommon.DataValue.GRAlarm, CZGRCommon.AppearanceValue.Curve);
            }
        }
        #endregion //btnGRAlarmData_Click

        #region btnTempCurve_Click
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTempCurve_Click(object sender, EventArgs e)
        {
            if (CheckGRDevice())
            {
                CZGRQRCStarter.Start(this._grdevice,
                    CZGRCommon.DataValue.GRTemp, CZGRCommon.AppearanceValue.Curve);
            }
        }
        #endregion //btnTempCurve_Click

        #region CheckGRDevice
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool CheckGRDevice()
        {
            if (this._grdevice == null)
                return false;
            else
                return true;
        }
        #endregion //CheckGRDevice

        #region btnPressCurve_Click
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPressCurve_Click(object sender, EventArgs e)
        {
            if (CheckGRDevice())
            {
                CZGRQRCStarter.Start(this._grdevice,
                    CZGRCommon.DataValue.GRPress,
                    CZGRCommon.AppearanceValue.Curve);
            }
        }
        #endregion //btnPressCurve_Click


    }
}
