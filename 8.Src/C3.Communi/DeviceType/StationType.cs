using System;
using Xdgk.Common;


namespace C3.Communi
{
    /// <summary>
    /// 
    /// </summary>
    public class StationType : TypeBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        internal StationType(string name, Type type)
            : this(name, null, null, type)
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="text"></param>
        /// <param name="type"></param>
        internal StationType(string name, string text, Type type)
            : this(name, text, null, type)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        internal StationType(string name, string text, string description, Type type)
            : base(name, text, description, type)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IStation Create(ISPU spu)
        {
            if (spu == null)
            {
                throw new ArgumentNullException("spu");
            }

            object obj = Activator.CreateInstance(this.Type);
            IStation st = (IStation)obj;
            st.Spu = spu;
            st.StationType = this;
            return st;
        }
    }

}
