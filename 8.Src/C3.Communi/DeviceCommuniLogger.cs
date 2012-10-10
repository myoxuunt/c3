using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace C3.Communi
{
    public class DeviceCommuniLogger
    {
        /// <summary>
        /// 
        /// </summary>
        private DeviceCommuniLogger()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        static public bool Enabled
        {
            get { return _enabled; }
        } static private bool _enabled = true;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="device"></param>
        /// <param name="cd"></param>
        static public void Log(IDevice device, CommuniDetail cd)
        {
            string path = string.Format(
                "{0}\\{1}_{2}.txt", 
                device.Station.Name, 
                device.GetType().Name, 
                device.Address);

            FileStream fs = File.Open(path, FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine(cd.ToString());
        }
    }
}
