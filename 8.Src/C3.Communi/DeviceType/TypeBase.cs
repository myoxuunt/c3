using System;

namespace C3.Communi
{
    public class TypeBase
    {

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="type"></param>
        internal TypeBase(
            string text, Type type)
        {
            this.Text = text;
            this.Type = type;
        }
        #endregion //Constructor

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
                    _text = this.Type.Name;
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

