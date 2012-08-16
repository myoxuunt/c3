using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace C3.Communi
{
    /// <summary>
    /// 
    /// </summary>
    public class CommuniPortConfigSerializer
    {
        [XmlInclude(typeof(NullCommuniPortConfig))]
        [XmlInclude(typeof(SerialCommuniPortConfig))]
        [XmlInclude(typeof(RemoteIPAddressConfig))]
        [XmlInclude(typeof(RemotePortConfig))]
        public class Wrapper
        {
            /// <summary>
            /// 
            /// </summary>
            public object Config
            {
                get { return _config; }
                set { _config = value; }
            } private object _config;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        static public string Serialize(ICommuniPortConfig value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            XmlSerializer er = new XmlSerializer(typeof(Wrapper));
            StringWriter sw = new StringWriter();
            Wrapper w = new Wrapper();
            w.Config = value;

            er.Serialize(sw, w);
            return sw.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        static public ICommuniPortConfig Deserialize(string s)
        {
            XmlSerializer er = new XmlSerializer(typeof(Wrapper));
            
            StringReader sr = new StringReader(s);
            object obj = null;
            try
            {
                obj = er.Deserialize(sr);
            }
            catch (InvalidOperationException invalidEx)
            {
                Console.WriteLine(invalidEx.ToString());
                return NullCommuniPortConfig.Default;
            }

            Wrapper w = obj as Wrapper;
            if (w != null)
            {
                return w.Config as ICommuniPortConfig;
            }
            else
            {
                return NullCommuniPortConfig.Default;
            }
            //return (ICommuniPortConfig)w.Config;
        }

    }
}
