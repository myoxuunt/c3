using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

using NUnit.Core;
using Xdgk.Common;
using System.Collections ;

namespace C3.Communi
{
    using Path = System.IO.Path;
    /// <summary>
    /// 
    /// </summary>
    public class BytesConverterManager
    {
        static private Logger log = InternalTrace.GetLogger(typeof(BytesConverterManager));
        static private string BytesConverterAddinDir = "bc";
        private List<Assembly> _assemblyList= new List<Assembly>();

        #region BytesConverters
        /// <summary>
        /// 
        /// </summary>
        public BytesConverterCollection BytesConverters
        {
            get { return _bytesConverterCollection; }
        } private BytesConverterCollection _bytesConverterCollection = new BytesConverterCollection();
        #endregion //BytesConverters

        #region BytesConverterManager
        /// <summary>
        /// 
        /// </summary>
        public BytesConverterManager() 
        {
            RegisterAddins();
        }
        #endregion //BytesConverterManager

        #region RegisterAddins
        /// <summary>
        /// 
        /// </summary>
        private void RegisterAddins()
        {
            // Load any extensions in the addins directory
            string addinDirectory = Path.Combine(Application.StartupPath, BytesConverterAddinDir );
            DirectoryInfo dir = new DirectoryInfo(addinDirectory);

            if (dir.Exists)
            {
                foreach (FileInfo file in dir.GetFiles("*.dll"))
                {
                    Register(file.FullName);
                }
            }
        }
        #endregion //RegisterAddins


        #region Register
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        private void Register(string path)
        {
            try
            {
                AssemblyName assemblyName = new AssemblyName();
                assemblyName.Name = Path.GetFileNameWithoutExtension(path);
                assemblyName.CodeBase = path;
                Assembly assembly = Assembly.Load(assemblyName);
                log.Debug("Loaded " + Path.GetFileName(path));
                this._assemblyList.Add(assembly);

                foreach (Type type in assembly.GetExportedTypes())
                {
                    if (type.IsClass && typeof(IBytesConverter).IsAssignableFrom(type))
                    {
                        IBytesConverter ibc = (IBytesConverter)Activator.CreateInstance(type);
                        this._bytesConverterCollection.Add( ibc );
                        //log.Error("Addin {0} was already registered", addin.Name);
                        //else
                        //{
                        //    addinRegistry.Register(addin);
                        log.Debug("Registered addin: {0}", ibc.GetType());
                        //}
                    }
                }
            }
            catch (Exception ex)
            {
                // NOTE: Since the gui isn't loaded at this point, 
                // the trace output will only show up in Visual Studio
                log.Error("Failed to load" + path, ex);
            }
        }
        #endregion //Register


        #region GetType
        /// <summary>
        /// 
        /// </summary>
        /// <param name="typeName"></param>
        /// <returns></returns>
        private Type GetType(string typeName)
        {
            foreach (Assembly a in _assemblyList)
            {
                Type t = a.GetType(typeName);
                if (t != null)
                    return t;
            }
            return null;
        }
        #endregion //GetType


        #region CreateBytesConverter
        /// <summary>
        /// 
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public IBytesConverter CreateBytesConverter(string typeName, object[] args)
        {
            Type t = this.GetType(typeName);
            if (t != null)
            {
                return (IBytesConverter)Activator.CreateInstance(t, args);
            }
            else
            {
                //return null;
                string s = string.Format("Cannot create bytes converter by '{0}'", typeName);
                throw new ArgumentException(s);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cfg"></param>
        /// <returns></returns>
        public IBytesConverter CreateBytesConverter(BytesConverterConfig cfg)
        {
            if (cfg == null)
            {
                throw new ArgumentNullException("cfg");
            }

            IBytesConverter bc = CreateBytesConverter(cfg.Name, null);
            foreach (object key in cfg.Propertys.Keys)
            {
                string value = cfg.Propertys[key].ToString();
                SetValue(bc, key.ToString(), value);
            }

            if (cfg.HasInner)
            {
                IBytesConverter innerBc = CreateBytesConverter(cfg.InnerBytesConverterConfig);
                bc.InnerBytesConverter = innerBc;
            }

            return bc;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        private void SetValue(object obj, string propertyName, string value)
        {
            bool isSet = false;
            PropertyInfo[] pis = obj.GetType().GetProperties();
            foreach (PropertyInfo pi in pis)
            {
                if (StringHelper.Equal(pi.Name, propertyName))
                {
                    object objValue = Convert.ChangeType(value, pi.PropertyType);
                    pi.SetValue(obj, objValue, null);
                    isSet = true;
                    break;
                }
            }

            if (!isSet)
            {
                string s = string.Format("not set property '{0}'", propertyName);
                throw new ArgumentException(s);
            }
        }

        #endregion //CreateBytesConverter
    }

    public class BytesConverterConfig
    {
        #region Name
        /// <summary>
        /// 
        /// </summary>
        public string Name
        {
            get
            {
                if (_name == null)
                {
                    _name = string.Empty;
                }
                return _name;
            }
            set
            {
                _name = value;
            }
        } private string _name;
        #endregion //Name

        #region HasInner
        /// <summary>
        /// 
        /// </summary>
        public bool HasInner
        {
            get
            {
                return _hasInner;
            }
            set
            {
                _hasInner = value;
            }
        } private bool _hasInner;
        #endregion //HasInner

        #region InnerBytesConverterConfig
        /// <summary>
        /// 
        /// </summary>
        public BytesConverterConfig InnerBytesConverterConfig
        {
            get
            {
                return _innerBytesConverterConfig;
            }
            set
            {
                _innerBytesConverterConfig = value;
            }
        } private BytesConverterConfig _innerBytesConverterConfig;
        #endregion //InnerBytesConverterConfig

        /// <summary>
        /// 
        /// </summary>
        public Hashtable Propertys
        {
            get
            {
                if (_hash == null)
                {
                    _hash = new Hashtable();
                }
                return _hash;
            }
            set { _hash = value; }
        } private Hashtable _hash;
    }
}
