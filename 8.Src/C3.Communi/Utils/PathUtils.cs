using System;
using System.IO;
using System.Reflection ;
using System.Collections.Generic;
using System.Text;

namespace C3.Communi
{
    public class PathUtils
    {
        private PathUtils()
        {
        }

        static public string GetAssemblyDirectory(Assembly assembly)
        {
            string location = assembly.Location;
            string dir = Path.GetDirectoryName(location);
            return dir;
        }

        static public string SocketListenerConfigFileName
        {
            get
            {
                return System.Windows.Forms.Application.StartupPath + "\\Config\\ListenPort.xml";
            }
        }
        static public string DPUConfigFileName
        {
            get
            {
                return System.Windows.Forms.Application.StartupPath + "\\Config\\dpu.xml";
            }
        }

        static public string SPUConfigFileName
        {
            get
            {
                return System.Windows.Forms.Application.StartupPath + "\\Config\\spu.xml";
            }
        }
        /// <summary>
        /// 
        /// </summary>
        static public string SourceConfigFileName
        {
            get
            {
                return System.Windows.Forms.Application.StartupPath + "\\Config\\Source.xml";
            }
        }

        static public string MapToStartupPath(string path)
        {
            return System.Windows.Forms.Application.StartupPath + "\\" + path;
        }
    }
}
