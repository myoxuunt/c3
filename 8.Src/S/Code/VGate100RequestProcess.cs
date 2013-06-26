
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
    internal class VGate100RequestProcess : IRequestProcess
    {
        private ReceivePart GetReceivePart()
        {
            if (_rp == null)
            {
                string xmlPath = Application.StartupPath + @"\Config\GateDeviceDefine\DeviceDefine.xml";
                ReceivePart rp = ReceivePartFacotry.Create(xmlPath, "vGate100", "receive");
                _rp = rp;
            }
            return _rp;
        } private ReceivePart _rp = null;

#region IRequestProcess 成员

        public bool Process(Client client, byte[] received)
        {
            StringBuilder sb = new StringBuilder();

            bool success = false;
            //foreach (ReceivePart RP in RPs)
            //{
            IParseResult pr = this.GetReceivePart().ToValues(received);
            if (pr.IsSuccess)
            {
                string name = Convert.ToString(pr.Results["name"]);
                name = name.Trim();

                DateTime dt = (DateTime)pr.Results["dt"];

                Console.WriteLine(name + " : " + dt);
                sb.AppendLine(string.Format("数据请求: '{0}', '{1}'", name, dt));

                //RequestType reqType = (RequestType)RP.Tag;
                byte[] bsReply = null;
                //if (reqType == RequestType.Gate)
                //{
                bsReply = CreateGateReplyBytes(name, dt, sb);
                //}
                //else if (reqType == RequestType.Pump)
                //{
                //bsReply = CreatePumpReplyBytes(name, dt, sb);
                //}

                bool r = client.CommuniPort.Write(bsReply);

                success = true;
            }
            //}

            if (!success)
            {
            }

            LogItem log = new LogItem(DateTime.Now, sb.ToString());
            client.LogItems.Add(log);

            Console.WriteLine(BitConverter.ToString(received));
            return success;
        }
#endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="dt"></param>
        /// <param name="logContentBuilder"></param>
        /// <returns></returns>
        private static byte[] CreateGateReplyBytes(string name, DateTime dt, StringBuilder logContentBuilder)
        {
            bool existGate = DB.ExistGate(name);
            if (!existGate)
            {
                logContentBuilder.AppendLine(string.Format("名称不存在: '{0}'", name));
                return new FailResponse(0, 0x85, ResponseStatusEnum.NotExistName).ToBytes();
            }

            DataTable tbl = DB.GetPumpDataTable(name, dt);
            if (tbl.Rows.Count == 0)
            {
                return new FailResponse(0, 0x85, ResponseStatusEnum.NotNewDatas).ToBytes();
            }

            int createdCount = 0;
            VGate100Data[] vgate100Datas = ConvertToVGate100Datas(tbl, out createdCount);
            logContentBuilder.AppendLine(string.Format("获取'{0}'条记录", createdCount));

            return new GateDataResponse(name, vgate100Datas, (byte)createdCount).ToBytes();
        }

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
    }

}
