using System;
using System.Xml;
using System.Collections.Generic;
using System.Text;

namespace C3.Communi
{
    internal class HideParameterManager
    {
        private HideParameterManager()
        {

        }

        #region HideParameters
        /// <summary>
        /// 
        /// </summary>
        static internal HideParameterCollection HideParameters
        {
            get
            {
                if (_hideParameters == null)
                {
                    _hideParameters = Load();
                }
                return _hideParameters;
            }
        } static private HideParameterCollection _hideParameters;
        #endregion //HideParameters

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static HideParameterCollection Load()
        {
            /*
            AssemblyInfoCollection r = new AssemblyInfoCollection();
            XmlDocument doc = new XmlDocument();
            doc.Load(path);
            XmlNode ais = doc.SelectSingleNode(AssemblyInfoNodeName.AssemblyInfoCollection);
            if (ais != null)
            {
                XmlNodeList ciList = ais.SelectNodes(AssemblyInfoNodeName.AssemblyInfo);
                foreach (XmlNode ciNode in ciList)
                {
                    string aipath = XmlHelper.GetAttribute(ciNode, AssemblyInfoNodeName.Path);
                    _log.Info("assembly info path: '{0}'", aipath );

                    
                    AssemblyInfo item = new AssemblyInfo();
                    item.Path = FixPath(aipath);
                    _log.Info("after fix path: '{0}'", item.Path );

                    r.Add(item);
                }
            }
            return r;
            */
            HideParameterCollection r = new HideParameterCollection();

            string fileName = PathUtils.HideParameterConfigFileName ;
            XmlDocument doc = new XmlDocument();
            doc.Load(fileName);
            XmlNode rootNode = doc.SelectSingleNode("hps");
            if (rootNode != null)
            {
                foreach (XmlNode node in rootNode)
                {
                    if (node.Name == "hp")
                    {
                        string parameterName = Xdgk.Common.XmlHelper.GetAttribute(node, "name");
                        HideParameter hp = new HideParameter(parameterName);
                        r.Add(hp);
                    }
                }
            }
            return r;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameterName"></param>
        /// <returns></returns>
        static internal bool Include(string parameterName)
        {
            foreach (HideParameter hp in HideParameters)
            {
                if (Xdgk.Common.StringHelper.Equals(hp.ParameterName, parameterName))
                {
                    return true;
                }
            }
            return false;
        }
    }

    internal class HideParameterCollection : Xdgk.Common.Collection<HideParameter>
    {
    }

    internal class HideParameter 
    {
        public HideParameter(string parameterName)
        {
            this.ParameterName = parameterName;
        }
        #region ParameterName
        /// <summary>
        /// 
        /// </summary>
        public string ParameterName
        {
            get
            {
                if (_parameterName == null)
                {
                    _parameterName = string.Empty;
                }
                return _parameterName;
            }
            set
            {
                _parameterName = value;
            }
        } private string _parameterName;
        #endregion //ParameterName
    }
}
