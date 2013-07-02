
using System;
using System.Collections.Generic;
using System.Text;
using C3.Communi.Resources;


namespace C3.Communi
{
    abstract public class CommuniDetailBase : ICommuniDetail
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="operaText"></param>
        public CommuniDetailBase(string operaText)
        {
            this._operaText = operaText;
        }

#region OperaText
        /// <summary>
        /// 
        /// </summary>
        public string OperaText
        {
            get
            {
                if (_operaText == null)
                {
                    _operaText = string.Empty;
                }
                return _operaText;
            }
        } private string _operaText;
#endregion //OperaText

#region GetBytesString
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bs"></param>
        /// <returns></returns>
        static protected  string GetBytesString(byte[] bs)
        {
            if (bs == null || bs.Length == 0)
            {
                return null;
            }

            string s = string.Format("[{0:000}] ", bs.Length) + BitConverter.ToString(bs);
            return s;
        }
#endregion //GetBytesString

        public virtual bool IsSuccess
        {
            get { return false; }
        }
    }

}
