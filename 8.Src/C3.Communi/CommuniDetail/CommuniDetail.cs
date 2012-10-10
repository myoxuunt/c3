using System;
using System.Collections.Generic;
using System.Text;
using C3.Communi.Resources;

namespace C3.Communi
{
    /// <summary>
    /// 
    /// </summary>
    public class CommuniDetail
    {
        #region CommuniDetail
        /// <summary>
        /// 
        /// </summary>
        /// <param name="opera"></param>
        /// <param name="parseResult"></param>
        /// <param name="send"></param>
        /// <param name="received"></param>
        public CommuniDetail(string operaText,
            byte[] send, DateTime sendDateTime, byte[] received, DateTime receivedDateTime, string parseResult, bool isSuccess)
        {
            this._sendDateTime = sendDateTime;
            this._receivedDateTime = receivedDateTime;
            this._operaText = operaText;
            this._parseResult = parseResult;
            this._send = send;
            this._received = received;
            this._isSuccess = isSuccess;
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

        #region OperaText
        /// <summary>
        /// 
        /// </summary>
        public string OperaText
        {
            get
            {
                if (_operaText == null)
                {
                    _operaText = string.Empty;
                }
                return _operaText;
            }
        } private string _operaText;
        #endregion //OperaText

        #region ParseResult
        /// <summary>
        /// 
        /// </summary>
        public string ParseResult
        {
            get
            {
                if (_parseResult == null)
                {
                    _parseResult = string.Empty;
                }
                return _parseResult;
            }
        } private string _parseResult;
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
        public bool IsSuccess
        {
            get { return _isSuccess; }
        } private bool _isSuccess;
        #endregion //IsSuccess

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetReport()
        {
            StringBuilder sb = new StringBuilder();
            string _splitString = C3.Communi.Resources.CommuniDetailResource.SplitString;

            sb.AppendLine(CommuniDetailResource.SendDateTime + _splitString + this.SendDateTime.ToString());
            sb.AppendLine(CommuniDetailResource.Opera + _splitString + this.OperaText);
            sb.AppendLine(CommuniDetailResource.Result + _splitString + this.ParseResult);
            sb.AppendLine(CommuniDetailResource.Sended + _splitString + GetBytesString(this.Send));
            sb.AppendLine(CommuniDetailResource.Received + _splitString + GetBytesString(this.Received));
            //sb.AppendLine();
            return sb.ToString();
        }

        #region GetBytesString
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bs"></param>
        /// <returns></returns>
        private string GetBytesString(byte[] bs)
        {
            if (bs == null || bs.Length == 0)
            {
                return null;
            }

            string s = string.Format("[{0:000}] ", bs.Length) + BitConverter.ToString(bs);
            return s;
        }
        #endregion //GetBytesString
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return GetReport();
        }
    }
}
