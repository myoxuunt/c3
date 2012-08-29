using System;
using System.Collections.Generic;
using System.Text;

namespace Xdgk.Common
{
    public class EnumTextAttribute : System.Attribute 
    {
        public EnumTextAttribute(string text)
        {
            this.Text = text;
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
    }
}
