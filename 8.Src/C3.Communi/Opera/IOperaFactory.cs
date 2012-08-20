using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using Xdgk.Common;
using Xdgk.Communi.Interface;

namespace C3.Communi
{
    public interface IUploadParser
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="device"></param>
        /// <param name="bs"></param>
        /// <returns></returns>
         UploadParseResult Parse(IDevice device, byte[] bs);
    }

    /// <summary>
    /// 
    /// </summary>
    public class DataFieldUploadParser : IUploadParser
    {
        public DataFieldUploadParser(ReceivePart rp)
        {
            if (rp == null)
            {
                throw new ArgumentNullException("rp");
            }
            this._receivePard = rp;
        }

        private ReceivePart _receivePard;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="device"></param>
        /// <param name="bs"></param>
        /// <returns></returns>
        public UploadParseResult Parse(IDevice device, byte[] bs)
        {
            int length = 0;
            if (bs != null)
            {
                length = bs.Length;
            }

            if (length < _receivePard.DataFieldManager.Length)
            {
                return UploadParseResult.CreateFailUploadParseResult(bs);
            }

            for (int i = 0; i < bs.Length - _receivePard.DataFieldManager.Length; i++)
            {
                byte[] bsForParse = new byte[_receivePard.DataFieldManager.Length];
                Array.Copy(bs, i, bsForParse, 0, _receivePard.DataFieldManager.Length);

                IParseResult pr = _receivePard.ToValues(bsForParse);
                if (pr.IsSuccess)
                {
                    byte[] remain = Remove(bs, i, _receivePard.DataFieldManager.Length);
                    UploadParseResult success = UploadParseResult.CreateSuccessUploadParseResult(
                        _receivePard.Name, pr.Results, remain);
                }
            }

            return UploadParseResult.CreateFailUploadParseResult(bs);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="begin"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        private byte[] Remove(byte[] source, int begin, int length)
        {
            Debug.Assert(source.Length >= begin + length);

            byte[] r = new byte[source.Length - length];
            Array.Copy(source, 0, r, 0, begin);
            Array.Copy(source, begin + length, r, begin, length);
            return r;
        }
    }

    public class UploadParserCollection : Collection<IUploadParser>
    {
    }

    /// <summary>
    /// 
    /// </summary>
    public interface IOperaFactory
    {
        IOpera Create(string deviceType, string operaName);
        UploadParserCollection CreateUploadParsers();
    }
}