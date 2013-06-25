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
    using PowerStatus = VPump100Common.PowerStatus;

    public class VPumpStatusParser
    {
        static private object[][] _map = new object[][]
        {
            new object[] {typeof(PumpStatus), PumpStatus.Unknown , "未知状态" },
            new object[] {typeof(PumpStatus), PumpStatus.None , "无运行状态" },
            new object[] {typeof(PumpStatus), PumpStatus.Run , "运行状态" },
            new object[] {typeof(PumpStatus), PumpStatus.RunWithVibrate , "振动运行状态" },


            new object[] {typeof(ForceStartStatus ), ForceStartStatus .Disable , "禁止强启" },
            new object[] {typeof(ForceStartStatus ), ForceStartStatus .Enable , "允许强启" },


            new object[] {typeof(VibrateStatus ), VibrateStatus .None , "无振状态" },
            new object[] {typeof(VibrateStatus ), VibrateStatus .Vibrate , "振动状态" },

            new object[] {typeof ( PowerStatus ), PowerStatus.CityPower , "市电状态"},
            new object[] {typeof ( PowerStatus ), PowerStatus.Battery , "电池状态"},
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
                if ( (Type)array[0] == enumType &&
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

        static public PowerStatus ParsePowerStatus(string statusText)
        {
            return (PowerStatus)Parse(statusText, typeof(PowerStatus));
        }

        static public PumpStatus  ParsePumpStatus(string statusText)
        {
            return (PumpStatus)Parse(statusText, typeof(PumpStatus));
        }

        static public VibrateStatus   ParseVibrateStatus(string statusText)
        {
            return (VibrateStatus)Parse(statusText, typeof(VibrateStatus));
        }

        static public ForceStartStatus ParseForceStartStatus(string statusText)
        {

            return (ForceStartStatus)Parse(statusText, typeof(ForceStartStatus));
        }
    }
    internal class RPProcessor
    {
        private enum RequestType
        {
            Gate,
            Pump,
        }

        static private ReceivePart[] RPs
        {
            get
            {
                if (_rps == null)
                {
                    _rps = new ReceivePart[2];

                    string xmlPath = Application.StartupPath + @"\Config\GateDeviceDefine\DeviceDefine.xml";
                    ReceivePart rp = ReceivePartFacotry.Create(xmlPath, "vGate100", "receive");
                    rp.Tag = RequestType.Gate;

                    _rps[0] = rp;

                    string xmlPath2 = Application.StartupPath + @"\Config\PumpDeviceDefine\DeviceDefine.xml";
                    ReceivePart rp2 = ReceivePartFacotry.Create(xmlPath2, "vPump100", "receive");
                    rp2.Tag = RequestType.Pump;
                    _rps[1] = rp2;

                }
                return _rps;
            }
        } static private ReceivePart[] _rps;

        /// <summary>
        /// 
        /// </summary>
        private RPProcessor()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="client"></param>
        /// <param name="bs"></param>
        static public void Process(Client client, byte[] bs)
        {
            StringBuilder sb = new StringBuilder();

            bool success = false;
            foreach (ReceivePart RP in RPs)
            {
                IParseResult pr = RP.ToValues(bs);
                if (pr.IsSuccess)
                {
                    string name = Convert.ToString(pr.Results["name"]);
                    name = name.Trim();

                    DateTime dt = (DateTime)pr.Results["dt"];

                    Console.WriteLine(name + " : " + dt);
                    sb.AppendLine(string.Format("数据请求: '{0}', '{1}'", name, dt));

                    RequestType reqType = (RequestType )RP.Tag ;
byte[] bsReply  = null;
                    if (reqType == RequestType.Gate)
                    {
                        bsReply = CreateGateReplyBytes(name, dt, sb);
                    }
                    else if (reqType == RequestType.Pump)
                    {
                        bsReply = CreatePumpReplyBytes(name, dt, sb);
                    }

                    bool r = client.CommuniPort.Write(bsReply);

                    success = true;
                }
            }

            if ( !success )
            {
                sb.AppendLine(string.Format("无效请求: '{0}'", BitConverter.ToString(bs)));
            }

            LogItem log = new LogItem(DateTime.Now, sb.ToString());
            client.LogItems.Add(log);

            Console.WriteLine(BitConverter.ToString(bs));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="dt"></param>
        /// <param name="sb"></param>
        /// <returns></returns>
        private static byte[] CreatePumpReplyBytes(string name, DateTime dt, StringBuilder logContentBuilder)
        {
            byte[] r = null;
            ResponseStatusEnum status = ResponseStatusEnum.Success;
            bool existPump = DB.ExistPump(name);
            if (existPump)
            {
            }
            else
            {
                status = ResponseStatusEnum.NotExistName;
            }
            return r;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        private static byte[] CreateGateReplyBytes(string name, DateTime dt, StringBuilder logContentBuilder)
        {
            byte[] r = null;
            ResponseStatusEnum status = ResponseStatusEnum.Success;
            DataTable tbl = null;

            bool existGate = DB.ExistGate(name);
            if (existGate)
            {
                int createdCount = 0;
                tbl = DB.GetPumpDataTable(name, dt);
                if (tbl.Rows.Count > 0)
                {
                    VGate100Data[] vgate100Datas = ConvertToVGate100Datas(tbl, out createdCount);
                    GateDataResponse rep = new GateDataResponse(name, vgate100Datas, (byte)createdCount);
                    byte[] bs = rep.ToBytes();
                    r = bs;
                }
                else
                {
                    status = ResponseStatusEnum.NotNewDatas;
                    r = new FailResponse(0, 0x85, status).ToBytes();
                }
                logContentBuilder.AppendLine(string.Format("获取'{0}'条记录", createdCount));
            }
            else
            {
                status = ResponseStatusEnum.NotExistName;
                logContentBuilder.AppendLine(string.Format("名称不存在: '{0}'", name));
                r = new FailResponse(0, 0x85, status).ToBytes();
            }
            return r;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tbl"></param>
        /// <returns></returns>
        static private VGate100Data[] ConvertToVGate100Datas(DataTable dataTable, out int createdCount)
        {
            createdCount = 0;

            List<VGate100Data> list = new List<VGate100Data>();

            foreach (DataRow row in dataTable.Rows)
            {
                DateTime dt = Convert.ToDateTime(row[ColumnNames.StrTime]);
                float lwBefore = Convert.ToSingle(row[ColumnNames.BeforeLevel]);
                float lwBehind = Convert.ToSingle(row[ColumnNames.BehindLevel]);
                float height = Convert.ToSingle(row[ColumnNames.Height]);
                float flux = Convert.ToSingle(row[ColumnNames.Flux]);
                float sum = Convert.ToSingle(row[ColumnNames.TuWater]);
                float remain = Convert.ToSingle(row[ColumnNames.ReWater]);

                VGate100Data data = new VGate100Data();
                data.DT = dt;
                data.BeforeWL = lwBefore;
                data.BehindWL = lwBehind;
                data.Height = height;
                data.InstantFlux = flux;
                data.TotalAmount = sum;
                data.RemainAmount = remain;

                list.Add(data);
                if (list.Count >= 5)
                {
                    break;
                }
            }
            createdCount = list.Count;

            while (list.Count < 5)
            {
                list.Add(new VGate100Data());
            }
            return list.ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="datatable"></param>
        /// <param name="createCount"></param>
        /// <returns></returns>
        static private VPump100Data[] ConvertToVPump100Datas(DataTable datatable, out int createCount)
        {
            createCount = 0;
            List<VPump100Data> list = new List<VPump100Data>();
            foreach (DataRow row in datatable.Rows)
            {
                DateTime dt = Convert.ToDateTime(row[ColumnNamesForPump.StrTime]);
                float instantFlux = Convert.ToSingle(row[ColumnNamesForPump.Flux]);
                float efficienty = Convert.ToSingle(row[ColumnNamesForPump.Efficiency]);
                float sum = Convert.ToSingle(row[ColumnNames.TuWater]);
                float remain = Convert.ToSingle(row[ColumnNames.ReWater]);

                PumpStatus pumpStatus = VPumpStatusParser.ParsePumpStatus(row[ColumnNamesForPump.PumpStatus].ToString());
                ForceStartStatus forceStatus = VPumpStatusParser.ParseForceStartStatus(row[ColumnNamesForPump.ForceRun].ToString());
                VibrateStatus vibrateStatus = VPumpStatusParser.ParseVibrateStatus(row[ColumnNamesForPump.Vibrate].ToString());
                PowerStatus powerStatus = VPumpStatusParser.ParsePowerStatus(row[ColumnNamesForPump.Power].ToString());


                VPump100Data data = new VPump100Data();
                data.DT = dt;
                data.InstantFlux = instantFlux;
                data.Efficiency = efficienty;
                data.TotalAmount = sum;
                data.RemainAmount = remain;
                data.PumpStatus = pumpStatus;
                data.ForceStartStatus = forceStatus;
                data.VibrateStatus = vibrateStatus;
                data.PowerStatus = powerStatus;

                list.Add(data);
                if (list.Count >= 5)
                {
                    break;
                }
            }
            createCount = list.Count;
            while (list.Count < 5)
            {
                list.Add(new VPump100Data());
            }
            return list.ToArray();
        }
    }
}
