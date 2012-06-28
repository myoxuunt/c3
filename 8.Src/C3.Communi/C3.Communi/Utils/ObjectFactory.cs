using System;
using System.Reflection;

namespace C3.Communi
{
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
