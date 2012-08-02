using System;
using Xdgk.Common;

namespace C3.Communi
{
    public class AddressErrorResult : ParseResultBase
    {
        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="receivePartName"></param>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        public AddressErrorResult(Int64 expected, Int64 actual)
        {
            this._expected = expected;
            this._actual = actual;
        }
        #endregion //Constructor


        #region Expected
        /// <summary>
        /// 
        /// </summary>
        public Int64 Expected
        {
            get { return _expected; }
        } private Int64 _expected;
        #endregion //Expected

        #region Actual
        /// <summary>
        /// 
        /// </summary>
        public Int64 Actual
        {
            get { return _actual; }
        } private Int64 _actual;
        #endregion //Actual

        #region Actual
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string s = string.Format("{0}, {1} '{2}', {3} '{4}'",
                    strings.AddressErrorResult,
                    strings.Expected, this.Expected,
                    strings.Actual, this.Actual);
            return s;
        }
        #endregion //Actual

    }

}
