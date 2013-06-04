using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Xdgk.Common;
using C3.Communi;

namespace S
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            SApp.App.Run();
        }
    }

    public class Client
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="communiPort"></param>
        public Client(ICommuniPort communiPort)
        {
            this.CommuniPort = communiPort;
            this.CommuniPort.Received += new EventHandler(CommuniPort_Received);
        }

        void CommuniPort_Received(object sender, EventArgs e)
        {
            ICommuniPort cp = (ICommuniPort )sender ;
            byte[] bs = cp.Read();

            // TODO: 
            //
        }

        /// <summary>
        /// 
        /// </summary>
        public ICommuniPort CommuniPort
        {
            get { return _communiPort; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("CommuniPort");
                }
                _communiPort = value;
            }
        } private ICommuniPort _communiPort;
    }

    public class ClientCollection : Collection<Client>
    {
    }

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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void ClientEventHandler ( object sender , ClientEventArgs e );

    /// <summary>
    /// 
    /// </summary>
    public class ClientManager
    {
        public event ClientEventHandler Added;
        public event ClientEventHandler Removed;
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
