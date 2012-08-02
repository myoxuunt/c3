using System;
using System.Collections.Generic;
using System.Text;

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
        /// <param name="config"></param>
        /// <returns></returns>
        public ICommuniPort Create(ICommuniPortConfig cpConfig)
        {
            if (cpConfig == null)
            {
                throw new ArgumentNullException("cpConfig");
            }

            if (!cpConfig.CanCreate)
            {
                throw new InvalidOperationException("cannot create form communiPortConfig");
            }

            // TODO: create
            //
            ICommuniPort cp = null;
            return cp;

        }
    }
}
