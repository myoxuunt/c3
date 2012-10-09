using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using Xdgk.Common;

namespace C3.Communi
{
    //public interface IUploadParser
    //{
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    /// <param name="device"></param>
    //    /// <param name="bs"></param>
    //    /// <returns></returns>
    //     UploadParseResult Parse(IDevice device, byte[] bs);
    //}

    /// <summary>
    /// 
    /// </summary>
    public class DataFieldPicker : IPicker
    {
        public DataFieldPicker(string name, ReceivePart rp)
        {
            if (rp == null)
            {
                throw new ArgumentNullException("rp");
            }
            this.Name = name;
            this._receivePard = rp;
        }

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

        private ReceivePart _receivePard;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="device"></param>
        /// <param name="bs"></param>
        /// <returns></returns>
        public PickResult Pick(IDevice device, byte[] bs)
        {
            int length = 0;
            if (bs != null)
            {
                length = bs.Length;
            }

            if (length < _receivePard.DataFieldManager.Length)
            {
                return PickResult.CreateFailPickResult(bs);
            }

            for (int i = 0; i <= bs.Length - _receivePard.DataFieldManager.Length; i++)
            {
                byte[] bsForParse = new byte[_receivePard.DataFieldManager.Length];
                Array.Copy(bs, i, bsForParse, 0, _receivePard.DataFieldManager.Length);

                IParseResult pr = _receivePard.ToValues(bsForParse);
                if (pr.IsSuccess)
                {
                    byte[] remain = Remove(bs, i, _receivePard.DataFieldManager.Length);
                    PickResult success = PickResult.CreateSuccessPickResult (
                        this.Name, pr, remain);
                    return success;
                }
            }

            return PickResult.CreateFailPickResult(bs);
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
            Array.Copy(source, begin + length, r, begin, source.Length - begin - length);
            return r;
        }
    }

    public interface IOperaFactory
    {
        IOpera Create(string deviceType, string operaName);
        //UploadParserCollection CreateUploadParsers();
        PickerCollection CreatePickers(string deviceType);
    }
}