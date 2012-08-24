using System;
using System.Collections.Generic;
using System.Text;

namespace C3
{
    /// <summary>
    /// 
    /// </summary>
    public class Config
    {
        #region AppName
        /// <summary>
        /// 
        /// </summary>
        public string AppName
        {
            get
            {
                if (_appName == null)
                {
                    _appName = string.Empty;
                }
                return _appName;
            }
            set
            {
                _appName = value;
            }
        } private string _appName;
        #endregion //AppName
    }
}
