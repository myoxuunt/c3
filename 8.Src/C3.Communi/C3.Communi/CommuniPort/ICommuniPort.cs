using System;
using System.Collections.Generic;
using System.Text;
using Xdgk.Common;

namespace C3.Communi
{
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

        // TODO: delete
        //
        //bool Match(ICommuniPortToken token);
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
}
