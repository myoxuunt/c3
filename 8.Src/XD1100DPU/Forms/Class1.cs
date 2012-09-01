using System;
using System.Collections.Generic;
using System.Text;
using Xdgk.Common;

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
                        new TemperatureControlMode("CZGR.XD100.XD100ModbusStrings.LineAndGT2",
                            TemperatureControlModeEnum.LineAndGT2));

                    _temperatureControlModeCollection.Add(
                        new TemperatureControlMode("CZGR.XD100.XD100ModbusStrings.LineAndBT2",
                            TemperatureControlModeEnum.LineAndBT2));

                    _temperatureControlModeCollection.Add(
                        new TemperatureControlMode("CZGR.XD100.XD100ModbusStrings.LineAndDiffT2 ",
                            TemperatureControlModeEnum.LineAndDiffT2));

                    _temperatureControlModeCollection.Add(
                        new TemperatureControlMode("CZGR.XD100.XD100ModbusStrings.SettingAndGT2 ",
                            TemperatureControlModeEnum.SettingAndGT2));

                    _temperatureControlModeCollection.Add(
                        new TemperatureControlMode("CZGR.XD100.XD100ModbusStrings.SettingAndBT2",
                            TemperatureControlModeEnum.SettingAndBT2));

                    
                    _temperatureControlModeCollection.Add(
                        new TemperatureControlMode("CZGR.XD100.XD100ModbusStrings.SettingAndDiffT2",
                            TemperatureControlModeEnum.SettingAndDiffT2));

                    _temperatureControlModeCollection.Add(
                        new TemperatureControlMode("CZGR.XD100.XD100ModbusStrings.ValveOpenDegree",
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
