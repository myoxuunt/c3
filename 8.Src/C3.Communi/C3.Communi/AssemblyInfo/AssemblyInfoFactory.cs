using System.Xml;
using Xdgk.Common;

namespace C3.Communi
{
    /// <summary>
    /// 
    /// </summary>
    public class AssemblyInfoFactory
    {
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

                    AssemblyInfo item = new AssemblyInfo();
                    item.Path = aipath;
                    r.Add(item);
                }
            }
            return r;
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
