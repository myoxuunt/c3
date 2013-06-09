using System;
using System.IO;
using System.Diagnostics;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Windows.Forms;
using Xdgk.Common;
using C3.Communi;
using VGATE100DPU;

namespace S
{
    internal class RPProcessor
    {
        static private ReceivePart RP
        {
            get
            {
                if (_rp == null)
                {
                    string xmlPath = Application.StartupPath + @"\Config\DeviceDefine.xml";
                    _rp = ReceivePartFacotry.Create(xmlPath, "vGate100", "receive");
                }
                return _rp;
            }
        }
        static ReceivePart _rp;

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

            IParseResult pr = RP.ToValues(bs);
            if (pr.IsSuccess)
            {
                string name = Convert.ToString(pr.Results["name"]);
                name = name.Trim();

                DateTime dt = (DateTime)pr.Results["dt"];

                Console.WriteLine(name + " : " + dt);
                sb.AppendLine(string.Format("数据请求: '{0}', '{1}'", name, dt));

                byte[] bsReply = CreateReplyBytes(name, dt, sb);

                bool r = client.CommuniPort.Write(bsReply);
            }
            else
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
        /// <returns></returns>
        private static byte[] CreateReplyBytes(string name, DateTime dt, StringBuilder logContentBuilder)
        {
            byte status = 0;

            bool existGate = DB.ExistGate(name);
            if (!existGate)
            {
                status = 1;
                logContentBuilder.AppendLine(string.Format("名称不存在: '{0}'", name));
            }

            DataTable tbl = DB.GetGateDataTable(name, dt);

            int createdCount;
            GateDataResponse rep = new GateDataResponse(name, status, ConvertToVGate100Datas(tbl, out createdCount));
            byte[] bs = rep.ToBytes();
            logContentBuilder.AppendLine(string.Format("获取'{0}'条记录", createdCount));
            return bs;
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
    }
}