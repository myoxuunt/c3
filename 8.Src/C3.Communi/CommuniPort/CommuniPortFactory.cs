using System;

namespace C3.Communi
{
    /// <summary>
    /// 
    /// </summary>
    public class CommuniPortFactory
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cpConfig"></param>
        /// <returns></returns>
        public static ICommuniPort Create(ICommuniPortConfig cpConfig)
        {
            if (cpConfig == null)
            {
                throw new ArgumentNullException("cpConfig");
            }

            if (!cpConfig.CanCreate)
            {
                string s = string.Format(
                    "cannot create form communiPortConfig: '{0}'",
                    cpConfig.ToString());
                throw new InvalidOperationException(s);
            }

            ICommuniPort cp = cpConfig.Create();
            return cp;

        }
    }
}
