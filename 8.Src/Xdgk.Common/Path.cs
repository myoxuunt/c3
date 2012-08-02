using System;
using System.Collections.Generic;
using System.Text;

namespace Xdgk.Common
{
    public class Path
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        static public string GetTempFileName(string extension)
        {
            string tempfilename = System.IO.Path.GetRandomFileName();
            tempfilename = System.IO.Path.GetFileNameWithoutExtension(tempfilename) + "." + extension;
            string temppath = System.IO.Path.GetTempPath();
            string r = System.IO.Path.Combine(temppath, tempfilename);
            return r;
        }
    }}
