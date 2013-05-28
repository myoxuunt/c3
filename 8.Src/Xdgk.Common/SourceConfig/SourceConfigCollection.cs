using System;
using System.Xml;
using System.Collections.Generic;
using System.Text;
using Xdgk.Common;

namespace Xdgk.Common
{
    public class SourceConfigManager
    {
        /// <summary>
        /// 
        /// </summary>
        private SourceConfigManager()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        static public SourceConfigCollection SourceConfigs
        {
            get
            {
                if (_sourceConfigs == null)
                {
                    _sourceConfigs = ReadSourceConfigs();
                }
                return _sourceConfigs;
            }
        } static private SourceConfigCollection _sourceConfigs;

        #region ReadSourceConfigs
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        static private SourceConfigCollection ReadSourceConfigs()
        {
            string path = System.Windows.Forms.Application.StartupPath + "\\Config\\Source.xml";

            SourceConfigCollection sourceConfigs = new SourceConfigCollection();
            XmlDocument doc = new XmlDocument();
            doc.Load(path);

            XmlNode sourcesNode = doc.SelectSingleNode("sources");
            foreach (XmlNode item in sourcesNode.ChildNodes)
            {
                XmlElement sourceNode = item as XmlElement;
                string key = sourceNode["key"].ChildNodes[0].Value;
                string value = sourceNode["value"].ChildNodes[0].Value;

                SourceConfig sourceConfig = new SourceConfig(key, value);
                sourceConfigs.Add(sourceConfig);
            }

            return sourceConfigs;
        }
        #endregion //ReadSourceConfigs
    }

    public class SourceConfigCollection : Collection<SourceConfig>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public SourceConfig Find(string key)
        {
            SourceConfig r = null ;
            foreach ( SourceConfig item in this )
            {
                if (StringHelper.Equal(item.Key, key))
                {
                    r = item;
                    break;
                }
            }
            return r ;
        }
    }

}
