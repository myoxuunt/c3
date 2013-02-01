using System.Xml;
using Xdgk.Common;

namespace C3.Communi
{
    public class FilterFactory
    {
        static public FilterCollection CreateFromConfigFile()
        {
            FilterCollection r = new FilterCollection ();
            string path = PathUtils.CpFilterConfigFileName;
            XmlDocument doc = new XmlDocument();
            doc.Load(path);
            XmlNode filtersNode = doc.SelectSingleNode("filters");
            if ( filtersNode != null )
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
