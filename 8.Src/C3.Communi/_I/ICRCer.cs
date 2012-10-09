
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;


namespace C3.Communi
{
    public interface ICRCer
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="begin"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        byte[] Calc(byte[] bytes, int begin, int length);
    }

}
