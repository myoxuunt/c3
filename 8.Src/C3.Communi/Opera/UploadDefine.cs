
using System;
using System.Xml;
using Xdgk.Common;


namespace C3.Communi
{
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

}
