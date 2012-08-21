using System;
using System.Xml;
using System.IO;
using Xdgk.Common;
using NLog;

namespace C3.Communi
{
    public class OperaDefineFactory
    {
        /// <summary>
        /// 
        /// </summary>
        Logger logger = LogManager.GetCurrentClassLogger();

        #region DeviceDefineCollection
        /// <summary>
        /// 
        /// </summary>
        public OperaDefineCollection DeviceDefineCollection
        {
            get
            {
                if (_deviceDefineCollection == null)
                {
                    _deviceDefineCollection = new OperaDefineCollection();
                }
                return _deviceDefineCollection;
            }
        } private OperaDefineCollection _deviceDefineCollection;
        #endregion //DeviceDefineCollection


        public UploadDefineCollection UploadDefines
        {
            get
            {
                if (_uploadDefines == null)
                {
                    _uploadDefines = new UploadDefineCollection();
                }
                return _uploadDefines;
            }
        } private UploadDefineCollection _uploadDefines;
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

            foreach (XmlNode deviceDefineNode in deviceDefinesNode.ChildNodes)
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
                    OperaDefine dd = new OperaDefine(deviceType, deviceText);

                    dd.OperaDefineNode = deviceDefineNode;
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

                    foreach (XmlNode operaOrUploadNode in deviceDefineNode.ChildNodes)
                    {
                        if (operaOrUploadNode.Name == DeviceDefineNodeNames.UploadDefine)
                        {
                            string uploadName = XmlHelper.GetAttribute ( operaOrUploadNode ,"name", false );
                            UploadDefine ud = new UploadDefine(deviceType, deviceText, uploadName);
                            ud.Node = operaOrUploadNode;
                            this.UploadDefines.Add(ud);
                        }
                    }
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
