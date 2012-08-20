using System;
using System.Collections.Generic;
using System.Text;

namespace C3.Communi
{
    /*
    /// <summary>
    /// 
    /// </summary>
    public enum ParseResultEnum
    {
        /// <summary>
        /// 
        /// </summary>
        Fail,

        /// <summary>
        /// 
        /// </summary>
        Success,
    }
     */

    /// <summary>
    /// 
    /// </summary>
    public class CommuniDetail
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="opera"></param>
        /// <param name="parseResult"></param>
        /// <param name="send"></param>
        /// <param name="received"></param>
        public CommuniDetail( string operaText, 
            byte[] send, DateTime sendDateTime, byte[] received, DateTime receivedDateTime, string parseResult,bool isSuccess)
        {
            this._sendDateTime = sendDateTime;
            this._receivedDateTime = receivedDateTime;
            this._operaText = operaText;
            this._parseResult = parseResult;
            this._send = send;
            this._received = received;
            this._isSuccess = isSuccess;
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime SendDateTime
        {
            get { return _sendDateTime; }
        } private DateTime _sendDateTime;

        /// <summary>
        /// 
        /// </summary>
        public DateTime ReceivedDateTime
        {
            get { return _receivedDateTime; }
        } private DateTime _receivedDateTime;

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


        /// <summary>
        /// 
        /// </summary>
        public bool IsSuccess
        {
            get { return _isSuccess; }
        } private bool _isSuccess;
    }
}
