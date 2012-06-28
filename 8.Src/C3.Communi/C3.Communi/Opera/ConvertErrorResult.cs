using System;
using Xdgk.Common;

namespace C3.Communi
{
    /*
    public class ConvertErrorResult : ParseResultBase
    {
        public ConvertErrorResult(byte[] actual, string converterName, Exception ex)
        {
            this._actual = actual;
            _convertException = ex;

        }

        /// <summary>
        /// 
        /// </summary>
        public Exception ConvertException
        {
            get { return _convertException; }
        } private Exception _convertException;
        /// <summary>
        /// 
        /// </summary>
        public byte[] Actual
        {
            get { return _actual; }
        } private byte[] _actual;

        public string ConverterName
        {
            get { return _converterName; }
        } private string  _converterName;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string s = string.Format("{0}, {1} '{2}', {3} '{4}', {5} '{6}'",
                    strings.ConvertErrorResult,
                    strings.Name, _converterName,
                    strings.Converter, this.DataField.BytesConverter.ToString(),
                    strings.Value, BitConverter.ToString(Actual));
            return s;
        }
    }
    */

}
