using System;
using System.Collections.Generic;
using System.Text;

namespace C3.Communi
{
    public class StationCommuniPortBinder
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cp"></param>
        /// <param name="hardware"></param>
        static public StationCollection Bind(ICommuniPort cp, Hardware hardware)
        {
            StationCollection binded = new StationCollection();

            foreach (IStation station in hardware.Stations)
            {
                ICommuniPortConfig cpConfig = station.CommuniPortConfig;
                if (cpConfig.IsMatch(cp))
                {
                    ICommuniPort old = station.CommuniPort;
                    if (old != cp)
                    {
                        if (old != null)
                        {
                            old.Close();
                        }
                        station.CommuniPort = cp;
                        binded.Add(station);
                    }
                }
            }

            return binded;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cp"></param>
        /// <param name="hardware"></param>
        /// <returns></returns>
        static public StationCollection Unbind(ICommuniPort cp, Hardware hardware)
        {
            StationCollection unbinded = new StationCollection();
            foreach (IStation station in hardware.Stations)
            {
                if (station.CommuniPort == cp)
                {
                    station.CommuniPort = null;
                    unbinded.Add(station);
                }
            }
            return unbinded;
        }
    }
}
