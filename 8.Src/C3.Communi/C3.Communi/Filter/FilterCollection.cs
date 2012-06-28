
using System;
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
            //if (this.Count == 0)
            //{
            //    return source;
            //}

            //string temp = (string)HexStringConverter.Default.ConvertToObject(source);
            //foreach (IFilter f in this)
            //{
            //    temp = f.Filt(temp);
            //}
            //byte[] bs = (byte[])HexStringConverter.Default.ConvertToBytes(temp);
            //return bs;
            throw new NotImplementedException();
        }
    }

}
