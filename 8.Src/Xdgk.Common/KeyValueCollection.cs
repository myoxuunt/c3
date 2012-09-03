using System;
using Xdgk.Common;

//namespace C3.Communi
namespace Xdgk.Common
{
    public class KeyValueCollection : Collection<KeyValue>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="item"></param>
        protected override void InsertItem(int index, KeyValue item)
        {
            KeyValue kv = Find(item.Key);
            if (kv != null)
            {
                string msg = string.Format("exist key '{0}'", kv.Key);
                throw new InvalidOperationException(msg);
            }
            base.InsertItem(index, item);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public KeyValue Find(string key)
        {
            KeyValue result = null;
            foreach (KeyValue kv in this)
            {
                if (StringHelper.Equal(key, kv.Key))
                {
                    result = kv;
                    break;
                }
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public object this[string key]
        {
            get
            {
                KeyValue kv = Find(key);
                if (kv != null)
                {
                    return kv.Value;
                }
                else
                {
                    throw new ArgumentException(string.Format("not find value by key '{0}'", key));
                }

            }
            set
            {
                KeyValue kv = Find(key);
                if (kv != null)
                {
                    kv.Value = value;
                }
                else
                {
                    kv = new KeyValue(key, value);
                    this.Add(kv);
                }
            }
        }
    }

}
