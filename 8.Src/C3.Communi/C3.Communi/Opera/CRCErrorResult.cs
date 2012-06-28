
using System;
using Xdgk.Common;

namespace C3.Communi
{
    public class CRCErrorResult : ParseResultBase
    {
        /// <summary>
        /// 
        /// </summary>
        //public CRCErrorResult(string deviceType, string operaName, byte[] expected, byte[] actual)
        //    : base(deviceType, operaName )
        public CRCErrorResult(byte[] expected, byte[] actual)
        {
            this._expected = expected;
            this._actual = actual;
        }
        /// <summary>
        /// 获取期望值
        /// </summary>
        public byte[] Expected
        {
            get { return _expected; }
        } private byte[] _expected;

        /// <summary>
        /// 获取实际值
        /// </summary>
        public byte[] Actual
        {
            get { return _actual; }
        } private byte[] _actual;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string s = string.Format("{0}, {1} '{2}', {3} '{4}'", strings.CRCErrorResult,
                    strings.Expected, BitConverter.ToString(this.Expected),
                    strings.Actual, BitConverter.ToString(this.Actual));
            return s;
        }
    }

}
