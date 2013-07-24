using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace Xdgk.Common
{
    /// <summary>
    /// 
    /// </summary>
    public class AppConfigReaderForDll
    {
        private string _dllPath;
        private Configuration _cfg;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dllPath"></param>
        public AppConfigReaderForDll(string dllPath)
        {
            this._dllPath = dllPath;
            _cfg = ConfigurationManager.OpenExeConfiguration(_dllPath);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="typeInDestDll"></param>
        public AppConfigReaderForDll(Type typeInDestDll)
        {
            if (typeInDestDll == null)
            {
                throw new ArgumentNullException("typeInDestDll");
            }

            _dllPath = typeInDestDll.Assembly.Location;
            _cfg = ConfigurationManager.OpenExeConfiguration(_dllPath);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string GetSettingValue(string name)
        {
            KeyValueConfigurationElement kv = _cfg.AppSettings.Settings[name];
            if (kv != null)
            {
                return kv.Value;
            }
            else
            {
                return null;
            }
        }
    }

}
