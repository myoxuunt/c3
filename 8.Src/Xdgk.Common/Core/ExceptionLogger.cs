using System;
using System.IO;
using System.Windows.Forms;

namespace Xdgk.Common
{
    internal class ExceptionLogger
    {
        /// <summary>
        /// 
        /// </summary>
        private ExceptionLogger()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ex"></param>
        static internal void Save(Exception ex)
        {
            string s = DateTime.Now.ToString() + Environment.NewLine + ex.ToString();
            Save(s);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exmsg"></param>
        static internal void Save(string exmsg)
        {
            try
            {
                string path = Application.StartupPath + "\\exception.txt";
                StreamWriter sw = File.AppendText(path);
                sw.WriteLine(DateTime.Now);
                sw.WriteLine(exmsg);
                sw.WriteLine();
                sw.Flush();
                sw.Close();
            }
            catch
            {
            }
        }
    }

}
