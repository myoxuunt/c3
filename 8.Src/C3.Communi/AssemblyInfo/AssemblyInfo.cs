using System;
using System.Reflection;

namespace C3.Communi
{
    /// <summary>
    /// 
    /// </summary>
    public class AssemblyInfo
    {
        #region Path
        /// <summary>
        /// 
        /// </summary>
        public string Path
        {
            get
            {
                if (_path == null)
                {
                    _path = string.Empty;
                }
                return _path;
            }
            set
            {
                _path = value;
            }
        } private string _path;
        #endregion //Path

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public object CreateInstanceWithInterface(Type type)
        {
            object obj = ObjectFactory.CreateWithInterface(this.Path, type);
            return obj;
        }
    }
}
