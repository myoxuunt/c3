using System;
using Xdgk.Common;

namespace C3.Communi
{
    /// <summary>
    /// ×ª»»´íÎó
    /// </summary>
    public class ConvertErrorResult : ParseResultBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="df"></param>
        public ConvertErrorResult( string receivePartName, byte[] actual, DataField df, Exception ex)
        {
            if (df == null)
                throw new ArgumentNullException("df");
            this._actual = actual;
            this._df = df;
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

        /// <summary>
        /// 
        /// </summary>
        public DataField DataField
        {
            get { return _df; }
        } private DataField _df;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string s = string.Format("{0}, {1} '{2}', {3} '{4}', {5} '{6}'", 
                strings.ConvertErrorResult,
                strings.Name, _df.Name,
                strings.Converter, this.DataField.BytesConverter.ToString(),
                strings.Value, BitConverter.ToString(Actual));
            return s;
        }
    }
   

}
