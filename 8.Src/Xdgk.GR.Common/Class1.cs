using System;
using Xdgk.Common;
using Xdgk.GR.Common;
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