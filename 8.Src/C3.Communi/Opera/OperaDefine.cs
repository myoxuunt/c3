using System;
using System.Xml;
using Xdgk.Common;

namespace C3.Communi
{
    public class OperaDefine
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="text"></param>
        public OperaDefine(string deviceType, string text)
        {
            if (deviceType == null || deviceType.Trim().Length == 0)
                throw new ArgumentNullException("deviceType");

            if (text == null || text.Trim().Length == 0)
                throw new ArgumentNullException("text");

            this.DeviceType = deviceType;
            this.Text = text;
        }

        /// <summary>
        /// 
        /// </summary>
        public string DeviceType
        {
            get { return this._devcieType; }
            set { this._devcieType = value; }
        } private string _devcieType;

        /// <summary>
        /// 
        /// </summary>
        public string Text
        {
            get { return _text; }
            set { _text = value; }
        } private string _text;

        /*
        /// <summary>
        /// 
        /// </summary>
        public ParameterDefineCollection ParameterDefineCollection
        {
        get 
        {
        if (_parameterDefineCollection == null)
        {
        _parameterDefineCollection = new ParameterDefineCollection();
        }
        return _parameterDefineCollection; 
        }
        set { _parameterDefineCollection = value; }
        } private ParameterDefineCollection _parameterDefineCollection;
        */

        /// <summary>
        /// 
        /// </summary>
        public XmlNode OperaDefineNode
        {
            get
            {
                return _operaDefineNode;
            }
            set 
            {
                _operaDefineNode = value;
            }
        } private XmlNode _operaDefineNode;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IOpera CreateOpera(string operaName)
        {
            IOpera opera = MyOperaFactory.Create(
                this.DeviceType,
                    operaName, 
                    this.OperaDefineNode
                    );

            if (opera == null)
            {
                string s = string.Format(
                    "can not create opera with '{0} {1}'",
                    this.DeviceType , operaName);

                throw new InvalidOperationException(s);
            }

            return opera;
        }
    }

    public class UploadDefine
    {
        public UploadDefine(string deviceType, string text, string uploadName )
        {
            this.DeviceType = deviceType;
            this.Text = text;
            this.UploadName = uploadName;
        }

        #region UploadName
        /// <summary>
        /// 
        /// </summary>
        public string UploadName
        {
            get
            {
                if (_uploadName == null)
                {
                    _uploadName = string.Empty;
                }
                return _uploadName;
            }
            set
            {
                _uploadName = value;
            }
        } private string _uploadName;
        #endregion //UploadName

        /// <summary>
        /// 
        /// </summary>
        public string DeviceType
        {
            get { return this._devcieType; }
            set { this._devcieType = value; }
        } private string _devcieType;

        /// <summary>
        /// 
        /// </summary>
        public string Text
        {
            get { return _text; }
            set { _text = value; }
        } private string _text;

        /// <summary>
        /// 
        /// </summary>
        public XmlNode Node 
        {
            get { return _node; }
            set { _node = value; }
        } private XmlNode _node;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IPicker Create(string name)
        {
            IPicker r = MyOperaFactory.CreatePicker(this.DeviceType, this.Node);
            return r;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class UploadDefineCollection : Collection<UploadDefine>
    {

    }

}
