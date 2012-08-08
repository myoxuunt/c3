using System;
using Xdgk.Common;

namespace C3.Communi
{
    /// <summary>
    /// 
    /// </summary>
    public class DeviceType
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
        {
            this.Name = name;
            this.Text = text;
            this.Description = description;
            this.Type = type;
        }
        #endregion //DeviceType

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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.Text;
        }

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


    }
}
