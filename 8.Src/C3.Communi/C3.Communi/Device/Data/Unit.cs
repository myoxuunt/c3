using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace C3.Communi
{
    /// <summary>
    /// 
    /// </summary>
    public class Unit
    {

        #region Members
        /// <summary>
        /// 
        /// </summary>
        static private Hashtable _hash = new Hashtable();

        #region units

        /// <summary>
        /// 
        /// </summary>
        public const string
            // none
            //
            None = "",

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
        #endregion //units
        #endregion //Members

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
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
        #endregion //Constructor

        #region FindByName
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
        #endregion //FindByName

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

        #region ToString
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.Text;
        }
        #endregion //ToString

    }

}
