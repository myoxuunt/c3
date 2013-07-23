
using System;
using System.Xml;


namespace Xdgk.Common
{
    public class DGVColumnConfigCollectionFactory
    {
        static public DGVColumnConfig Create(string line)
        {
            string dataPropertyName = string.Empty;
            string format = string.Empty;
            string text = string.Empty;
            int width = 100;
            bool visible = true;

            string[] items = line.Split(';');
            foreach (string item in items)
            {
                if (item.Trim().Length == 0)
                {
                    continue;
                }

                string[] kv = item.Split('=');

                if (kv.Length == 2)
                {
                    string key = kv[0].Trim().ToUpper();
                    string value = kv[1].Trim();

                    switch (key)
                    {
                        case "DATAPROPERTYNAME":
                            dataPropertyName = value;
                            break;

                        case "FORMAT":
                            format = value;
                            break;

                        case "TEXT":
                            text = value;
                            break;

                        case "WIDTH":
                            width = int.Parse(value);
                            break;

                        case "VISIBLE":
                            visible = bool.Parse(value);
                            break;

                        default :
                            throw new FormatException(string.Format("not find key '{0}'", kv[0]));
                    }
                }
                else
                {
                    throw new FormatException(item);
                }
            }
            // dataPropertyName=adfa; b=aaa; c=fdfkdfj;
            DGVColumnConfig r = new DGVColumnConfig(dataPropertyName, format, text);
            r.Width = width;
            r.Visible = visible;
            return r;

        }

        static public DGVColumnConfigCollection Create(string[] lines)
        {
            DGVColumnConfigCollection r = new DGVColumnConfigCollection();
            foreach (string line in lines)
            {
                DGVColumnConfig item = Create(line);
                r.Add(item);
            }
            return r; 
        }

        static public DGVColumnConfigCollection CreateFromXmlString(string xmlString)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlString);
            return CreateFromXml(doc);
        }

        static public DGVColumnConfigCollection CreateFromXml(XmlDocument doc)
        {
            DGVColumnConfigCollection r = new DGVColumnConfigCollection();
            XmlNode root = doc.SelectSingleNode("columns");
            foreach (XmlNode node in root.SelectNodes("column"))
            {
                string dataPropertyName= XmlHelper.GetAttribute(node, "dataPropertyName", false);
                string text = XmlHelper.GetAttribute(node, "text", false);
                string format = XmlHelper.GetAttribute(node, "format", true);
                string strWidth = XmlHelper.GetAttribute(node, "width", true);


                int width = 100 ;
                if ( !string.IsNullOrEmpty (strWidth ))
                {
                    width = int.Parse ( strWidth );
                }
                DGVColumnConfig item = new DGVColumnConfig(
                        dataPropertyName, format, text);
                item.Width = width;
                r.Add(item);
            }
            return r;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        static public DGVColumnConfigCollection CreateFromXml(string path)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(path);
            return CreateFromXml(doc);
        }
    }
}
