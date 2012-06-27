using System;
using System.Collections.Generic;
using System.Text;

namespace C3.Communi
{
    public class TypeHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="targetType"></param>
        /// <param name="interfaceType"></param>
        /// <returns></returns>
        static public bool IsImplementInterface(Type targetType, Type interfaceType)
        {
            Type[] types = targetType.GetInterfaces();
            foreach (Type t in types)
            {
                if (t == interfaceType)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
