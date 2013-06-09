using System;
using System.Collections.Generic;
using System.Text;
using C3.Communi;

namespace Xdgk.Communi
{
    public class Utf8EncodingConverter : BytesConverter
    {
        static private char PAD_Char = ' ';

        /// <summary>
        ///  -1 is not limit
        /// </summary>
        public int BytesCountForConvert
        {
            get { return _bytesCountForConvert; }
            set
            {
                _bytesCountForConvert = value;
                if (_bytesCountForConvert < 0)
                {
                    _bytesCountForConvert = -1;
                }
            }
        } private int _bytesCountForConvert = -1;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override byte[] ConvertToBytes(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }

            byte[] r = null;
            string s = obj.ToString();
            byte[] bs = Encoding.UTF8.GetBytes(s);
            if (_bytesCountForConvert == -1)
            {
                r = bs;
            }
            else
            {
                r = new byte[_bytesCountForConvert];
                for (int i = 0; i < _bytesCountForConvert; i++)
                {
                    r[i] = (byte)PAD_Char;
                }

                Array.Copy(bs, 0, r, 0,
                    _bytesCountForConvert > bs.Length ? bs.Length : _bytesCountForConvert);
            }
            return r;
        }

        public override object ConvertToObject(byte[] bytes)
        {
            if (bytes == null)
            {
                throw new ArgumentNullException("bytes");
            }

            string s = null;
            if (_bytesCountForConvert == -1)
            {
                s = Encoding.UTF8.GetString(bytes);
            }
            else
            {
                if (bytes.Length > _bytesCountForConvert)
                {
                    byte[] temp = new byte[_bytesCountForConvert];
                    Array.Copy(bytes, 0, temp, 0, _bytesCountForConvert);
                    s = Encoding.UTF8.GetString(temp);
                }
                else
                {
                    s = Encoding.UTF8.GetString(bytes);
                }
            }
            return s;
        }
    }

}
