using System;
using System.IO;
using System.Reflection;
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
            return Path.Combine(System.Windows.Forms.Application.StartupPath, path);
            //return System.Windows.Forms.Application.StartupPath + "\\" + path;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        internal static bool IsAbsolutePhysicalPath(string path)
        {
            if ((path == null) || (path.Length < 3))
            {
                return false;
            }
            return (((path[1] == ':') && IsDirectorySeparatorChar(path[2])) || IsUncSharePath(path));
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        internal static bool IsUncSharePath(string path)
        {
            return (((path.Length > 2) && IsDirectorySeparatorChar(path[0])) && IsDirectorySeparatorChar(path[1]));
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="ch"></param>
        /// <returns></returns>
        private static bool IsDirectorySeparatorChar(char ch)
        {
            if (ch != '\\')
            {
                return (ch == '/');
            }
            return true;
        }
    }
}
