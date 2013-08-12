using System;
using System.Collections.Generic;
using System.Text;

namespace C3.Communi
{
    /// <summary>
    /// 
    /// </summary>
    public class NullCommuniPortConfig : CommuniPortConfigBase
    {
        /// <summary>
        /// 
        /// </summary>
        private NullCommuniPortConfig()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        static public readonly NullCommuniPortConfig Default = new NullCommuniPortConfig();

        /// <summary>
        /// 
        /// </summary>
        override public bool CanCreate
        {
            get { return false; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        override public ICommuniPort Create()
        {
            string s = string.Format(
                "can not create ICommuniPort from '{0}'",
                this.GetType().Name
                );

            throw new InvalidOperationException(s);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cp"></param>
        /// <returns></returns>
        override public bool IsMatch(ICommuniPort cp)
        {
            return false;
        }
    }
}
