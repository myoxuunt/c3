using System;
using System.IO;
using System.Diagnostics;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Windows.Forms;
using Xdgk.Common;
using Xdgk.Common.Protocol;
using C3.Communi;
using VGate100Common;
using VPump100Common;

namespace S
{
    public class VPumpStatusParser
    {
        static private object[][] _map = new object[][]
        {
            new object[] {typeof(PumpStatus), PumpStatus.Unknown , "δ֪״̬" },
                new object[] {typeof(PumpStatus), PumpStatus.None , "������״̬" },
                new object[] {typeof(PumpStatus), PumpStatus.Run , "����״̬" },
                new object[] {typeof(PumpStatus), PumpStatus.RunWithVibrate , "������״̬" },


                new object[] {typeof(ForceStartStatus ), ForceStartStatus .Disable , "��ֹǿ��" },
                new object[] {typeof(ForceStartStatus ), ForceStartStatus .Enable , "����ǿ��" },


                new object[] {typeof(VibrateStatus ), VibrateStatus .None , "����״̬" },
                new object[] {typeof(VibrateStatus ), VibrateStatus .Vibrate , "��״̬" },

                new object[] {typeof ( PowerStatus ), PumpPowerStatus.CityPower , "�е�״̬"},
                new object[] {typeof ( PowerStatus ), PumpPowerStatus.Battery , "���״̬"},
        };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="statusText"></param>
        /// <returns></returns>
        static public object Parse(string statusText, Type enumType)
        {
            foreach (object[] array in _map)
            {
                if ((Type)array[0] == enumType &&
                        StringHelper.Equal(array[2].ToString(), statusText))
                {
                    return array[1];
                }
            }

            Console.WriteLine(
                    string.Format(
                        "not find '{0}' type status text '{1}'",
                        enumType.GetType().Name, statusText));

            foreach (object[] array in _map)
            {
                if ((Type)array[0] == enumType)
                {
                    return array[1];
                }
            }

            throw new ArgumentException("not find enum type: " + enumType.Name);
        }

        static public PumpPowerStatus ParsePowerStatus(string statusText)
        {
            return (PumpPowerStatus)Parse(statusText, typeof(PowerStatus));
        }

        static public PumpStatus ParsePumpStatus(string statusText)
        {
            return (PumpStatus)Parse(statusText, typeof(PumpStatus));
        }

        static public VibrateStatus ParseVibrateStatus(string statusText)
        {
            return (VibrateStatus)Parse(statusText, typeof(VibrateStatus));
        }

        static public ForceStartStatus ParseForceStartStatus(string statusText)
        {

            return (ForceStartStatus)Parse(statusText, typeof(ForceStartStatus));
        }
    }

}
