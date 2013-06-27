using System;
using System.Collections.Generic;
using System.Text;
using Xdgk.Common;

namespace VPump100Common
{
    public enum PumpPowerStatus
    {
        [EnumText("市电供电")]
        CityPower,
        [EnumText("电池供电")]
        Battery,
    }

    public enum PumpStatus
    {
        [EnumText("无运行状态")]
        None,
        [EnumText("运行状态")]
        Run,
        [EnumText("振动运行状态")]
        RunWithVibrate,
        [EnumText("未知状态")]
        Unknown,
    }

    public enum VibrateStatus
    {
        [EnumText("无振状态")]
        None,
        [EnumText("振动状态")]
        Vibrate,
    }


    public enum ForceStartStatus
    {
        [EnumText("禁止强启")]
        Disable,
        [EnumText("运行强启")]
        Enable,
    }

}
