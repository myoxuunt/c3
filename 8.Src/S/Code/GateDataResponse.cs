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
    public class GateDataResponse : BaseOpera
    {
        private VGate100Data [] _datas;
        private byte _datasCount = 0;

        public GateDataResponse(ResponseStatusEnum status)
            : this(string.Empty, status, null, 0)
        {

        }

        public GateDataResponse(string gateName, ResponseStatusEnum status, VGate100Data[] datas, int datasCount)
            : base(0, 0x85)
        {
            this.GateName = gateName;
            this.Status = status;
            this._datas = datas;
            this._datasCount = (byte)datasCount;
        }

        /// <summary>
        /// 
        /// </summary>
        public ResponseStatusEnum Status
        {
            get { return (ResponseStatusEnum)base.CommandNO; }
            set { base.CommandNO = (byte)value; }
        }

        public string GateName
        {
            get { return _gateName; }
            set { _gateName = value; }
        } private string _gateName;


        public override byte[] OnToBytes()
        {

            if (Status == 0)
            {
                byte[] bs = ASCIIEncoding.ASCII.GetBytes(new string(' ', 30));
                Debug.Assert(bs.Length == 30);

                byte[] bsName = UTF8Encoding.UTF8.GetBytes(this.GateName);
                Debug.Assert(bsName.Length <= 30);

                Array.Copy(bsName, bs, bsName.Length);

                MemoryStream ms = new MemoryStream();
                ms.Write(bs, 0, bs.Length);

                //
                //
                ms.WriteByte(_datasCount);

                byte[] bs2 = GetGateDataBytes(this._datas);
                ms.Write(bs2, 0, bs2.Length);

                return ms.ToArray();
            }
            else
            {
                return new byte[0];
            }
        }

        const int max = 5;

        private byte[] GetGateDataBytes(VGate100Data[] datas)
        {
            MemoryStream ms = new MemoryStream();
            foreach (VGate100Data data in datas)
            {
                byte[] bs = data.ToBytes();
                ms.Write(bs, 0, bs.Length);
            }
            return ms.ToArray();
        }
    }
}
