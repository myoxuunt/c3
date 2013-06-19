using System;
using System.Diagnostics;
using System.Reflection;
using System.Collections.Generic;
using System.Text;

namespace Xdgk.Common
{
    /// <summary>
    /// 
    /// </summary>
    public class EnumTextAttribute : System.Attribute
    {
        #region EnumTextAttribute
        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        public EnumTextAttribute(string text)
        {
            this.Text = text;
        }
        #endregion //EnumTextAttribute

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
                    _text = string.Empty;
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
        /// <param name="enumValue"></param>
        /// <returns></returns>
        static public string GetEnumTextAttributeValue(object enumValue)
        {
            if ( enumValue == null )
            {
                throw new ArgumentNullException ("enumValue");
            }
            //Console.WriteLine(enumValue);

            Type type = enumValue.GetType();

            if (!type.IsEnum)
            {
                throw new ArgumentException(
                    string.Format("Type '{0}' is not enum", 
                    enumValue.GetType().Name));
            }
            FieldInfo fi = type.GetField(enumValue.ToString());

            if (fi != null)
            {
                object[] atts = fi.GetCustomAttributes(typeof(EnumTextAttribute), false);
                if (atts.Length > 0)
                {
                    EnumTextAttribute etAtt = (EnumTextAttribute)atts[0];
                    return etAtt.Text;
                }
            }

            return enumValue.ToString();
        }
    }
}
