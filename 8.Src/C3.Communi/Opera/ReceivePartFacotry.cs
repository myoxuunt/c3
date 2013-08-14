
using System;
using System.Diagnostics;
using System.Collections;
using System.Xml;
using Xdgk.Common;
using NLog;


namespace C3.Communi
{
    public class ReceivePartFacotry
    {
        private ReceivePartFacotry()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xmlPath"></param>
        /// <param name="operaName"></param>
        /// <returns></returns>
        static public ReceivePart Create(string xmlPath, string deviceType, string operaName)
        {

            // "/devicedefines/devicedefine[attribute::devicetype='vGate100']/operadefine[attribute::name='read']/receivepart"
            //
            string format = "/{0}/{1}[attribute::{2}='{{0}}']/{3}[attribute::{4}='{{1}}']/{5}";
            string xpath = string.Format(format, 
                    DeviceDefineNodeNames.DeviceDefineCollection,
                    DeviceDefineNodeNames.DeviceDefine,
                    DeviceDefineNodeNames.DeviceType,
                    DeviceDefineNodeNames.OperaDefine,
                    DeviceDefineNodeNames.OperaName,
                    DeviceDefineNodeNames.ReceivePart);

            xpath = string.Format(xpath, deviceType, operaName);

            XmlDocument doc = new XmlDocument();
            doc.Load(xmlPath);

            XmlNodeList list = doc.SelectNodes(xpath);
            Debug.Assert(list.Count > 0);

            XmlNode node = list[0];
            return Create(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="receivePartNode"></param>
        /// <returns></returns>
        static public ReceivePart Create(XmlNode receivePartNode)
        {
            return MyOperaFactory.CreateReceivePart(receivePartNode);
        }
    }

}
