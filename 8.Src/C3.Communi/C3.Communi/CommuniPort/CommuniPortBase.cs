
using System;
using Xdgk.Common;

namespace C3.Communi
{
    abstract public class CommuniPortBase : ICommuniPort
    {

        public event EventHandler Received;
        public event EventHandler Determined;
        public event EventHandler Closed;



        public DateTime CreateDateTime
        {
            get { throw new NotImplementedException(); }
        }

        public string ToXml()
        {
            throw new NotImplementedException();
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
            get { throw new NotImplementedException(); }
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




        FilterCollection ICommuniPort.Filters
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
    }

}
