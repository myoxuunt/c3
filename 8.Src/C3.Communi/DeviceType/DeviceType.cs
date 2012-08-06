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
        internal DeviceType(string name)
        {
            this.Name = name;
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
            private set
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
        public DeviceKind Kind
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

    }
}
