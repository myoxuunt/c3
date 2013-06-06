using System;
using System.Collections.Generic;
using System.Text;
using C3.Communi;

namespace Xdgk.Communi
{
    public class Utf8EncodingConverter : BytesConverter
    {
        public override byte[] ConvertToBytes(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            string s = obj.ToString();
            return ASCIIEncoding.UTF8.GetBytes(s);
        }

        public override object ConvertToObject(byte[] bytes)
        {
            if (bytes == null)
            {
                throw new ArgumentNullException("bytes");
            }
            string s = ASCIIEncoding.UTF8.GetString(bytes);
            return s;
        }
    }

}
