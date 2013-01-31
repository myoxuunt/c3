using Xdgk.Common;

namespace C3.Communi
{
    public class FilterCollection : Collection<IFilter>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public byte[] Filtrate(byte[] source)
        {
            if (this.Count == 0)
            {
                return source;
            }

            byte[] bs = source;
            foreach (IFilter f in this)
            {
                bs = f.Filtrate(bs);
            }
            return bs;
        }
    }

}
