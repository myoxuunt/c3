﻿using System.Collections;
using System.IO;

namespace C3.Communi
{
    static public class DeviceCommuniLogger
    {

        #region DeviceCommuniLogger
        /// <summary>
        /// 
        /// </summary>
        static DeviceCommuniLogger()
        {
            string enableCommuniLog = System.Configuration.ConfigurationManager.AppSettings["EnableCommuniLog"];
            if (!string.IsNullOrEmpty(enableCommuniLog))
            {
                bool b = false;
                if (bool.TryParse(enableCommuniLog, out b))
                {
                    Enabled = b;
                }
            }
        }
        #endregion //DeviceCommuniLogger

        #region Enabled
        /// <summary>
        /// 
        /// </summary>
        static public bool Enabled
        {
            get { return _enabled; }
            set { _enabled = value; }
        } static private bool _enabled = false;
        #endregion //Enabled

        #region Log
        /// <summary>
        /// 
        /// </summary>
        /// <param name="device"></param>
        /// <param name="cd"></param>
        static public void Log(IDevice device, CommuniDetail cd)
        {
            if (Enabled)
            {
                string path = string.Format(
                    ".\\CommuniLog\\{0}\\{1}_{2}.txt",
                    device.Station.Name,
                    device.GetType().Name,
                    device.Address);

                path = PathUtils.MapToStartupPath(path);

                StreamWriter sw = GetStreamWriter(path);
                sw.WriteLine(cd.ToString());
                sw.WriteLine();
                sw.Flush();
            }
        }
        #endregion //Log

        #region StreamWriter
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private static StreamWriter GetStreamWriter(string path)
        {
            string key = path.ToUpper();

            StreamWriter sw = null;
            sw = _hash[key] as StreamWriter;

            if (sw == null)
            {
                sw = CreateFile(path);
                _hash[key] = sw;
            }
            return sw;
        }
        #endregion //StreamWriter

        #region Members
        /// <summary>
        /// 
        /// </summary>
        static private Hashtable _hash = new Hashtable();
        #endregion //Members

        #region CreateFile
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        static private StreamWriter CreateFile(string path)
        {
            string dir = System.IO.Path.GetDirectoryName(path);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            FileStream fs = File.Open(path, FileMode.Append);
            StreamWriter sw = new StreamWriter(fs);
            return sw;
        }
        #endregion //CreateFile
    }
}
