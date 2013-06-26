
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
    internal class VPump100RequestProcess : IRequestProcess
    {
        /// <summary>
        /// 
        /// </summary>
        public VPump100RequestProcess()
        {

        }

        private ReceivePart GetReceivePart()
        {
            if (_rp == null)
            {
                string xmlPath = Application.StartupPath + @"\Config\PumpDeviceDefine\DeviceDefine.xml";
                ReceivePart rp = ReceivePartFacotry.Create(xmlPath, "vPump100", "receive");
                _rp = rp;
            }
            return _rp;
        } private ReceivePart _rp;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="client"></param>
        /// <param name="bs"></param>
        public bool Process(Client client, byte[] bs)
        {
            StringBuilder sb = new StringBuilder();

            bool success = false;

            IParseResult pr = this.GetReceivePart().ToValues(bs);
            if (pr.IsSuccess)
            {
                string name = Convert.ToString(pr.Results["name"]);
                name = name.Trim();

                DateTime dt = (DateTime)pr.Results["dt"];

                Console.WriteLine(name + " : " + dt);
                sb.AppendLine(string.Format("数据请求: '{0}', '{1}'", name, dt));

                byte[] bsReply = null;
                bsReply = CreatePumpReplyBytes(name, dt, sb);

                bool r = client.CommuniPort.Write(bsReply);

                success = true;
            }

            if (!success)
            {
                sb.AppendLine(string.Format("无效请求: '{0}'", BitConverter.ToString(bs)));
            }

            LogItem log = new LogItem(DateTime.Now, sb.ToString());
            client.LogItems.Add(log);

            Console.WriteLine(BitConverter.ToString(bs));

            return success;
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
            bool existPump = DB.ExistPump(name);
            if (!existPump)
            {
                return new FailResponse(0, 0x86, ResponseStatusEnum.NotExistName).ToBytes();
            }

            DataTable tbl = DB.GetPumpDataTable(name, dt);
            if (tbl.Rows.Count == 0)
            {
                return new FailResponse(0, 0x86, ResponseStatusEnum.NotNewDatas).ToBytes();
            }

            int createdCount = 0;
            VPump100Data[] datas = ConvertToVPump100Datas(tbl, out createdCount);
            return new PumpDataResponse(name, datas, (byte)createdCount).ToBytes();
        }

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
                PumpPowerStatus powerStatus = VPumpStatusParser.ParsePowerStatus(row[ColumnNamesForPump.Power].ToString());


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
