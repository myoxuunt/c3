using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Xdgk.Common;

namespace C3.Communi
{

    /// <summary>
    /// 
    /// </summary>
    public interface IParseResult
    {
        bool IsSuccess { get; set; }

        string Name { get; set; }

        KeyValueCollection Results { get; set; }

        byte[] ParseBytes { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public interface IUploadParseResult : IParseResult
    {
        byte[] Remain { get; set; }
    }


    /// <summary>
    /// 
    /// </summary>
    public class UploadParseResult : ParseResultBase , IUploadParseResult 
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="remain"></param>
        /// <returns></returns>
        static public UploadParseResult CreateSuccessUploadParseResult(string name, KeyValueCollection keyValues, byte[] remain)
        {
            UploadParseResult r = new UploadParseResult();
            r.IsSuccess = true;
            r.Name = name;
            r.Remain = remain;
            r.Results = keyValues;
            return r; 
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="remain"></param>
        /// <returns></returns>
        static public UploadParseResult CreateFailUploadParseResult(byte[] remain)
        {
            UploadParseResult r = new UploadParseResult();
            r.IsSuccess = false;
            r.Remain = remain;
            return r;
        }

        /// <summary>
        /// 
        /// </summary>
        private UploadParseResult()
        {
        }

        #region IUploadParseResult 成员

        /// <summary>
        /// 
        /// </summary>
        public byte[] Remain
        {
            get
            {
                if (_remain == null)
                {
                    _remain = new byte[0];
                }
                return _remain;
            }
            set
            {
                _remain = value;
            }
        } private byte[] _remain;

        #endregion
    }
}
