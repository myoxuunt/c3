using System;
using System.Reflection;
using System.Collections.Generic;
using System.Text;

namespace Xdgk.Common
{
    /// <summary>
    /// 
    /// </summary>
    public class ReflectionHelper
    {
        private ReflectionHelper()
        {

        }

        static public object GetPropertyValue(object obj, string propertyName)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }

            if (string.IsNullOrEmpty(propertyName))
            {
                throw new ArgumentException("propertyName");
            }

            Type type = obj.GetType();
            PropertyInfo pi = type.GetProperty(propertyName);
            if (pi == null)
            {
                string msg = string.Format("not find property by name '{0}' from type '{1}'",
                    propertyName, obj.GetType().Name);
                throw new ArgumentException(msg);
            }
            object r = pi.GetValue(obj, null);
            return r;
        }

        static public bool HasProperty(object obj, string propertyName)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }

            if (string.IsNullOrEmpty(propertyName))
            {
                throw new ArgumentException("propertyName");
            }


            Type type = obj.GetType();
            PropertyInfo pi = type.GetProperty(propertyName);
            return pi != null;
        }
    }


}