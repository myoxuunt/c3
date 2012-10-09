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
        /// <param name="text"></param>
        /// <param name="type"></param>
        internal StationType( string text, Type type)
            : base( text, type)
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
