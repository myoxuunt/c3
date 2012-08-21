using System;
using System.Collections.Generic;
using System.Text;
using Xdgk.Common;

namespace C3.Communi
{
    public class PickResult
    {
        public bool IsSuccess;
        public byte[] Remain;
        public IParseResult ParseResult;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="remain"></param>
        /// <returns></returns>
        static public PickResult CreateSuccessPickResult(string name, IParseResult pr, byte[] remain)
        {
            PickResult  r = new PickResult();
            r.IsSuccess = true;
            //r.Name = name;
            r.Remain = remain;
            //r.Results = keyValues;
            r.ParseResult = pr;
            return r; 
        }

        static public PickResult  CreateFailPickResult(byte[] remain)
        {
            PickResult r = new PickResult();
            r.IsSuccess = false;
            r.Remain = remain;
            return r;
        }
    }

    public interface IPicker
    {
        string Name { get; set; } 
        PickResult Pick(IDevice device, byte[] bs);
    }

    public class PickerCollection : Collection<IPicker>
    {
    }
}
