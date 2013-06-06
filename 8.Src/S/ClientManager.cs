
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Xdgk.Common;
using C3.Communi;


namespace S
{
    public class ClientManager
    {
        public event ClientEventHandler Added;
        public event ClientEventHandler Removed;

        // TODO: received request event
        //
        //public event 

#region Clients
        /// <summary>
        /// 
        /// </summary>
        public ClientCollection Clients
        {
            get
            {
                if (_clients == null)
                {
                    _clients = new ClientCollection();
                }
                return _clients;
            }
        } private ClientCollection _clients;
#endregion //Clients


        /// <summary>
        /// 
        /// </summary>
        /// <param name="client"></param>
        public void Add(Client client)
        {
            this.Clients.Add(client);

            if (Added != null)
            {
                Added(this, new ClientEventArgs(client));
            }
        }

        internal void RemoveByCommuniPort(ICommuniPort cp)
        {
            for( int i = this.Clients.Count -1 ; i>=0 ; i-- )
            {
                Client c = this.Clients[i];
                if ( c .CommuniPort == cp )
                {
                    this.Remove(c);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="c"></param>
        private void Remove(Client c)
        {
            this.Clients.Remove(c);

            if (Removed != null)
            {
                Removed(this, new ClientEventArgs(c));
            }
        }
    }

}
