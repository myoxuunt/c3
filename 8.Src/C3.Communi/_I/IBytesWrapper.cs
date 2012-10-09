
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;


namespace C3.Communi
{
    public interface IBytesWrapper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        byte[] Wrap(byte[] bytes);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        byte[] Unwrap(byte[] bytes);
    }

}
