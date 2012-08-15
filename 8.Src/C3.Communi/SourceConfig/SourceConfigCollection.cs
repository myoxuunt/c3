using System;
using System.Collections.Generic;
using System.Text;
using Xdgk.Common;

namespace C3.Communi
{
    public class SourceConfigCollection : Collection<SourceConfig>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public SourceConfig Find(string key)
        {
            SourceConfig r = null ;
            foreach ( SourceConfig item in this )
            {
                if (StringHelper.Equal(item.Key, key))
                {
                    r = item;
                    break;
                }
            }
            return r ;
        }
    }

}
