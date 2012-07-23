using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace C3.Communi
{
    // TODO: 2012-07-23 move to c3
    //
    /// <summary>
    /// 
    /// </summary>
    public class UnitType
    {
        private UnitType(string text)
        {
            if (text == null)
            {
                _text = string.Empty;
            }

            this._text = text;
        }

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
        } private string _text;
        #endregion //Text


        // TODO: 2012-07-23 unit type text
        //
        static public readonly UnitType
            None = new UnitType(""),
            Length = new UnitType("Length"),
            Press = new UnitType("Press"),
            Temperature = new UnitType("Temperature"),
            Flow = new UnitType("Flow"),
            Volume = new UnitType("Volume"),
            Time = new UnitType("Time");
    }

    /// <summary>
    /// 
    /// </summary>
    public class Unit
    {
        static Unit()
        {
            object[] objs = new object[] 
            {
                UnitType.None , new string[] {""},

                UnitType.Flow  ,new string[] {"m3/s", "m3/h"},
                UnitType.Length , new string[] {"mm","cm","m"},
                UnitType.Press , new string[] {"pa","mpa"},
                UnitType.Temperature, new string[] {"c"},
                UnitType.Time, new string[] {"second","minute","hour"},
                UnitType.Volume,  new string[] {"m3"},
            };

            for (int i = 0; i < objs.Length; i += 2)
            {
                UnitType ut = (UnitType)objs[i];
                string[] values = (string[])objs[i + 1];
                foreach (string value in values)
                {
                    Unit unit = new Unit(value, ut);

                    string key = value.ToUpper();
                    _hash[key] = unit;
                }
            }

        }

        static private Hashtable _hash = new Hashtable();

        #region units

        public const string
            // none
            //
            None = "None",

            // length
            //
            Mm = "mm",
            Cm = "cm",
            M = "m",

            // time
            //
            Second = "second",
            Minute = "minute",
            Hour = "hour",

            // press
            //
            Pa = "pa",
            Mpa = "mpa",


            // temperature
            //
            Centidegree = "c",

            // flow
            //
            M3PerHour = "m3/h",
            M3PerSecond = "m3/s",

            // volume
            //
            M3 = "m3";
        #endregion //

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        static public Unit FindByName(string name)
        {
            string key = name.Trim().ToUpper();
            object obj = _hash[key];
            if (obj != null)
            {
                return (Unit)obj;
            }
            else
            {
                throw new ArgumentException(string.Format("not fine unit '{0}'", name));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="unitType"></param>
        private Unit(string text, UnitType unitType)
        {
            if (text == null)
            {
                text = string.Empty;
            }
            this._text = text;

            if (unitType == null)
            {
                throw new ArgumentNullException("unitType");
            }
            this._unitType = unitType;
        }

        #region Text
        /// <summary>
        /// 
        /// </summary>
        public string Text
        {
            get
            {
                return _text;
            }
        } private string _text;
        #endregion //Text

        #region UnitType
        /// <summary>
        /// 
        /// </summary>
        public UnitType UnitType
        {
            get
            {
                return _unitType;
            }
        } private UnitType _unitType;
        #endregion //UnitType

    }

    /// <summary>
    /// 
    /// </summary>
    public class DeviceDataItemAttribute : System.Attribute
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="orderNumber"></param>
        /// <param name="unit"></param>
        public DeviceDataItemAttribute(string name, int orderNumber, string unit)
            : this(name, orderNumber, Unit.FindByName(unit))
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="orderNumber"></param>
        /// <param name="unit"></param>
        public DeviceDataItemAttribute(string name, int orderNumber, Unit unit)
        {
            this._name = name;
            this._orderNumber = orderNumber;

            if (unit == null)
            {
                throw new ArgumentNullException("unit");
            }
            this._unit = unit;
        }

        #region Name
        /// <summary>
        /// 
        /// </summary>
        public string Name
        {
            get
            {
                if (_name == null)
                {
                    _name = string.Empty;
                }
                return _name;
            }
        } private string _name;
        #endregion //Name

        #region OrderNumber
        /// <summary>
        /// 
        /// </summary>
        public int OrderNumber
        {
            get
            {
                return _orderNumber;
            }
        } private int _orderNumber;
        #endregion //OrderNumber

        #region Unit
        /// <summary>
        /// 
        /// </summary>
        public Unit Unit
        {
            get
            {
                return _unit;
            }
        } private Unit _unit;
        #endregion //Unit

        #region Format
        /// <summary>
        /// 
        /// </summary>
        public string Format
        {
            get
            {
                if (_format == null)
                {
                    _format = string.Empty;
                }
                return _format;
            }
            set
            {
                _format = value;
            }
        } private string _format;
        #endregion //Format

    }


    /// <summary>
    /// 
    /// </summary>
    public class TestDeviceData : DeviceDataBase
    {
        public override string ToString()
        {
            return base.ToString();
        }

        static public void T()
        {
            Unit u = Unit.FindByName(Unit.Cm);
            TestDeviceData dat = new TestDeviceData();
            PropertyInfo[] pis = dat.GetType().GetProperties();
            foreach (PropertyInfo pi in pis)
            {
                object[] atts = pi.GetCustomAttributes(typeof(DeviceDataItemAttribute), false);
                if (atts.Length > 0)
                {
                    //Console.WriteLine(atts[0].ToString());
                    DeviceDataItemAttribute att = (DeviceDataItemAttribute)atts[0];
                    object value = pi.GetValue(dat, null);
                    Console.WriteLine("{0} {1} {2} {3}", att.Name, att.OrderNumber, value, att.Unit.Text);

                }
            }
        }

        #region DoubleValue
        /// <summary>
        /// 
        /// </summary>

        [DeviceDataItem("DV", 0, Unit.Cm)]
        public double DoubleValue
        {
            get
            {
                //new DeviceDataItemAttribute("", 0, Unit.Cm);
                return _doubleValue;
            }
            set
            {
                _doubleValue = value;
            }
        } private double _doubleValue = 1.23d;
        #endregion //DoubleValue

        #region FloatValue
        /// <summary>
        /// 
        /// </summary>
        [DeviceDataItem("intvalue", 4, Unit.M3PerHour)]
        public float FloatValue
        {
            get
            {
                return _floatValue;
            }
            set
            {
                _floatValue = value;
            }
        } private float _floatValue = 12.567f;
        #endregion //FloatValue

        #region IntValue
        /// <summary>
        /// 
        /// </summary>
        [DeviceDataItem("intvalue", 1, Unit.M3PerHour)]
        public int IntValue
        {
            get
            {
                return _intValue;
            }
            set
            {
                _intValue = value;
            }
        } private int _intValue = 0987;

        #region S
        /// <summary>
        /// 
        /// </summary>
        [DeviceDataItem("stringvalue", 9999, Unit.M3PerHour)]
        public string S
        {
            get
            {
                if (_s == null)
                {
                    _s = string.Empty;
                }
                return _s;
            }
            set
            {
                _s = value;
            }
        } private string _s="stringvlu";
        #endregion //S
        #endregion //IntValue
    }
}
