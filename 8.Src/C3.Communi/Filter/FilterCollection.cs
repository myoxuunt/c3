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
        public byte[] Filt(byte[] source)
        {
            if (this.Count == 0)
            {
                return source;
            }

            byte[] bs = source;
            foreach (IFilter f in this)
            {
                bs = f.Filt(bs);
            }
            return bs;
        }
    }

}
