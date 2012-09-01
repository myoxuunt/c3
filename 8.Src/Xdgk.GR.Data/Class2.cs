using System;
using System.Collections.Generic;
using System.Text;
using Xdgk.Common;

namespace Xdgk.GR.Data
{
    /// <summary>
    /// 
    /// </summary>
    public enum TemperatureControlModeEnum
    {
        OT_GT2 = 0,
        Time_GT2 = 1,
    }

    /// <summary>
    /// 
    /// </summary>
    public enum ValveTypeEnum
    {
        AnalogValve = 0,
        TrinityValve = 1,
    }

    /// <summary>
    /// 
    /// </summary>
    public class XD100Defines
    {
        /// <summary>
        ///  
        /// </summary>
        static public TemperatureControlModeCollection TemperatureControlModeCollection
        {
            get
            {
                if (_temperatureControlModeCollection == null)
                {
                    _temperatureControlModeCollection = new TemperatureControlModeCollection();

                    TemperatureControlMode m = null;
                    m = new TemperatureControlMode(
                        //CZGR1.XD100.XD100Strings.OTGT2ControlLine, 
                        "温度控制曲线",
                        TemperatureControlModeEnum.OT_GT2);
                    _temperatureControlModeCollection.Add(m);

                    m = new TemperatureControlMode(
                        //CZGR1.XD100.XD100Strings.TimeControlLine, 
                        "分时控制曲线",
                        TemperatureControlModeEnum.Time_GT2);
                    _temperatureControlModeCollection.Add(m);
                }
                return _temperatureControlModeCollection;
            }
        } static private TemperatureControlModeCollection _temperatureControlModeCollection;


        /// <summary>
        /// 
        /// </summary>
        static public ValveTypeCollection ValveTypeCollection
        {
            get
            {
                if (_valveTypeCollection == null)
                {
                    _valveTypeCollection = new ValveTypeCollection();
                    ValveType v = null;
                    v = new ValveType(
                        //CZGR1.XD100.XD100Strings.AnalogValve, 
                        "模拟阀",
                        ValveTypeEnum.AnalogValve);
                    _valveTypeCollection.Add(v);

                    v = new ValveType(
                        //CZGR1.XD100.XD100Strings.TrinityValve, 
                        "三位阀",
                        ValveTypeEnum.TrinityValve);
                    _valveTypeCollection.Add(v);
                }
                return _valveTypeCollection;
            }
        } static private ValveTypeCollection _valveTypeCollection;

    }

    /// <summary>
    /// 
    /// </summary>
    public class OTControlLineDefines
    {
        public const int OTMin = -50;
        public const int OTMax = 50;
        public const int GT2Min = 0;
        public const int GT2Max = 99;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ot"></param>
        /// <returns></returns>
        static public bool CheckOTValue(int ot)
        {
            return (ot >= OTMin) && (ot <= OTMax);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gt2"></param>
        /// <returns></returns>
        static public bool CheckGT2Value(int gt2)
        {
            return (gt2 >= GT2Min) && (gt2 <= GT2Max);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class TimeControlLineDefines
    {
        public const int MinAdjustValue = -9;
        public const int MaxAdjustValue = +9;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        static public bool CheckTimeAdjustValue(int value)
        {
            return value >= MinAdjustValue && value <= MaxAdjustValue;
        }
    }


    /// <summary>
    /// 
    /// </summary>
    public class TemperatureControlMode
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        //public TemperatureControlMode(string name, int value)
        public TemperatureControlMode(string name, TemperatureControlModeEnum mode)
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
        public TemperatureControlModeEnum Mode
        {
            get { return this._mode; }
        } private TemperatureControlModeEnum _mode;


    }

    /// <summary>
    /// 
    /// </summary>
    public class TemperatureControlModeCollection : Collection<TemperatureControlMode>
    {

    }

    /// <summary>
    /// 
    /// </summary>
    public class ValveType
    {
        /// <summary>
        /// 
        /// </summary>
        //public ValveType (string name, int value)
        public ValveType(string name, ValveTypeEnum type)
        {
            this.Name = name;
            //this.Value = value;
            this._valveTypeEnum = type;
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

        #region Value
        /// <summary>
        /// 
        /// </summary>
        public int Value
        {
            get { return (int)this._valveTypeEnum; }
        }
        #endregion //Value

        /// <summary>
        /// 
        /// </summary>
        public ValveTypeEnum ValveTypeEnum
        {
            get { return _valveTypeEnum; }
        } private ValveTypeEnum _valveTypeEnum;
    }

    /// <summary>
    /// 
    /// </summary>
    public class ValveTypeCollection : Collection<ValveType>
    {
    }
}

