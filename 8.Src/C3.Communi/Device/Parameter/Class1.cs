using System;
using System.Collections.Generic;
using System.Text;
using Xdgk.Common;

namespace C3.Communi
{
    class Class1
    {
    }

    /// <summary>
    /// 
    /// </summary>
    public interface IDeviceParameter
    {
        int OrderNumber { get; set; }
        string Name { get; set; }
        // TODO: 2012-08-02
        //
        //string Text { get; set; }
        object Value { get; set; }
        Type ValueType { get; set; }
        string Description { get; set; }
        Unit Unit { get; set; }
    }

    public class DeviceParameter : IDeviceParameter
    {
        #region IDeviceParameter 成员
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
            set
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
                if (_value == null)
                {
                    _value = new object();
                }
                return _value;
            }
            set
            {
                _value = value;
            }
        } private object _value;
        #endregion //Value

        #region Description
        /// <summary>
        /// 
        /// </summary>
        public string Description
        {
            get
            {
                if (_description == null)
                {
                    _description = string.Empty;
                }
                return _description;
            }
            set
            {
                _description = value;
            }
        } private string _description;
        #endregion //Description

        #region Unit
        /// <summary>
        /// 
        /// </summary>
        public Unit Unit
        {
            get
            {
                if (_unit == null)
                {
                    _unit = Unit.FindByName(Unit.None);
                }
                return _unit;
            }
            set
            {
                _unit = value;
            }
        } private Unit _unit;
        #endregion //Unit

        /// <summary>
        /// 
        /// </summary>
        public Type ValueType
        {
            get
            {
                return _valueType;
            }
            set
            {
                _valueType = value;
            }
        } private Type _valueType;

        #endregion
    }

    /// <summary>
    /// 
    /// </summary>
    public class DeviceParameterCollection : Collection<IDeviceParameter>
    {

    }
}
