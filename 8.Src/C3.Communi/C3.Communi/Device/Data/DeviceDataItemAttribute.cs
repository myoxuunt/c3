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
    public class DeviceDataItemAttribute : System.Attribute
    {

        #region Constructor
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
