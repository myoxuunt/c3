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

        #region ReflectionHelper
        /// <summary>
        /// 
        /// </summary>
        private ReflectionHelper()
        {

        }
        #endregion //ReflectionHelper

        #region GetPropertyValue
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
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

            PropertyInfo pi = GetPropertyInfo(obj, propertyName);
            object r = pi.GetValue(obj, null);
            return r;
        }
        #endregion //GetPropertyValue

        #region SetPropertyValue
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        static public void SetPropertyValue(object obj, string propertyName, object value)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }

            if (string.IsNullOrEmpty(propertyName))
            {
                throw new ArgumentException("propertyName");
            }

            PropertyInfo pi = GetPropertyInfo(obj, propertyName);
            pi.SetValue(obj, value, null);
        }
        #endregion //SetPropertyValue

        #region HasProperty
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
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
        #endregion //HasProperty

        #region GetPropertyInfo
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        private static PropertyInfo GetPropertyInfo(object obj, string propertyName)
        {
            Type type = obj.GetType();
            PropertyInfo pi = type.GetProperty(propertyName);
            if (pi == null)
            {
                string msg = string.Format(
                    "not find property by name '{0}' from type '{1}'",
                    propertyName, obj.GetType().Name);
                throw new ArgumentException(msg);
            }
            return pi;
        }
        #endregion //GetPropertyInfo
    }
}
