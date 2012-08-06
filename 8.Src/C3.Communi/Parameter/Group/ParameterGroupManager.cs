using System;
using System.Collections.Generic;
using System.Text;
using Xdgk.Common;
using System.Diagnostics;


namespace C3.Communi
{
    public class ParameterGroupManager
    {
        /// <summary>
        /// 
        /// </summary>
        private ParameterGroupManager()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        static public ParameterGroupCollection Groups
        {
            get
            {
                if (_groups == null)
                {
                    _groups = new ParameterGroupCollection();
                }
                return _groups;
            }
        } static private ParameterGroupCollection _groups;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        static ParameterGroup GetGroup(string name)
        {
            ParameterGroup r = null;
            foreach (ParameterGroup item in Groups)
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
