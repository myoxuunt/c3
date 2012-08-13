using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Xdgk.Common;
namespace C3.Communi.P
{
    #region IModel
    public interface IModel
    {
    }
    #endregion //IModel

    #region IViewer
    public interface IViewer
    {
        Control UC { get; }
        IController Controller { get; set; }
    }
    #endregion //IViewer

    #region IController
    public interface IController
    {
        IModel Model { get; set; }
        IViewer Viewer { get; set; }

        void UpdateModel();
        void UpdateViewer();
        bool Verify();
    }

    public class ControllerCollection: Collection<IController>
    {
    }
    #endregion //IController

    #region IParameter
    /// <summary>
    /// 
    /// </summary>
    public interface IParameter : IOrderNumber, IModel 
    {
        string Name { get; set; }
        string Text { get; set; }
        object Value { get; set; }
        //void SetValue(string stringValue);
        Type ValueType { get; set; }
        string Description { get; set; }
        Unit Unit { get; set; }
        // TODO: 2012-08-03
        //
        // ParameterOption Option{get;set;}

    }
    #endregion //IParameter

    #region ParameterCollection
    public class ParameterCollection : OrderNumberCollection<IParameter>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameterName"></param>
        /// <returns></returns>
        public IParameter this[string parameterName]
        {
            get
            {
                VerifyParameterName(parameterName);

                int index = this.Find(parameterName);
                if (index != -1)
                {
                    return this[index];
                }
                else
                {
                    return null;
                }
            }
            set
            {
                VerifyParameterName(parameterName);

                int index = this.Find(parameterName);
                if (index != -1)
                {
                    this.SetItem(index, value);
                }
                else
                {
                    this.Add(value);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameterName"></param>
        private void VerifyParameterName(string parameterName)
        {
            if (parameterName == null || parameterName.Trim().Length == 0)
            {
                throw new ArgumentException("parameterName is null or empty");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameterName"></param>
        /// <returns></returns>
        private int Find(string parameterName)
        {
            int index = -1;
            for (int i = 0; i < this.Count; i++)
            {
                IParameter item = this[i];
                if (StringHelper.Equal(item.Name, parameterName))
                {
                    index = i;
                    break;
                }
            }
            return index;
        }
    }
    #endregion //ParameterCollection

    #region ParameterBase
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
            // TODO: 2012-08-10 value is valuetype
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
                if (_valueType == typeof (NullCommuniPortConfig ))
                {
                    int b = 0;
                }
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
    #endregion //ParameterBase

    #region string...
    #region StringParameter
    public class StringParameter : ParameterBase
    {
        public StringParameter(string name, string value, int orderNumber)
            : base(name, typeof(string), value, orderNumber)
        {

        }
    }
    #endregion //StringParameter

    #region StringParameterViewer
    public class StringParameterViewer : IViewer
    {
        private UCStringParameterUI _uc = new UCStringParameterUI();

        #region IViewer 成员
        /// <summary>
        /// 
        /// </summary>
        public IController Controller
        {
            get
            {
                return _controller;
            }
            set
            {
                _controller = value;
            }
        } private IController _controller;

        /// <summary>
        /// 
        /// </summary>
        public string ParameterName
        {
            get { return this._uc.ParameterName; }
            set { this._uc.ParameterName = value; }
        }

        public string Value
        {
            get { return this._uc.Value; }
            set { this._uc.Value = value; }
        }

        public string Unit
        {
            get { return this._uc.Unit; }
            set { this._uc.Unit = value; }
        }
        
        #endregion

        #region IViewer 成员

        public Control UC
        {
            get
            {
                return _uc;
            }
        }

        #endregion
    }
    #endregion //StringParameterViewer

    #region StringParameterController
    public class StringParameterController : IController
    {
        public StringParameterController(StringParameter p)
        {
            this.Model = p;
        }

        #region IController 成员

        public IModel Model
        {
            get
            {
                return _model;
            }
            set
            {
                _model = value;
                UpdateViewer();
            }
        } private IModel _model;

        public IViewer Viewer
        {
            get
            {
                if (_stringParameterViewer == null)
                {
                    _stringParameterViewer = new StringParameterViewer();
                    _stringParameterViewer.Controller = this;
                }

                return _stringParameterViewer;
            }
            set
            {
                throw new NotImplementedException();
            }
        } private StringParameterViewer _stringParameterViewer;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private StringParameterViewer GetStringParameterViewer()
        {
            return this.Viewer as StringParameterViewer;
        }

        public void UpdateModel()
        {         StringParameter p = (StringParameter)this.Model;
            StringParameterViewer v = this.GetStringParameterViewer();
            p.Value = v.Value;
           
        }

        public void UpdateViewer()
        {
    StringParameter p = (StringParameter)this.Model;
            StringParameterViewer v = this.GetStringParameterViewer();
            v.ParameterName = p.Name;
            v.Value = p.Value.ToString();
            v.Unit = p.Unit.ToString();
        }

        public bool Verify()
        {
            return true;
        }

        #endregion
    }
    #endregion //StringParameterController
    #endregion //string...


#region group...
    public interface IGroup : IOrderNumber, IModel
    {
        string Name { get; set; }
        string Text { get; set; }
        ParameterCollection Parameters { get; }
    }

    public class Group : IGroup 
    {
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
        #endregion //Text

        #region Parameters
        /// <summary>
        /// 
        /// </summary>
        public ParameterCollection Parameters
        {
            get
            {
                if (_parameters == null)
                {
                    _parameters = new ParameterCollection();
                }
                return _parameters;
            }
        } private ParameterCollection _parameters;
        #endregion //Parameters
    }

    /// <summary>
    /// 
    /// </summary>
    public class GroupCollection : OrderNumberCollection<IGroup>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="create"></param>
        /// <returns></returns>
        public IGroup GetGroup( string name)
        {
            IGroup  r = null;
            foreach (IGroup item in this)
            {
                if (StringHelper.Equal(item.Name, name))
                {
                    r = item;
                    break;
                }
            }

            return r;
        }
    }

    public class GroupViewer : IViewer
    {

        #region IViewer 成员

        public IController Controller
        {
            get
            {
                return _controller;
            }
            set
            {
                this._controller = (GroupController)value;
            }
        } private GroupController _controller;

        #endregion

        #region IViewer 成员

        public Control UC
        {
            get { return _uc; }
        }
        private UCGroupUI _uc = new UCGroupUI();


        public void AddViewer(IViewer viewer)
        {
            this._uc.AddControl(viewer.UC);
        }
        #endregion
    }

    public class GroupController : IController
    {
        #region IController 成员

        public IModel Model
        {
            get
            {
                return _group;
            }
            set
            {
                _group = (IGroup)value;
                foreach (IController ctrl in this.Controllers)
                {
                    ((GroupViewer)this.Viewer).AddViewer(ctrl.Viewer);
                }
                UpdateViewer();
            }
        } private IGroup _group;


        /// <summary>
        /// 
        /// </summary>
        public ControllerCollection Controllers
        {
            get 
            {
                if (_controllers == null)
                {
                    _controllers = new ControllerCollection();
                    foreach (IParameter p in this._group.Parameters)
                    {
                        IController c = ControllerFactory.Create(p);
                        _controllers.Add(c);
                    }
                }
                return _controllers; 
            }
        } private ControllerCollection _controllers;

        /// <summary>
        /// 
        /// </summary>
        public IViewer Viewer
        {
            get
            {
                if (_groupViewer == null)
                {
                    _groupViewer = new GroupViewer();
                    _groupViewer.Controller = this;
                }
                return _groupViewer;
            }
            set
            {
                throw new NotImplementedException();
            }
        } private GroupViewer _groupViewer;

        public void UpdateModel()
        {
            //throw new NotImplementedException();
            foreach (IController item in this.Controllers)
            {
                item.UpdateModel();
            }
        }

        public void UpdateViewer()
        {
            foreach (IController item in this.Controllers)
            {
                item.UpdateViewer();
            }
        }

        public bool Verify()
        {
            bool r = true;
            foreach (IController item in this.Controllers)
            {
                if (!item.Verify())
                {
                    r = false;
                    break;
                }
            }
            return r;
        }

        #endregion
    }

    /// <summary>
    /// 
    /// </summary>
    public class GroupControllerCollection : Collection<GroupController>
    {
    }

#endregion //    


    #region NumberParameter
    public class NumberParameter : ParameterBase
    {
        public NumberParameter(string name, Type valueType, object value, int orderNumber)
            : base (name,valueType , value, orderNumber )
        {
            //TODO: check value type
            // 
            // is byte int32 short long float double ...
        }
    }
    #endregion //NumberParameter

    #region IParameterViewer
    /// <summary>
    /// 
    /// </summary>
    public interface IParameterViewer : IViewer 
    {
        Control Control { get; set; }
    }
    #endregion //IParameterViewer

    public class ControllerFactory
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        static public IController Create(IParameter p)
        {
            if (p is StringParameter)
            {
                StringParameterController c = new StringParameterController((StringParameter)p);
                return c;
            }
            throw new ArgumentException(p.ToString ());
        }

        static public IController Create(IGroup g)
        {
            GroupController c = new GroupController();
            c.Model = g;
            return c;
        }
    }
}
