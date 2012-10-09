using System;
using Xdgk.Common;

namespace C3.Communi
{
    abstract public class ParameterBase : IParameter
    {
        #region Constructor
        #region Parameter
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="orderNumber"></param>
        public ParameterBase(string name, Type valueType, object value, int orderNumber)
            : this(name, valueType, value, null, orderNumber, null)
        {
        }
        #endregion //Parameter

        //#region Parameter
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="name"></param>
        ///// <param name="value"></param>
        ///// <param name="unit"></param>
        ///// <param name="orderNumber"></param>
        //public Parameter(string name, object value, Unit unit, int orderNumber)
        //    : this(name, value, unit, orderNumber, null)
        //{

        //}
        //#endregion //Parameter

        //#region Parameter
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="name"></param>
        ///// <param name="valueType"></param>
        ///// <param name="value"></param>
        ///// <param name="orderNumber"></param>
        //public Parameter(string name, Type valueType, object value, int orderNumber)
        //    : this(name, value, null, orderNumber, null)
        //{

        //}
        //#endregion //Parameter

        #region Parameter
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="valueType"></param>
        /// <param name="value"></param>
        /// <param name="unit"></param>
        /// <param name="orderNumber"></param>
        /// <param name="description"></param>
        public ParameterBase(string name, Type valueType, object value, Unit unit, int orderNumber, string description)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            if (valueType == null)
            {
                throw new ArgumentNullException("valueType");
            }

            this.Name = name;
            this.ValueType = valueType;
            this.Value = value;
            this.Unit = unit;
            this.OrderNumber = orderNumber;
            this.Description = description;
        }
        #endregion //Parameter
        #endregion //Constructor

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
                if (value == null || value.Trim().Length == 0)
                {
                    throw new ArgumentException("Name is null or empty");
                }
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
            set
            {
                SetValue(value);
            }
        } private object _value;
        #endregion //Value

        #region SetValue
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        protected void SetValue(object value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("Value");
            }

            if (value != _value)
            {
                this.VerifyValue(value);
                this._value = value;
            }
        }
        #endregion //SetValue

        #region VerifyValue
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        virtual protected void VerifyValue(object value)
        {
            // value is valuetype
            //
            //if (value.GetType() != this.ValueType)
            if (!this.ValueType.IsInstanceOfType(value))
            {
                string s = string.Format("value type is '{0}', but expect is '{1}'",
                        value.GetType().Name,
                        this.ValueType.GetType().Name);
                throw new InvalidOperationException(s);
            }
        }
        #endregion //VerifyValue

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

        #region ValueType
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
                //if (_valueType == typeof(NullCommuniPortConfig))
                //{
                //    int b = 0;
                //}
            }
        } private Type _valueType;
        #endregion //ValueType

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
                    _text = this.Name;
                }
                return _text;
            }
            set
            {
                _text = value;
            }
        } private string _text;

        #endregion
    }

}
