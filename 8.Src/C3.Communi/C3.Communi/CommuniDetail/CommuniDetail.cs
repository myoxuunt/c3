using System;
using System.Collections.Generic;
using System.Text;

namespace C3.Communi
{
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
        public CommuniDetail(string operaText, string parseResult, byte[] send, byte[] received, ParseResultEnum parseResultEnum)
            : this(DateTime.Now, operaText, parseResult, send, received, parseResultEnum)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="opera"></param>
        /// <param name="parseResult"></param>
        /// <param name="send"></param>
        /// <param name="received"></param>
        public CommuniDetail(DateTime dt, string operaText, string parseResult, byte[] send, byte[] received, ParseResultEnum parseResultEnum)
        {
            this._dt = dt;
            this._operaText = operaText;
            this._parseResult = parseResult;
            this._send = send;
            this._received = received;
            this._parseResultEnum = parseResultEnum;
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime DT
        {
            get { return _dt; }
        } private DateTime _dt = DateTime.Now;

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
        public ParseResultEnum ParseResultEnum
        {
            get { return _parseResultEnum; }
        } private ParseResultEnum _parseResultEnum;

        /// <summary>
        /// 
        /// </summary>
        public bool IsSuccess
        {
            get
            {
                return this.ParseResultEnum == ParseResultEnum.Success;
            }
        }
    }
}
