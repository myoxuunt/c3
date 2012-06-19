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
    public interface ICommuniPortToken
    {
        bool CanCreate { get; }
        ICommuniPort Create();
    }

    public interface ICommuniPort
    {
        string ToXml();
        bool Match(ICommuniPortToken token);
        void Close();

        CommuniPortStatus Status { get; }

        bool Write(byte[] bytes);

        byte[] Read();

        bool IsOccupy { get; }

        void Occupy(TimeSpan ts);
        FilterCollection Filters { get; }
    }

    public class CommuniPortCollection : Xdgk.Common.Collection<ICommuniPort>
    {

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
