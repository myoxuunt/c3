using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace C3
{
    /// <summary>
    /// 
    /// </summary>
    public class Config
    {
        public Config()
        {
            this._appName = ConfigurationManager.AppSettings["AppName"];

            string showAddinMenu = ConfigurationManager.AppSettings["ShowAddinMenu"];
            if (showAddinMenu != null)
            {
                bool result = false;
                if (bool.TryParse(showAddinMenu, out result))
                {
                    this._showAddinMenu = result;
                }
            }

            this._version = ConfigurationManager.AppSettings["Version"];

        }

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


        #region ShowAddinMenu
        /// <summary>
        /// 
        /// </summary>
        public bool ShowAddinMenu
        {
            get
            {
                return _showAddinMenu;
            }
            set
            {
                _showAddinMenu = value;
            }
        } private bool _showAddinMenu = false;
        #endregion //ShowAddinMenu


    }
}
