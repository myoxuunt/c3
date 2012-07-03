
using System;
using Xdgk.Common;

namespace C3.Communi
{
    public interface ICommuniPortConfig
    {
        bool CanCreate { get; }
        ICommuniPort Create();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cp"></param>
        /// <returns></returns>
        bool IsMatch(ICommuniPort cp);
    }

    public class NullCommuniPortConfig : ICommuniPortConfig
    {

        /// <summary>
        /// 
        /// </summary>
        static public readonly NullCommuniPortConfig Default = new NullCommuniPortConfig();

        /// <summary>
        /// 
        /// </summary>
        public bool CanCreate
        {
            get { return false; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ICommuniPort Create()
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
        public bool IsMatch(ICommuniPort cp)
        {
            return false;
        }

    }

}
