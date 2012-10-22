using System;
using System.Collections.Generic;
using System.Text;

namespace Xdgk.Common
{
    public class StringStringDictionary : Dictionary<string, string>
    {

    }

    public class StringStringDictionaryConverter 
    {
        public const char KeyValueSplitChar = '=';
        public const char ItemSplitChar = ';';
        /// <summary>
        /// 
        /// </summary>
        private StringStringDictionaryConverter()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        static public StringStringDictionary Parse(string s)
        {
            StringStringDictionary  ht = new StringStringDictionary();
            string[] pairs = s.Split(ItemSplitChar);
            foreach (string pair in pairs)
            {
                string[] kv = pair.Split(KeyValueSplitChar);
                if (kv.Length == 2)
                {
                    string k = kv[0].Trim ();
                    string v = kv[1].Trim ();

                    if (k.Length > 0)
                    {
                        ht[k] = v;
                    }
                }
            }
            return ht;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        static public string ToString(StringStringDictionary parameterHashtable)
        {
            string s = string.Empty;
            foreach ( string key in parameterHashtable.Keys )
            {
                s += string.Format("{0}={1};", key, parameterHashtable[key]);
            }
            return s;
        }
    }

}
