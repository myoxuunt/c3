using System;
using System.Collections.Generic;
using System.Text;
using C3.Communi.Resources;

namespace C3.Communi
{
    /// <summary>
    /// 
    /// </summary>
    public class CommuniDetail : CommuniDetailBase
    {
        public IParseResult ParseResult
        {
            get { return _parseResult; }
        }
        private IParseResult _parseResult;

        #region CommuniDetail
        /// <summary>
        /// 
        /// </summary>
        /// <param name="opera"></param>
        /// <param name="parseResult"></param>
        /// <param name="send"></param>
        /// <param name="received"></param>
        public CommuniDetail(string operaText,
            byte[] send, DateTime sendDateTime, 
            byte[] received, DateTime receivedDateTime, 
            IParseResult parseResult)
            : base(operaText)
        {
            if (parseResult == null)
            {
                throw new ArgumentNullException("parseResult");
            }

            this._sendDateTime = sendDateTime;
            this._receivedDateTime = receivedDateTime;
            this._send = send;
            this._received = received;

            this._parseResult = parseResult;
        }
        #endregion //CommuniDetail

        #region SendDateTime
        /// <summary>
        /// 
        /// </summary>
        public DateTime SendDateTime
        {
            get { return _sendDateTime; }
        } private DateTime _sendDateTime;
        #endregion //SendDateTime

        #region ReceivedDateTime
        /// <summary>
        /// 
        /// </summary>
        public DateTime ReceivedDateTime
        {
            get { return _receivedDateTime; }
        } private DateTime _receivedDateTime;
        #endregion //ReceivedDateTime


        #region ParseResult
        /// <summary>
        /// 
        /// </summary>
        public string ParseResultText
        {
            get
            {
                return _parseResult.ToString();
            }
        } 
        #endregion //ParseResult

        #region Send
        /// <summary>
        /// 
        /// </summary>
        public byte[] Send
        {
            get
            {
                if (_send == null)
                {
                    _send = new byte[0];
                }
                return _send;
            }
        } private byte[] _send;
        #endregion //Send

        #region Received
        /// <summary>
        /// 
        /// </summary>
        public byte[] Received
        {
            get
            {
                if (_received == null)
                {
                    _received = new byte[0];
                }
                return _received;
            }
        } private byte[] _received;
        #endregion //Received

        #region IsSuccess
        /// <summary>
        /// 
        /// </summary>
        override public bool IsSuccess
        {
            get
            {
                return _parseResult.IsSuccess;
            }
        } 
        #endregion //IsSuccess

        #region Tag
        /// <summary>
        /// 
        /// </summary>
        public object Tag
        {
            get { return _parseResult.Tag; }
        }
        #endregion //Tag

        #region GetReport
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetReport()
        {
            StringBuilder sb = new StringBuilder();
            string _splitString = C3.Communi.Resources.CommuniDetailResource.SplitString;

            sb.AppendLine(CommuniDetailResource.SendDateTime + _splitString + this.SendDateTime.ToString() + " => " + this.ReceivedDateTime.ToString());
            sb.AppendLine(CommuniDetailResource.Opera + _splitString + this.OperaText);
            sb.AppendLine(CommuniDetailResource.Result + _splitString + this.ParseResult);
            sb.AppendLine(CommuniDetailResource.Sended + _splitString + GetBytesString(this.Send));
            sb.AppendLine(CommuniDetailResource.Received + _splitString + GetBytesString(this.Received));

            if (this._parseResult.Tag != null)
            {
                sb.AppendLine(CommuniDetailResource.Tag + _splitString + this._parseResult.Tag);
            }
            return sb.ToString();
        }
        #endregion //GetReport

  

        #region ToString
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return GetReport();
        }
        #endregion //ToString
    }
}
