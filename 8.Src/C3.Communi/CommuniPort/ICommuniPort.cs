using System;
using System.Collections.Generic;
using System.Text;
using Xdgk.Common;

namespace C3.Communi
{
    public enum CommuniPortStatus
    {

    }

    /// <summary>
    /// 
    /// </summary>
    public interface ICommuniPortConfig
    {
        bool CanCreate { get; }
        ICommuniPort Create();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cp"></param>
        /// <returns></returns>
        bool IsMatch(ICommuniPort cp);
    }

    /// <summary>
    /// 
    /// </summary>
    public interface ICommuniPort
    {
        /// <summary>
        /// 
        /// </summary>
        event EventHandler Received;
        event EventHandler Determined;
        event EventHandler Closed;

        /// <summary>
        /// 
        /// </summary>
        DateTime CreateDateTime { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        //string ToXml();

        //CommuniPortStatus Status { get; }

        void Close();


        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        bool Write(byte[] bytes);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        byte[] Read();

        bool IsOccupy { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ts"></param>
        void Occupy(TimeSpan ts);

        FilterCollection Filters { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string Identity { get; set; }

        /// <summary>
        /// 
        /// </summary>
        IdentityParserCollection IdentityParsers { get; set; }

        /// <summary>
        /// 
        /// </summary>
        bool IsOpened
        {
            get;
        }

    }

    public class CommuniPortCollection : Xdgk.Common.Collection<ICommuniPort>
    {

    }

    /// <summary>
    /// 
    /// </summary>
    public interface IIdentityParser
    {
        bool Parse(byte[] identityBytes, out string identity);
    }

    /// <summary>
    /// 
    /// </summary>
    public class IdentityParserCollection : Collection<IIdentityParser>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="identityBytes"></param>
        /// <returns></returns>
        public bool Parse(byte[] identityBytes, out string identity)
        {
            bool b = false;
            identity = null;

            foreach (IIdentityParser parser in this)
            {
                b = parser.Parse(identityBytes, out identity);
                if (b)
                {
                    break;
                }
            }
            return b;
        }
    }

    public interface IFilter
    {
        byte[] Filt( byte[] source);
    }

    /// <summary>
    /// 
    /// </summary>
    public class FilterCollection : Collection<IFilter>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public byte[] Filt(byte[] source)
        {
            //if (this.Count == 0)
            //{
            //    return source;
            //}

            //string temp = (string)HexStringConverter.Default.ConvertToObject(source);
            //foreach (IFilter f in this)
            //{
            //    temp = f.Filt(temp);
            //}
            //byte[] bs = (byte[])HexStringConverter.Default.ConvertToBytes(temp);
            //return bs;
            throw new NotImplementedException();
        }
    }

}
