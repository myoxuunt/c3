using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Xdgk.Common
{
    /// <summary>
    /// 
    /// </summary>
    public class DataItemAttribute : System.Attribute
    {

        #region Constructor

        public DataItemAttribute(string name, int orderNumber, string unit)
            : this(name, orderNumber, unit, null)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="orderNumber"></param>
        /// <param name="unit"></param>
        public DataItemAttribute(string name, int orderNumber, string unit, string format)
            : this(name, orderNumber, Unit.FindByName(unit), format)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="orderNumber"></param>
        /// <param name="unit"></param>
        public DataItemAttribute(string name, int orderNumber, Unit unit, string format)
        {
            this._name = name;
            this._orderNumber = orderNumber;

            if (unit == null)
            {
                throw new ArgumentNullException("unit");
            }
            this._unit = unit;
            this._format = format;
            Console.WriteLine(this._format);
        }
        #endregion //Constructor

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
            set
            {
                _orderNumber = value;
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

}
