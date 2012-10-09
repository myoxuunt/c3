using System.Xml;
using Xdgk.Common;
using NLog;

namespace C3.Communi
{
    /// <summary>
    /// 
    /// </summary>
    public class AssemblyInfoFactory
    {
        static Logger _log = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        static public AssemblyInfoCollection CreateFromXml(string path)
        {
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
        }

        static private string FixPath(string path)
        {
            return PathUtils.MapToStartupPath(path);
        }

        /// <summary>
        /// 
        /// </summary>
        private class AssemblyInfoNodeName
        {
            /// <summary>
            /// 
            /// </summary>
            private AssemblyInfoNodeName()
            {
            }

            /// <summary>
            /// 
            /// </summary>
            public const string
                Path = "path",
                AssemblyInfoCollection = "assemblyInfoCollection",
                AssemblyInfo = "assemblyInfo";
        }
    }
}
