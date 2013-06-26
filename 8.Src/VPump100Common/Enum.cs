using System;
using System.Collections.Generic;
using System.Text;
using Xdgk.Common;

namespace VPump100Common
{
    public enum PumpPowerStatus
    {
        CityPower,
        Battery,
    }

    public enum PumpStatus
    {
        None,
        Run,
        RunWithVibrate,
        Unknown,
    }

    public enum VibrateStatus
    {
        None,
        Vibrate,
    }


    public enum ForceStartStatus
    {
        Disable,
        Enable,
    }

}
