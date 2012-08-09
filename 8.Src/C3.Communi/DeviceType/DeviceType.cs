using System;
using Xdgk.Common;

namespace C3.Communi
{
    public class TypeBase
    { 
        internal TypeBase(string name, string text, string description, Type type)
        {
            this.Name = name;
            this.Text = text;
            this.Description = description;
            this.Type = type;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.Text;
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
        #region Type
        /// <summary>
        /// 
        /// </summary>
        public Type Type
        {
            get
            {
                return _type;
            }
            private set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Type");
                }
                _type = value;
            }
        } private Type _type;
        #endregion //Type

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
            private
            set
            {
                _name = value;
            }
        } private string _name;
        #endregion //Name
    }

    public class StationType : TypeBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        internal StationType(string name, Type type)
            : this(name, null, null, type)
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="text"></param>
        /// <param name="type"></param>
        internal StationType(string name, string text, Type type)
            : this(name, text, null, type)
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        internal StationType(string name, string text, string description, Type type)
            : base(name, text, description, type)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IStation Create()
        {
            object obj = Activator.CreateInstance(this.Type);
            IStation st = (IStation)obj;
            st.StationType = this;
            return st;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class DeviceType : TypeBase 
    {
        #region DeviceType

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        internal DeviceType(string name, Type type)
            : this(name, null, null, type)
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="text"></param>
        /// <param name="type"></param>
        internal DeviceType(string name, string text, Type type)
            : this(name, text, null, type)
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        internal DeviceType(string name, string text, string description, Type type)
            : base(name, text, description, type)
        {
        }
        #endregion //DeviceType


        #region Kind
        /// <summary>
        /// 
        /// </summary>
        private DeviceKind Kind
        {
            get
            {
                //if (_kind == null)
                //{
                //    _kind = new DeviceKind();
                //}
                return _kind;
            }
            set
            {
                _kind = value;
            }
        } private DeviceKind _kind;
        #endregion //Kind

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IDevice Create()
        {
            IDevice d = (IDevice)Activator.CreateInstance(this.Type);
            d.DeviceType = this;

            return d;
        }
    }
}
