using System;
using System.Configuration;
using System.Collections.Generic;
using System.Windows.Forms;
using Xdgk.Common;
using C3.Communi;


namespace S
{
    public class Config
    {
        static public Config Default
        {
            get
            {
                if (_default == null)
                {
                    _default = new Config();
                    _default._appName = ConfigurationManager.AppSettings["AppName"];
                    _default._version = ConfigurationManager.AppSettings["Version"];
                }
                return _default;
            }
        } static private Config _default;

        #region Version
        /// <summary>
        /// 
        /// </summary>
        public string Version
        {
            get
            {
                if (_version == null)
                {
                    _version = string.Empty;
                }
                return _version;
            }
            set
            {
                _version = value;
            }
        } private string _version;
        #endregion //Version

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
