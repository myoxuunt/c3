using System;
using Xdgk.Common;

namespace C3.Communi
{
    abstract public class OperaBase : IOpera
    {

        #region CreateSendBytes
        /// <summary>
        /// 
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        public byte[] CreateSendBytes(IDevice device)
        {
            byte[] bytes = OnCreateSendBytes(device);

            return bytes;
        }
        #endregion //CreateSendBytes

        #region OnCreateSendBytes
        /// <summary>
        /// 
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        abstract public byte[] OnCreateSendBytes(IDevice device);
        #endregion //OnCreateSendBytes

        #region ParseReceivedBytes
        /// <summary>
        /// 
        /// </summary>
        /// <param name="device"></param>
        /// <param name="received"></param>
        /// <returns></returns>
        public IParseResult ParseReceivedBytes(IDevice device, byte[] received)
        {
            IParseResult pr = OnParseReceivedBytes(device, received);
            return pr;
        }
        #endregion //ParseReceivedBytes

        #region OnParseReceivedBytes
        /// <summary>
        /// 
        /// </summary>
        /// <param name="device"></param>
        /// <param name="received"></param>
        /// <returns></returns>
        abstract public IParseResult OnParseReceivedBytes(IDevice device, byte[] received);
        #endregion //OnParseReceivedBytes

        #region Name
        /// <summary>
        /// 
        /// </summary>
        public string Name
        {
            get
            {
                if (_name == null)
                {
                    _name = string.Empty;
                }
                return _name;
            }
            set
            {
                _name = value;
            }
        } private string _name;
        #endregion //Name

        #region Text
        /// <summary>
        /// 
        /// </summary>
        public string Text
        {
            get
            {
                if (_text == null)
                {
                    _text = string.Empty;
                }
                return _text;
            }
            set
            {
                _text = value;
            }
        } private string _text;
        #endregion //Text

        #region ReceiveParts
        /// <summary>
        /// 
        /// </summary>
        public ReceivePartCollection ReceiveParts
        {
            get
            {
                if (_receivePartCollection == null)
                    _receivePartCollection = new ReceivePartCollection();
                return _receivePartCollection;
            }
            set
            {
                this._receivePartCollection = value;
            }
        } private ReceivePartCollection _receivePartCollection;
        #endregion //ReceiveParts

        #region SendPart
        /// <summary>
        /// 
        /// </summary>
        public SendPart SendPart
        {
            get { return _sendPart; }
            set { _sendPart = value; }
        } private SendPart _sendPart;
        #endregion //

    }

}
