using System;
using System.Text.RegularExpressions;
using Xdgk.Common;

namespace C3.Communi
{
    /// <summary>
    /// 
    /// </summary>
    public class RegexFilter : IFilter
    {

        #region Members
        private Regex _regex;
        private string _replacement = string.Empty;
        #endregion //Members

        #region RegexFilter
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pattern"></param>
        public RegexFilter(string pattern)
            : this(string.Empty, pattern, string.Empty)
        {

        }
        #endregion //RegexFilter

        #region RegexFilter
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pattern"></param>
        public RegexFilter(string name, string pattern, string replacement)
        {
            this.Name = name;
            this.Pattern = pattern;
            this.Replacement = replacement;

            RegexOptions options = RegexOptions.IgnoreCase;
            _regex = new Regex(this.Pattern, options);
        }
        #endregion //RegexFilter

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
            set { _name = value; }
        } private string _name;
        #endregion //Name

        /// <summary>
        /// 
        /// </summary>
        public string Replacement
        {
            get
            {
                if (_replacement == null)
                {
                    _replacement = string.Empty;
                }
                return _replacement;
            }
            set { _replacement = value; }
        }

        #region Pattern
        /// <summary>
        /// 
        /// </summary>
        public string Pattern
        {
            get
            {
                if (_pattern == null)
                {
                    _pattern = string.Empty;
                }
                return _pattern;
            }
            set { _pattern = value; }
        } private string _pattern;
        #endregion //Pattern

        #region Filtrate
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public byte[] Filtrate(byte[] source)
        {
            string strTemp = (string)HexStringConverter.Default.ConvertToObject(source);
            strTemp = Filtrate(strTemp);

            byte[] bs = null;
            try
            {
                bs = HexStringConverter.Default.ConvertToBytes(strTemp);
            }
            catch (Exception ex)
            {
                ErrorManager em = SoftManager.GetSoft().ErrorManager;
                
            }
            return bs;
        }
        #endregion //Filtrate

        #region Filtrate
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public string Filtrate(string source)
        {
            bool isMatch = _regex.IsMatch(source);
            if (isMatch)
            {
                source = _regex.Replace(source, _replacement);
            }
            return source;
        }
        #endregion //Filtrate
    }
}
