using System;
using System.IO;
using System.Diagnostics;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Windows.Forms;
using Xdgk.Common;
using C3.Communi;

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
                    _rp = ReceivePartFacotry.Create(xmlPath, "vFlux", "receive");
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
            IParseResult pr = RP.ToValues(bs);
            if (pr.IsSuccess)
            {
                string name = Convert.ToString(pr.Results["name"]);
                name = name.Trim();

                DateTime dt = (DateTime)pr.Results["dt"];

                Console.WriteLine(name + " : " + dt);

                byte[] bsReply = CreateReplyBytes(name, dt);

                client.CommuniPort.Write(bsReply);

            }
            else
            {

            }
            Console.WriteLine(BitConverter.ToString(bs));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        private static byte[] CreateReplyBytes(string name, DateTime dt)
        {
            byte status = 0;

            bool existGate = DB.ExistGate(name);
            DataTable tbl = null;
            if (!existGate)
            {
                status = 1;
            }

            tbl = DB.GetGateDataTable(name, dt);

            GateDataResponse rep = new GateDataResponse(name, status, tbl);
            return rep.ToBytes();
        }
    }


    class ColumnNames
    {
        public const string StrTime = "StrTime";
        public const string BeforeLevel = "BeforeLevel";
        public const string BehindLevel = "BehindLevel";
        public const string Height = "Height";
        public const string Flux = "Flux";
        public const string ReWater = "ReWater";
        public const string TuWater = "TuWater";
    }

    /// <summary>
    /// 
    /// </summary>
    abstract public class BaseOpera
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="address"></param>
        /// <param name="commandType"></param>
        public BaseOpera(byte address, byte commandType)
        {
            this.Address = address;
            this.CommandType = commandType;
        }

        #region Address
        /// <summary>
        /// 
        /// </summary>
        public byte Address
        {
            get
            {
                return _address;
            }
            set
            {
                _address = value;
            }
        } private byte _address;
        #endregion //Address

        /// <summary>
        /// 
        /// </summary>
        public byte CommandNO
        {
            get { return _commandNO; }
            set { _commandNO = value; }
        } private byte _commandNO;

        /// <summary>
        /// 
        /// </summary>
        public byte CommandType
        {
            get { return _commandType; }
            set { _commandType = value; }
        } private byte _commandType;

        public byte[] ToBytes()
        {
            MemoryStream ms = new MemoryStream();
            byte[] h = new byte[] { 0x5b, 0x5b };
            byte[] t = new byte[] { 0x5d, 0x5d };

            ms.Write(h, 0, h.Length);
            ms.WriteByte(this.Address);
            ms.WriteByte(this.CommandType);
            ms.WriteByte(this.CommandNO);

            byte[] bs = OnToBytes();
            ms.Write(bs, 0, bs.Length);
            ms.Write(t, 0, t.Length);

            return ms.ToArray();

        }
        abstract public byte[] OnToBytes();
    }

    public class GateDataResponse : BaseOpera
    {
        private DataTable _gateTable;

        public GateDataResponse(string gateName, byte status, DataTable gateDatatable)
            : base(0, 0x85)
        {
            this.GateName = gateName;
            this.Status = status;
            this._gateTable = gateDatatable;
        }

        /// <summary>
        /// 0 - success
        /// 1 - not find gate name
        /// </summary>
        public byte Status
        {
            get { return base.CommandNO; }
            set { base.CommandNO = value; }
        }

        public string GateName
        {
            get { return _gateName; }
            set { _gateName = value; }
        } private string _gateName;


        public override byte[] OnToBytes()
        {

            byte[] bs = ASCIIEncoding.ASCII.GetBytes(new string(' ', 30));
            Debug.Assert(bs.Length == 30);

            byte[] bsName = UTF8Encoding.UTF8.GetBytes(this.GateName);
            Debug.Assert(bsName.Length <= 30);

            Array.Copy(bsName, bs, bsName.Length);

            MemoryStream ms = new MemoryStream();
            ms.Write(bs, 0, bs.Length);

            byte[] bs2 = GetGateDataBytes(this._gateTable);
            ms.Write(bs2, 0, bs2.Length);

            return ms.ToArray();
        }

        const int max = 5;

        private byte[] GetGateDataBytes(DataTable dataTable)
        {
            MemoryStream ms = new MemoryStream();
            int rows = dataTable.Rows.Count;
            if (rows > max)
            {
                rows = max;
            }

            ms.WriteByte((byte)rows);

            for (int i = 0; i < max; i++)
            {
                DateTime dt = DateTime.MinValue;
                float lwBefore = 0;
                float lwBehind = 0;
                float height = 0;
                float flux = 0;
                float sum = 0;
                float remain = 0;

                if (i < dataTable.Rows.Count)
                {
                    DataRow row = dataTable.Rows[i];

                    dt = Convert.ToDateTime(row[ColumnNames.StrTime]);
                    lwBefore = Convert.ToSingle(row[ColumnNames.BeforeLevel]);
                    lwBehind = Convert.ToSingle(row[ColumnNames.BehindLevel]);
                    height = Convert.ToSingle(row[ColumnNames.Height]);
                    flux = Convert.ToSingle(row[ColumnNames.Flux]);
                    sum = Convert.ToSingle(row[ColumnNames.TuWater]);
                    remain = Convert.ToSingle(row[ColumnNames.ReWater]);
                }

                Write(ms, BitConverter.GetBytes(dt.Ticks));
                Write(ms, BitConverter.GetBytes(lwBefore));
                Write(ms, BitConverter.GetBytes(lwBehind));
                Write(ms, BitConverter.GetBytes(height));
                Write(ms, BitConverter.GetBytes(flux));
                Write(ms, BitConverter.GetBytes(sum));
                Write(ms, BitConverter.GetBytes(remain));
            }

            return ms.ToArray();
        }

        void Write(MemoryStream ms, byte[] bs)
        {
            ms.Write(bs, 0, bs.Length);
        }
    }

}
