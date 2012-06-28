
using System;
using Xdgk.Common;

namespace C3.Communi
{
    public class DataErrorResult : ParseResultBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        public DataErrorResult(string name, byte[] expected, byte[] actual)
        {
            if (expected == null)
                throw new ArgumentNullException("expected");
            if (actual == null)
                throw new ArgumentNullException("actual");

            this.Name = name;

            _expected = expected;
            _actual = actual;
        }

        public byte[] Expected
        {
            get { return _expected; }
        } private byte[] _expected;

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
        /// <returns></returns>
        public override string ToString()
        {
            string s = string.Format("{0}, {1} '{2}', {3} '{4}'", 
                    strings.DataErrorResult,
                    strings.Expected, BitConverter.ToString(this.Expected),
                    strings.Actual, BitConverter.ToString(this.Actual));
            return s;
        }
    }

}
