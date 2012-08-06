using System;
using System.Collections.Generic;
using System.Text;
using Xdgk.Common;
using System.Diagnostics;


namespace C3.Communi
{
    /// <summary>
    /// 
    /// </summary>
    public class GroupCollection : OrderNumberCollection<IGroup>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="create"></param>
        /// <returns></returns>
        public IGroup GetGroup( string name)
        {
            IGroup  r = null;
            foreach (IGroup item in this)
            {
                if (StringHelper.Equal(item.Name, name))
                {
                    r = item;
                    break;
                }
            }

            return r;
        }
    }

}
