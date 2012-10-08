using System;
using C3.Data;
using Xdgk.Common;
using Xdgk.GR.Data;

namespace Xdgk.XD100Modbus
{
    /// <summary>
    /// 
    /// </summary>
    public enum TemperatureControlModeEnum
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
    public class XD100ModbusDefines
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
                    _temperatureControlModeCollection.Add(
                        new TemperatureControlMode(XD100ModbusStrings.LineAndGT2,
                            TemperatureControlModeEnum.LineAndGT2));

                    _temperatureControlModeCollection.Add(
                        new TemperatureControlMode(XD100ModbusStrings.LineAndBT2,
                            TemperatureControlModeEnum.LineAndBT2));

                    _temperatureControlModeCollection.Add(
                        new TemperatureControlMode(XD100ModbusStrings.LineAndDiffT2 ,
                            TemperatureControlModeEnum.LineAndDiffT2));

                    _temperatureControlModeCollection.Add(
                        new TemperatureControlMode(XD100ModbusStrings.SettingAndGT2 ,
                            TemperatureControlModeEnum.SettingAndGT2));

                    _temperatureControlModeCollection.Add(
                        new TemperatureControlMode(XD100ModbusStrings.SettingAndBT2,
                            TemperatureControlModeEnum.SettingAndBT2));

                    
                    _temperatureControlModeCollection.Add(
                        new TemperatureControlMode(XD100ModbusStrings.SettingAndDiffT2,
                            TemperatureControlModeEnum.SettingAndDiffT2));

                    _temperatureControlModeCollection.Add(
                        new TemperatureControlMode(XD100ModbusStrings.ValveOpenDegree,
                            TemperatureControlModeEnum.ValveOpenDegree));
                }
                return _temperatureControlModeCollection;
            }
        } static private TemperatureControlModeCollection _temperatureControlModeCollection;
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
            get { return this._mode;}
        } private TemperatureControlModeEnum _mode;


    }

    /// <summary>
    /// 
    /// </summary>
    public class TemperatureControlModeCollection : Collection<TemperatureControlMode>
    {
        
    }
}

namespace Xdgk.GR.Data
{
    /// <summary>
    /// 
    /// </summary>
    public class FlowmeterData : DataBase
    {
        #region InstantFlux
        /// <summary>
        /// 
        /// </summary>
        [DataItem("瞬时", 10, Unit.M3PerSecond, "f2")]
        public double InstantFlux
        {
            get
            {
                return _instantFlux;
            }
            set
            {
                _instantFlux = value;
            }
        } private double _instantFlux;
        #endregion //InstantFlux

        #region Sum
        /// <summary>
        /// 
        /// </summary>
        [DataItem("累计", 20, Unit.M3, "f0")]
        public double Sum
        {
            get
            {
                return _sum;
            }
            set
            {
                _sum = value;
            }
        } private double _sum;
        #endregion //Sum
    }

    /// <summary>
    /// 
    /// </summary>
    public enum PumpStatusEnum
    {
        Stop = 0,
        Run = 1,
    }

    public class PumpStatus
    {
        /// <summary>
        /// 
        /// </summary>
        static public PumpStatus Run = new PumpStatus(PumpStatusEnum.Run, "运行");
        /// <summary>
        /// 
        /// </summary>
        static public PumpStatus Stop = new PumpStatus(PumpStatusEnum.Stop, "停止");


        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        static public PumpStatus Find(PumpStatusEnum value)
        {
            if (value == PumpStatusEnum.Run)
            {
                return Run;
            }
            else if (value == PumpStatusEnum.Stop)
            {
                return Stop;
            }
            else
            {
                throw new ArgumentException(value.ToString());
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        private PumpStatus(PumpStatusEnum value, string text)
        {
            this.PumpStatusEnum = value;
            this.Text = text;
        }

        #region PumpStatusEnum
        /// <summary>
        /// 
        /// </summary>
        public PumpStatusEnum PumpStatusEnum
        {
            get
            {
                return _pumpStatusEnum;
            }
            private set
            {
                _pumpStatusEnum = value;
            }
        } private PumpStatusEnum _pumpStatusEnum;
        #endregion //PumpStatusEnum

        #region Text
        /// <summary>
        /// 
        /// </summary>
        public string Text
        {
            get
            {
                if (_text == null)
                {
                    _text = string.Empty;
                }
                return _text;
            }
            private set
            {
                _text = value;
            }
        } private string _text;
        #endregion //Text

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Text;
        }
    }
}
