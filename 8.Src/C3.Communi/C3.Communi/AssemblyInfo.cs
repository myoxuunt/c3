using System;
using System.Collections;
using System.Reflection;
using System.Xml;
using System.Collections.Generic;
using System.Text;
using Xdgk.Common;

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
                if (obj != null)
                {
                    list.Add(obj);
                }
                else
                {
                    // TODO:
                    //
                }
            }
            return list.ToArray();
        }
    }

    public class AssemblyInfoFactory
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        static public AssemblyInfoCollection CreateFromXml(string path)
        {
            AssemblyInfoCollection r = new AssemblyInfoCollection();
            XmlDocument doc = new XmlDocument();
            doc.Load(path);
            XmlNode ais = doc.SelectSingleNode(AssemblyInfoNodeName.AssemblyInfoCollection);
            if (ais != null)
            {
                XmlNodeList ciList = ais.SelectNodes(AssemblyInfoNodeName.AssemblyInfo);
                foreach (XmlNode ciNode in ciList)
                {
                    string aipath = XmlHelper.GetAttribute(ciNode, AssemblyInfoNodeName.Path);

                    AssemblyInfo item = new AssemblyInfo();
                    item.Path = aipath;
                    r.Add(item);
                }
            }
            return r;
        }

        /// <summary>
        /// 
        /// </summary>
        private class AssemblyInfoNodeName
        {
            /// <summary>
            /// 
            /// </summary>
            private AssemblyInfoNodeName()
            {
            }

            /// <summary>
            /// 
            /// </summary>
            public const string
                Path = "path",
                AssemblyInfoCollection = "assemblyInfoCollection",
                AssemblyInfo = "assemblyInfo";
        }
    }


    public class AssemblyException : Exception 
    {
        public AssemblyException(string msg)
            : base(msg)
        {

        }

        public AssemblyException(string msg, Exception innerException)
            : base(msg, innerException)
        {
        }
    }


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

    public class ObjectFactory
    {
        static public object CreateWithInterface (string path ,Type interfaceType)
        {
            Assembly assembly = null;
            try
            {
                assembly = Assembly.LoadFrom(path);
            }
            catch (Exception ex)
            {
                string msg = string.Format("load assembly exception '{0}'", path);
                AssemblyException ex2 = new AssemblyException(msg, ex);
                throw ex2;
            }

            foreach (Type tp in assembly.GetTypes())
            {
                if (TypeHelper.IsImplementInterface(tp, interfaceType))
                {
                    object obj = Activator.CreateInstance(tp);
                    // TODO: change type
                    //
                    return obj;
                }
            }

            string msg2 = string.Format("not implement ISPU interface in assembly '{0}'", path);
            throw new AssemblyException(msg2);
        }
    }
}
