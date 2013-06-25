using System;
using System.IO;
using System.Diagnostics;
using System.Text;
using Xdgk.Common;
using Xdgk.Common.Protocol;
using VGate100Common;
using VPump100Common;

namespace S
{
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

}
