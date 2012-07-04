
using System;
using Xdgk.Common;

namespace C3.Communi
{
    public class DeviceDefineManager
    {
        /// <summary>
        /// 
        /// </summary>
        NUnit.Core.Logger logger = NUnit.Core.InternalTrace.GetLogger(typeof(DeviceDefineManager));

#region DeviceDefineCollection
        /// <summary>
        /// 
        /// </summary>
        public DeviceDefineCollection DeviceDefineCollection
        {
            get 
            {
                if (_deviceDefineCollection == null)
                {
                    _deviceDefineCollection = new DeviceDefineCollection();
                }
                return _deviceDefineCollection; 
            }
        } private DeviceDefineCollection _deviceDefineCollection;
#endregion //DeviceDefineCollection


#region LoadFromFile
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        public void LoadFromFile(string filename)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filename);

            // 2010-09-10
            // 
            // device defines node is null, file is not devicedefine file
            // 
            XmlNode deviceDefinesNode = doc.SelectSingleNode(DeviceDefineNodeNames.DeviceDefineCollection);

            if (deviceDefinesNode == null)
            {
                logger.Error("load " + filename + " fail");
                return;
            }

            foreach( XmlNode deviceDefineNode in deviceDefinesNode.ChildNodes )
            {
                if (deviceDefineNode.Name == DeviceDefineNodeNames.DeviceDefine)
                {
                    string deviceType = XmlHelper.GetAttribute((XmlElement)deviceDefineNode,
                            DeviceDefineNodeNames.DeviceType);
                    string deviceText = XmlHelper.GetAttribute((XmlElement)deviceDefineNode,
                            DeviceDefineNodeNames.DeviceText);

                    // TODO: remove xmlOperaFactory DeviceDefineCollection
                    //
                    // TODO: 2010-07-28 add device parameter define 
                    //
                    DeviceDefine dd = new DeviceDefine(deviceType, deviceText);

                    dd.DevcieDefineNode = deviceDefineNode;
                    /*
                       XmlNode paramDefinesNode = deviceDefineNode.SelectSingleNode(ParameterNodeNames.ParameterDefineCollection);
                       if (paramDefinesNode != null)
                       {
                       ParameterDefineCollection paramDefines = XmlModbusParameterDefineBuilder.BuildParameterDefineCollection(paramDefinesNode);
                       dd.ParameterDefineCollection = paramDefines;
                       }
                       */

                    this.DeviceDefineCollection.Add(dd);

                    //this.CommuniSoft.DeviceDefineCollection.Add(dd);
                }
            }
        }
#endregion //LoadFromFile


#region LoadFromPath
        /// <summary>
        /// 
        /// </summary>
        /// <param name="directoryName"></param>
        public void LoadFromPath(string path)
        {
            string searchPattern = "*.xml";
            string[] files = Directory.GetFiles(path, searchPattern);

            foreach (string file in files)
            {
                this.LoadFromFile(file);
            }
        }
#endregion //LoadFromPath


    }

}
