using System.Xml;
using System.IO;
using Xdgk.Common;

namespace C3.Communi
{
    public class FilterFactory
    {
        static private NLog.Logger log = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        static public FilterCollection CreateFromConfigFile()
        { 
            string path = PathUtils.CpFilterConfigFileName;
            return CreateFromConfigFile(path);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        static public FilterCollection CreateFromConfigFile(string path)
        {
            FilterCollection r = new FilterCollection ();
            if (Directory.Exists(path))
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(path);
                XmlNode filtersNode = doc.SelectSingleNode("filters");
                if (filtersNode != null)
                {
                    XmlNodeList filterNodeList = filtersNode.SelectNodes("filter");
                    foreach (XmlNode node in filterNodeList)
                    {
                        IFilter f = CreateFromFilterNode(node);
                        if (f != null)
                        {
                            r.Add(f);
                        }
                    }
                }
            }
            else
            {
                log.Info("not find cp filter file: {0}", path);
            }
            return r ;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private static IFilter CreateFromFilterNode(XmlNode node)
        {
            string type = string.Empty;
            string pattern = string.Empty;
            string name = string.Empty;
            string replacement = string.Empty;

            foreach( XmlNode cn in node.ChildNodes )
            {
                switch (cn.Name)
                {
                    case "type":
                        break;

                    case "name":
                        name = cn.InnerText;
                        break;

                    case "pattern":
                        pattern = cn.InnerText;
                        break;

                    case "replacement":
                        replacement = cn.InnerText;
                        break;

                    default :
                        break;
                }
            }

            RegexFilter f = null;
            if (!string.IsNullOrEmpty(pattern))
            {
                f = new RegexFilter(name, pattern, replacement);
            }
            return f;
        }
    }

}
