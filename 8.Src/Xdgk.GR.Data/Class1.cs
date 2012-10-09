using System;
using C3.Data;
using Xdgk.Common;
using Xdgk.GR.Common;

namespace Xdgk.GR.Common
{
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