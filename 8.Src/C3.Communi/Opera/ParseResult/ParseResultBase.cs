
using System;
using Xdgk.Common;

namespace C3.Communi
{
    abstract public class ParseResultBase : IParseResult
    {
        #region IsSuccess
        /// <summary>
        /// 
        /// </summary>
        public bool IsSuccess
        {
            get
            {
                return _isSuccess;
            }
            set
            {
                _isSuccess = value;
            }
        } private bool _isSuccess;
        #endregion //IsSuccess

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

        #region Results
        /// <summary>
        /// 
        /// </summary>
        public KeyValueCollection Results
        {
            get
            {
                if (_results == null)
                {
                    _results = new KeyValueCollection();
                }
                return _results;
            }
            set
            {
                _results = value;
            }
        } private KeyValueCollection _results;
        #endregion //Results

        #region ParseBytes
        public byte[] ParseBytes
        {
            get
            {
                if (_parseBytes == null)
                {
                    _parseBytes = new byte[0];
                }
                return _parseBytes;
            }
            set
            {
                _parseBytes = value;
            }
        } private byte[] _parseBytes;
        #endregion //ParseBytes

    }
}
