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
    public class PumpDataResponse : NameCountResponseBase 
    {
        public PumpDataResponse(string pumpName, VPump100Data [] datas, byte datasCount)
            : base(0, 0x86, pumpName, datas, datasCount)
        {
        }
    }

    
    /// <summary>
    /// 
    /// </summary>
    public class FailResponse : BaseOpera
    {
        public FailResponse(byte address, byte cmdType, ResponseStatusEnum status)
            : base(address, cmdType)
        {
            this.Status = status;
        }

        public ResponseStatusEnum Status
        {
            get { return (ResponseStatusEnum)base.CommandNO; }
            set { base.CommandNO = (byte)value; }
        }

        public override byte[] OnToBytes()
        {
            return new byte[0];
        }
    }

    public class NameCountResponseBase : BaseOpera
    {
        private string _name;
        private byte _count;
        private IToBytes[] _toBytesList;

        public NameCountResponseBase(byte address, byte commandType, string name,  IToBytes[] toBytesList,byte count)
            : base(address, commandType)
        {
            this._name = name;
            this._count = count;
            this._toBytesList = toBytesList;
        }

        public override byte[] OnToBytes()
        {
            // name
            //
            byte[] bs = ASCIIEncoding.ASCII.GetBytes(new string(' ', 30));
            Debug.Assert(bs.Length == 30);

            byte[] bsName = UTF8Encoding.UTF8.GetBytes(this._name);
            Debug.Assert(bsName.Length <= 30);

            Array.Copy(bsName, bs, bsName.Length);

            MemoryStream ms = new MemoryStream();
            ms.Write(bs, 0, bs.Length);

            // count
            //
            ms.WriteByte(this._count);

            // data
            //
            byte[] bs2 = GetGateDataBytes(this._toBytesList);
            ms.Write(bs2, 0, bs2.Length);

            return ms.ToArray();
        }

        private byte[] GetGateDataBytes(IToBytes[] datas)
        {
            MemoryStream ms = new MemoryStream();
            foreach (IToBytes item in datas)
            {
                byte[] bs = item.ToBytes();
                ms.Write(bs, 0, bs.Length);
            }
            return ms.ToArray();
        }
    }

    public class GateDataResponse : NameCountResponseBase 
    {
        public GateDataResponse(string gateName,  VGate100Data[] datas, byte datasCount)
            //: base(0, 0x85)
            : base(0, 0x85, gateName, datas, datasCount)
        {
            //this.GateName = gateName;
            //this.Status = status;
            //this._datas = datas;
            //this._datasCount = (byte)datasCount;
        }

        ///// <summary>
        ///// 
        ///// </summary>
        //public ResponseStatusEnum Status
        //{
        //    get { return (ResponseStatusEnum)base.CommandNO; }
        //    set { base.CommandNO = (byte)value; }
        //}

        //public string GateName
        //{
        //    get { return _gateName; }
        //    set { _gateName = value; }
        //} private string _gateName;


        //public override byte[] OnToBytes()
        //{

        //    if (Status == 0)
        //    {
        //        byte[] bs = ASCIIEncoding.ASCII.GetBytes(new string(' ', 30));
        //        Debug.Assert(bs.Length == 30);

        //        byte[] bsName = UTF8Encoding.UTF8.GetBytes(this.GateName);
        //        Debug.Assert(bsName.Length <= 30);

        //        Array.Copy(bsName, bs, bsName.Length);

        //        MemoryStream ms = new MemoryStream();
        //        ms.Write(bs, 0, bs.Length);

        //        //
        //        //
        //        ms.WriteByte(_datasCount);

        //        byte[] bs2 = GetGateDataBytes(this._datas);
        //        ms.Write(bs2, 0, bs2.Length);

        //        return ms.ToArray();
        //    }
        //    else
        //    {
        //        return new byte[0];
        //    }
        //}

        //const int max = 5;

        //private byte[] GetGateDataBytes(VGate100Data[] datas)
        //{
        //    MemoryStream ms = new MemoryStream();
        //    foreach (VGate100Data data in datas)
        //    {
        //        byte[] bs = data.ToBytes();
        //        ms.Write(bs, 0, bs.Length);
        //    }
        //    return ms.ToArray();
        //}
    }
}
