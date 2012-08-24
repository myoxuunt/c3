using System;
using System.Collections;

namespace C3.Communi
{
    /// <summary>
    /// 
    /// </summary>
    public class AssemblyInfoCollection : Xdgk.Common.Collection<AssemblyInfo>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public object[] CreateInstanceWithInterface(Type type)
        {
            ArrayList list = new ArrayList();
            foreach (AssemblyInfo ai in this)
            {
                object obj = ai.CreateInstanceWithInterface(type);
                list.Add(obj);
            }
            return list.ToArray();
        }
    }
}
