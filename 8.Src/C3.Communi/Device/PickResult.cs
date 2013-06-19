using System;
using System.Collections.Generic;
using System.Text;

namespace C3.Communi
{
    public class PickResult
    {
        /// <summary>
        /// 
        /// </summary>
        public bool IsSuccess
        {
            get { return _isSuccess; }
            set { _isSuccess = value; }
        } private bool _isSuccess;


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
            set { _remain = value; }
        } private byte[] _remain;
                

        /// <summary>
        /// 
        /// </summary>
        public IParseResult ParseResult
        {
            get { return _parseResult; }
            set { _parseResult = value; }
        }
        private IParseResult _parseResult;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="parseResult"></param>
        /// <param name="remain"></param>
        /// <returns></returns>
        static public PickResult CreateSuccessPickResult(string name, IParseResult pr, byte[] remain)
        {
            PickResult r = new PickResult();
            r.IsSuccess = true;
            //r.Name = name;
            r.Remain = remain;
            //r.Results = keyValues;
            r.ParseResult = pr;
            return r;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="remain"></param>
        /// <returns></returns>
        static public PickResult CreateFailPickResult(byte[] remain)
        {
            PickResult r = new PickResult();
            r.IsSuccess = false;
            r.Remain = remain;
            return r;
        }
    }
}
