﻿using System;
using System.Text;
using System.Collections;
using Xdgk.Common;
using C3.Communi;

namespace Xdgk.GR.Common
{

    public interface IOutsideTemperatureProvider
    {
        float GetStandardOutsideTemperature(IDevice device);
    }
    
    public class FixedOTProvider : IOutsideTemperatureProvider 
    {
        public float Value
        {
            get { return _value; }
            set { _value = value; }
        } private float _value;

        #region IOutsideTemperatureProvider 成员

        /// <summary>
        /// 
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        public float GetStandardOutsideTemperature(IDevice device)
        {
            return Value;
        }

        #endregion
    }
    public class DeviceOTProvider : IOutsideTemperatureProvider
    {

        public DeviceOTProvider (IOutside outside)
        {
            this.Outside = outside;
        }

        /// <summary>
        /// 
        /// </summary>
        public IOutside Outside
        {
            get { return _outside; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Outside");
                }
                _outside = value;
            }
        } private IOutside _outside;

        #region IOutsideTemperatureProvider 成员
        /// <summary>
        /// 
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        public float GetStandardOutsideTemperature(IDevice device)
        {
            return this.Outside.OutsideTemperature;
        }
        #endregion
    }

    public class OutsideTemperatureProviderManager
    {
        private OutsideTemperatureProviderManager()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        static public IOutsideTemperatureProvider Provider
        {
            get
            {
                if (_p == null)
                {
                    _p = new FixedOTProvider();
                }
                return _p; 
            }
            set
            {
                _p = value;
            }
        } static private IOutsideTemperatureProvider _p;

        #region IOutsideTemperatureProvider 成员
        /// <summary>
        /// 
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        static public float GetStandardOutsideTemperature(IDevice device)
        {
            return Provider.GetStandardOutsideTemperature(device);
        }

        #endregion
    }

    public enum ModeValue
    {
        /// <summary>
        /// 
        /// </summary>
        [EnumText("直供")]
        Direct,

        /// <summary>
        /// 
        /// </summary>
        [EnumText("间供")]
        Indirect,

        /// <summary>
        /// 
        /// </summary>
        [EnumText("混水")]
        Mixed,
    }

    abstract public class HeatTransferMode
    {
        static private HeatTransferMode
            _direct = new HtmDirect(),
                    _indirect = new HtmIndirect(),
                    _mixed = new HtmMixed();

        /// <summary>
        /// 
        /// </summary>
        static private HeatTransferMode[] a = new HeatTransferMode[]
        {
            _direct, _indirect , _mixed 
        };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        static public HeatTransferMode Parse(int value)
        {
            ModeValue mv = (ModeValue)value;
            return Parse(mv);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modeValue"></param>
        /// <returns></returns>
        static public HeatTransferMode Parse(ModeValue modeValue)
        {
            HeatTransferMode r = null;
            foreach (HeatTransferMode item in a)
            {
                if (modeValue == item.ModeValue)
                {
                    r = item;
                }
            }
            return r;
        }

        /// <summary>
        /// 
        /// </summary>
        abstract public ModeValue ModeValue{get;}


    }
    internal class HtmDirect : HeatTransferMode 
    {
        public override ModeValue ModeValue
        {
            get { return ModeValue.Direct; }
        }

    }
    internal class HtmIndirect: HeatTransferMode
    {
        public override ModeValue ModeValue
        {
            get { return ModeValue.Indirect ; }
        }
    }
    internal class HtmMixed: HeatTransferMode
    {
        public override ModeValue ModeValue
        {
            get { return ModeValue.Mixed; }
        }
    }
    public interface IOutside
    {
        float OutsideTemperature { get; }
    }

    public class WarnWrapper
    {
        public WarnWrapper()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        public WarnWrapper(IList list)
        {
            this.WarnList = list;
        }

        /// <summary>
        /// 
        /// </summary>
        public IList WarnList
        {
            get
            {
                if (_warnList == null)
                {
                    _warnList = new ArrayList();
                }
                return _warnList;
            }
            set { _warnList = value; }
        } private IList _warnList;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (object item in this.WarnList)
            {
                sb.Append(item.ToString());
                sb.Append(";");
            }
            return sb.ToString();
        }
    }

    public class GRData : DataBase
    {
        private const string FloatFormat = "f2",
            IntFormat = "G";

        #region Members
        private float _GT1;
        private float _BT1;
        private float _GT2;
        private float _BT2;
        private float _OT;
        private float _GTBase2;
        private float _GP1;
        private float _BP1;
        private float _WL;
        private float _GP2;
        private float _BP2;
        private float _I1;
        private float _I2;
        private float _IR;
        private Int32 _S1;
        private Int32 _S2;
        private Int32 _SR;
        private int _OD;
        private float _PA2;
        private PumpStatus _CM1;
        private PumpStatus _CM2;
        private PumpStatus _CM3;
        private PumpStatus _RM1;
        private PumpStatus _RM2;

        private WarnWrapper _warnWrapper;
        #endregion //Members

        #region GRData
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="cardSn"></param>
        public GRData()
        {
        }
        #endregion //GRData

        #region GT1
        /// <summary>
        /// 
        /// </summary>
        [DataItem("一次供温", 1, Unit.Centidegree, FloatFormat)]
        public float GT1
        {
            get { return _GT1; }
            set { _GT1 = value; }
        }
        #endregion //GT1

        #region BT1
        /// <summary>
        /// 
        /// </summary>
        [DataItem("一次回温", 2, Unit.Centidegree, FloatFormat)]
        public float BT1
        {
            get { return _BT1; }
            set { _BT1 = value; }
        }
        #endregion //BT1

        #region GT2
        /// <summary>
        /// 
        /// </summary>
        [DataItem("二次供温", 3, Unit.Centidegree, FloatFormat)]
        public float GT2
        {
            get { return _GT2; }
            set { _GT2 = value; }
        }
        #endregion //GT2

        #region BT2
        /// <summary>
        /// 
        /// </summary>
        [DataItem("二次回温", 4, Unit.Centidegree, FloatFormat)]
        public float BT2
        {
            get { return _BT2; }
            set { _BT2 = value; }
        }
        #endregion //BT2

        #region OT
        /// <summary>
        /// 
        /// </summary>
        [DataItem("室外温度", 5, Unit.Centidegree, FloatFormat)]
        public float OT
        {
            get { return _OT; }
            set { _OT = value; }
        }
        #endregion //OT

        #region GTBase2
        /// <summary>
        /// 
        /// </summary>
        [DataItem("供温基准", 6, Unit.Centidegree, FloatFormat)]
        public float GTBase2
        {
            get { return _GTBase2; }
            set { _GTBase2 = value; }
        }
        #endregion //GTBase2

        #region GP1
        /// <summary>
        /// 
        /// </summary>
        [DataItem("一次供压", 7, Unit.Mpa, FloatFormat)]
        public float GP1
        {
            get { return _GP1; }
            set { _GP1 = value; }
        }
        #endregion //GP1

        #region BP1
        /// <summary>
        /// 
        /// </summary>
        [DataItem("一次回压", 8, Unit.Mpa, FloatFormat)]
        public float BP1
        {
            get { return _BP1; }
            set { _BP1 = value; }
        }
        #endregion //BP1

        #region WL
        /// <summary>
        /// 
        /// </summary>
        [DataItem("水箱水位", 9, Unit.M, FloatFormat)]
        public float WL
        {
            get { return _WL; }
            set { _WL = value; }
        }
        #endregion //WL

        #region GP2
        /// <summary>
        /// 
        /// </summary>
        [DataItem("二次供压", 10, Unit.Mpa, FloatFormat)]
        public float GP2
        {
            get { return _GP2; }
            set { _GP2 = value; }
        }
        #endregion //GP2

        #region BP2
        /// <summary>
        /// 
        /// </summary>
        [DataItem("二次回压", 11, Unit.Mpa, FloatFormat)]
        public float BP2
        {
            get { return _BP2; }
            set { _BP2 = value; }
        }
        #endregion //BP2

        #region I1
        /// <summary>
        /// 
        /// </summary>
        [DataItem("一次瞬时", 12, Unit.M3PerHour, FloatFormat)]
        public float I1
        {
            get { return _I1; }
            set { _I1 = value; }
        }
        #endregion //I1

        #region I2
        /// <summary>
        /// 
        /// </summary>
        [DataItem("二次瞬时", 13, Unit.M3PerHour, FloatFormat)]
        public float I2
        {
            get { return _I2; }
            set { _I2 = value; }
        }
        #endregion //I2

        #region IR
        /// <summary>
        /// 
        /// </summary>
        [DataItem("补水瞬时", 14, Unit.M3PerHour, FloatFormat)]
        public float IR
        {
            get
            {
                if (_IR < 0)
                {
                    _IR = 0;
                }
                return _IR;
            }
            set { _IR = value; }
        }
        #endregion //IR

        #region S1
        [DataItem("一次累计", 15, Unit.M3, IntFormat)]
        /// <summary>
        /// 
        /// </summary>
        public Int32 S1
        {
            get { return _S1; }
            set { _S1 = value; }
        }
        #endregion //S1

        #region S2
        [DataItem("二次累计", 16, Unit.M3, IntFormat)]
        public Int32 S2
        {
            get { return _S2; }
            set { _S2 = value; }
        }
        #endregion //S1

        #region SR
        /// <summary>
        /// 
        /// </summary>
        [DataItem("补水累计", 17, Unit.M3, IntFormat)]
        public Int32 SR
        {
            get { return _SR; }
            set { _SR = value; }
        }
        #endregion //SR

        #region OD
        /// <summary>
        /// 
        /// </summary>
        [DataItem("阀门开度", 18, Unit.Percentage, IntFormat)]
        public int OD
        {
            get { return _OD; }
            set { _OD = value; }
        }
        #endregion //OD

        #region PA2
        /// <summary>
        /// 
        /// </summary>
        public float PA2
        {
            get { return _PA2; }
            set { _PA2 = value; }
        }
        #endregion //PA2

        #region IH1
        /// <summary>
        /// 
        /// </summary>
        [DataItem("瞬时热量", 19, Unit.GJPerHour, FloatFormat)]
        public double IH1
        {
            get
            {
                return _iH1;
            }
            set
            {
                _iH1 = value;
            }
        } private double _iH1;
        #endregion //IH1

        #region SH1
        /// <summary>
        /// 
        /// </summary>
        [DataItem("累计热量", 20, Unit.GJ, IntFormat)]
        public double SH1
        {
            get
            {
                return _sH1;
            }
            set
            {
                _sH1 = value;
            }
        } private double _sH1;
        #endregion //SH1

        #region CM1
        /// <summary>
        /// 
        /// </summary>
        [DataItem("循环泵1", 119, Unit.None)]
        public PumpStatus CM1
        {
            get
            {
                if (_CM1 == null)
                {
                    _CM1 = PumpStatus.Stop;
                }
                return _CM1;
            }
            set { _CM1 = value; }
        }
        #endregion //CM1

        #region PumpStatus CM2
        /// <summary>
        /// 
        /// </summary>
        [DataItem("循环泵2", 120, Unit.None)]
        public PumpStatus CM2
        {
            get
            {
                if (_CM2 == null)
                {
                    _CM2 = PumpStatus.Stop;
                }
                return _CM2;
            }
            set { _CM2 = value; }
        }
        #endregion //CM2

        #region CM3
        /// <summary>
        /// 
        /// </summary>
        [DataItem("循环泵3", 121, Unit.None)]
        public PumpStatus CM3
        {
            get
            {

                if (_CM3 == null)
                {
                    _CM3 = PumpStatus.Stop;
                }
                return _CM3;
            }
            set { _CM3 = value; }
        }
        #endregion //CM3

        #region RM1
        /// <summary>
        /// 
        /// </summary>
        [DataItem("补水泵1", 122, Unit.None)]
        public PumpStatus RM1
        {
            get
            {
                if (_RM1 == null)
                {
                    _RM1 = PumpStatus.Stop;
                }
                return _RM1;
            }
            set { _RM1 = value; }
        }
        #endregion //RM1

        #region RM2
        /// <summary>
        /// 
        /// </summary>
        [DataItem("补水泵2", 123, Unit.None)]
        public PumpStatus RM2
        {
            get
            {
                if (_RM2 == null)
                {
                    _RM2 = PumpStatus.Stop;
                }
                return _RM2;
            }
            set { _RM2 = value; }
        }
        #endregion //RM2


        [DataItem("循环泵频率", 124, Unit.None)]
        public string CMFrequencyDisplay
        {
            get
            {
                return this.CyclePumpDatas.ToString();
            }
        }

        [DataItem("补水泵频率", 125, Unit.None)]
        public string RMFrequencyDisplay
        {
            get 
            {
                return this.RecruitPumpDatas.ToString();
            }
        }

        #region Warn
        /// <summary>
        /// 
        /// </summary>
        [DataItem("报警", 126, Unit.None)]
        public WarnWrapper Warn
        {
            get
            {
                if (_warnWrapper == null)
                {
                    _warnWrapper = new WarnWrapper();
                }
                return _warnWrapper;
            }
            set { _warnWrapper = value; }
        }
        #endregion //Warn

        #region CyclePumpDatas
        /// <summary>
        /// 
        /// </summary>
        public PumpDataCollection CyclePumpDatas
        {
            get
            {
                if (_cyclePumpDatas == null)
                {
                    _cyclePumpDatas = new PumpDataCollection();
                }
                return _cyclePumpDatas;
            }
        } private PumpDataCollection _cyclePumpDatas;
        #endregion //CyclePumpDatas

        #region RecruitPumpDatas
        /// <summary>
        /// 
        /// </summary>
        public PumpDataCollection RecruitPumpDatas
        {
            get
            {
                if (_recruitPumpDatas == null)
                {
                    _recruitPumpDatas = new PumpDataCollection();
                }
                return _recruitPumpDatas;
            }
        } private PumpDataCollection _recruitPumpDatas;
        #endregion //RecruitPumpDatas
    }

    /// <summary>
    /// 
    /// </summary>
    public enum XD1100TemperatureControlModeEnum
    {
        LineAndGT2 = 0,
        LineAndBT2 = 1,
        LineAndDiffT2 = 2,
        SettingAndGT2 = 3,
        SettingAndBT2 = 4,
        SettingAndDiffT2 = 5,
        ValveOpenDegree = 6,
    }

    /// <summary>
    /// 
    /// </summary>
    public class XD1100Defines
    {
        /// <summary>
        /// 
        /// </summary>
        static public XD1100TemperatureControlModeCollection TemperatureControlModeCollection
        {
            get
            {
                if (_temperatureControlModeCollection == null)
                {
                    _temperatureControlModeCollection = new XD1100TemperatureControlModeCollection();
                    _temperatureControlModeCollection.Add(
                        new XD1100TemperatureControlMode(XD100ModbusStrings.LineAndGT2,
                            XD1100TemperatureControlModeEnum.LineAndGT2));

                    _temperatureControlModeCollection.Add(
                        new XD1100TemperatureControlMode(XD100ModbusStrings.LineAndBT2,
                            XD1100TemperatureControlModeEnum.LineAndBT2));

                    _temperatureControlModeCollection.Add(
                        new XD1100TemperatureControlMode(XD100ModbusStrings.LineAndDiffT2 ,
                            XD1100TemperatureControlModeEnum.LineAndDiffT2));

                    _temperatureControlModeCollection.Add(
                        new XD1100TemperatureControlMode(XD100ModbusStrings.SettingAndGT2 ,
                            XD1100TemperatureControlModeEnum.SettingAndGT2));

                    _temperatureControlModeCollection.Add(
                        new XD1100TemperatureControlMode(XD100ModbusStrings.SettingAndBT2,
                            XD1100TemperatureControlModeEnum.SettingAndBT2));

                    
                    _temperatureControlModeCollection.Add(
                        new XD1100TemperatureControlMode(XD100ModbusStrings.SettingAndDiffT2,
                            XD1100TemperatureControlModeEnum.SettingAndDiffT2));

                    _temperatureControlModeCollection.Add(
                        new XD1100TemperatureControlMode(XD100ModbusStrings.ValveOpenDegree,
                            XD1100TemperatureControlModeEnum.ValveOpenDegree));
                }
                return _temperatureControlModeCollection;
            }
        } static private XD1100TemperatureControlModeCollection _temperatureControlModeCollection;
    }

    /// <summary>
    /// 
    /// </summary>
    public class XD1100TemperatureControlMode
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        //public XD1100TemperatureControlMode(string name, int value)
        public XD1100TemperatureControlMode(string name, XD1100TemperatureControlModeEnum mode)
        {
            this.Name = name;
            //this.Value = value;
            this._mode = mode;
        }

        #region Name
        /// <summary>
        /// 
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        } private string _name;
        #endregion //Name

        /// <summary>
        /// 
        /// </summary>
        public int Value
        {
            get { return (int)this.Mode; }
        }

        /// <summary>
        /// 
        /// </summary>
        public XD1100TemperatureControlModeEnum Mode
        {
            get { return this._mode;}
        } private XD1100TemperatureControlModeEnum _mode;


    }

    /// <summary>
    /// 
    /// </summary>
    public class XD1100TemperatureControlModeCollection : Collection<XD1100TemperatureControlMode>
    {
        
    }
}