//using System;
//using System.Collections.Generic;
//using System.Text;
//using Xdgk.Common;

//namespace Xdgk.GR.Data
//{
//    /// <summary>
//    /// 
//    /// </summary>
//    public enum XD100TemperatureControlModeEnum
//    {
//        LineAndGT2 = 0,
//        LineAndBT2 = 1,
//        LineAndDiffT2 = 2,
//        SettingAndGT2 = 3,
//        SettingAndBT2 = 4,
//        SettingAndDiffT2 = 5,
//        ValveOpenDegree = 6,
//    }

//    /// <summary>
//    /// 
//    /// </summary>
//    public class XD1100Defines
//    {
//        /// <summary>
//        /// 
//        /// </summary>
//        static public XD1100TemperatureControlModeCollection XD1100TemperatureControlModeCollection
//        {
//            get
//            {
//                if (_temperatureControlModeCollection == null)
//                {
//                    _temperatureControlModeCollection = new XD1100TemperatureControlModeCollection();
//                    _temperatureControlModeCollection.Add(
//                        new XD1100TemperatureControlMode("CZGR.XD100.XD100ModbusStrings.LineAndGT2",
//                            XD100TemperatureControlModeEnum.LineAndGT2));

//                    _temperatureControlModeCollection.Add(
//                        new XD1100TemperatureControlMode("CZGR.XD100.XD100ModbusStrings.LineAndBT2",
//                            XD100TemperatureControlModeEnum.LineAndBT2));

//                    _temperatureControlModeCollection.Add(
//                        new XD1100TemperatureControlMode("CZGR.XD100.XD100ModbusStrings.LineAndDiffT2 ",
//                            XD100TemperatureControlModeEnum.LineAndDiffT2));

//                    _temperatureControlModeCollection.Add(
//                        new XD1100TemperatureControlMode("CZGR.XD100.XD100ModbusStrings.SettingAndGT2 ",
//                            XD100TemperatureControlModeEnum.SettingAndGT2));

//                    _temperatureControlModeCollection.Add(
//                        new XD1100TemperatureControlMode("CZGR.XD100.XD100ModbusStrings.SettingAndBT2",
//                            XD100TemperatureControlModeEnum.SettingAndBT2));

                    
//                    _temperatureControlModeCollection.Add(
//                        new XD1100TemperatureControlMode("CZGR.XD100.XD100ModbusStrings.SettingAndDiffT2",
//                            XD100TemperatureControlModeEnum.SettingAndDiffT2));

//                    _temperatureControlModeCollection.Add(
//                        new XD1100TemperatureControlMode("CZGR.XD100.XD100ModbusStrings.ValveOpenDegree",
//                            XD100TemperatureControlModeEnum.ValveOpenDegree));
//                }
//                return _temperatureControlModeCollection;
//            }
//        } static private XD1100TemperatureControlModeCollection _temperatureControlModeCollection;
//    }

//    /// <summary>
//    /// 
//    /// </summary>
//    public class XD1100TemperatureControlMode
//    {
//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="name"></param>
//        /// <param name="value"></param>
//        //public XD1100TemperatureControlMode(string name, int value)
//        public XD1100TemperatureControlMode(string name, XD100TemperatureControlModeEnum mode)
//        {
//            this.Name = name;
//            //this.Value = value;
//            this._mode = mode;
//        }

//        #region Name
//        /// <summary>
//        /// 
//        /// </summary>
//        public string Name
//        {
//            get { return _name; }
//            set { _name = value; }
//        } private string _name;
//        #endregion //Name

//        /// <summary>
//        /// 
//        /// </summary>
//        public int Value
//        {
//            get { return (int)this.Mode; }
//        }

//        /// <summary>
//        /// 
//        /// </summary>
//        public XD100TemperatureControlModeEnum Mode
//        {
//            get { return this._mode;}
//        } private XD100TemperatureControlModeEnum _mode;


//    }

//    /// <summary>
//    /// 
//    /// </summary>
//    public class XD1100TemperatureControlModeCollection : Collection<XD1100TemperatureControlMode>
//    {
        
//    }
//}
