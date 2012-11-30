using System;
using System.Collections.Generic;
using System.Text;

namespace C3.Communi
{

    /// <summary>
    /// 
    /// </summary>
    public interface ITag
    {
        object Tag { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class DeviceKindAttribute : System.Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        public DeviceKindAttribute(string name)
        {
            this.Name = name;
        }

        #region Name
        /// <summary>
        /// 
        /// </summary>
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (value == null || value.Trim ().Length == 0)
                {
                    throw new ArgumentException("Name cannot null or empty");
                }
                _name = value;
            }
        } private string _name;
        #endregion //Name

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
    }
}
