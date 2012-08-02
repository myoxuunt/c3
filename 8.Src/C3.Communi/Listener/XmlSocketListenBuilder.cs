using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace C3.Communi
{
    public class XmlSocketListenBuilder 
    {

        private string _filename;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        public XmlSocketListenBuilder(string filename)
        {
            _filename = filename;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="soft"></param>
        public void Build(SocketListenerManager socketListenerManager)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(_filename);
            XmlNode node = doc.SelectSingleNode(XmlListenNodeNames.ListenPortCollection);
            if (node != null)
            {
                foreach (XmlNode n in node.ChildNodes)
                {
                    if (n.Name == XmlListenNodeNames.ListenPort)
                    {
                        int port = int.Parse(n.InnerText);
                        SocketListener sckListener = new SocketListener(port);
                        sckListener.Start();
                        socketListenerManager.Add(sckListener);
                    }
                }
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    internal class XmlListenNodeNames
    {
        public const string
            ListenPortCollection = "listenports",
            ListenPort = "listenport";
    }
}
