using System;
using System.IO.Ports ;
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


    /// <summary>
    /// 
    /// </summary>
    public class SerialCommuniPort : ICommuniPort
    {

        public SerialCommuniPort(SerialPort sp)
        {

        }

#region SerialPort
/// <summary>
/// 
/// </summary>
public SerialPort SerialPort
{
	get
	{
		return _serialPort;
	}
	set
	{
        if (value == null)
		{
            throw new ArgumentNullException("SerialPort");
		}
		_serialPort = value;
	}
} private SerialPort _serialPort;
#endregion //SerialPort

        #region ICommuniPort 成员

        public event EventHandler Received;

        public event EventHandler Determined;

        public event EventHandler Closed;

        public DateTime CreateDateTime
        {
            get { throw new NotImplementedException(); }
        }

        public void Close()
        {
            throw new NotImplementedException();
        }

        public bool Write(byte[] bytes)
        {
            throw new NotImplementedException();
        }

        public byte[] Read()
        {
            throw new NotImplementedException();
        }

        public bool IsOccupy
        {
            get { throw new NotImplementedException(); }
        }

        public void Occupy(TimeSpan ts)
        {
            throw new NotImplementedException();
        }

        public FilterCollection Filters
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string Identity
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public IdentityParserCollection IdentityParsers
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public bool IsOpened
        {
            get { throw new NotImplementedException(); }
        }

        #endregion
    }
}
