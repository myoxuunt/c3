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

}
