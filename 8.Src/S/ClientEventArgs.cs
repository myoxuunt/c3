
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Xdgk.Common;
using C3.Communi;


namespace S
{
    public class ClientEventArgs: EventArgs 
    {
        public ClientEventArgs ( Client client )
        {
            this.Client = client;
        }

#region Client
        /// <summary>
        /// 
        /// </summary>
        public Client Client
        {
            get
            {
                return _client;
            }
            set
            {
                _client = value;
            }
        } private Client _client;
#endregion //Client
    }

}
