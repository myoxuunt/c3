
using System;
using Xdgk.Common;


namespace C3.Communi
{
    public class ReportItem
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="unit"></param>
        public ReportItem(string name, object value, Unit unit)
            : this(name, value, unit, null)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="unit"></param>
        public ReportItem(string name, object value, Unit unit, string format)
        {
            this.Name = name;
            this.Value = value;
            this.Unit = unit;
            this.Format = format;
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
            private set
            {
                _name = value;
            }
        } private string _name;
        #endregion //Name

        #region Value
        /// <summary>
        /// 
        /// </summary>
        public object Value
        {
            get
            {
                return _value;
            }
            private set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Value");
                }

                _value = value;
            }
        } private object _value;
        #endregion //Value

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
            private set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Unit");
                }
                _unit = value;
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
            private set
            {
                _format = value;
            }
        } private string _format;
        #endregion //Format

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string s = string.Format(
                    "Name: '{0}', Value: '{1}', Unit: '{2}'",
                    this.Name, this.Value, this.Unit
                    );

            return s;
        }
    }

}
