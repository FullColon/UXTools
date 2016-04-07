using EC.Networking;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Timers;

namespace EC.Networking
{
    public class ECNetworkServer
    {
        private object mMutexObj = new object();
        private TcpListener mListener;
        private volatile int mBacklogSize;
        private volatile List<ECNetworkClient>  mClients;
        private Thread mListenerThread;
        private volatile bool mIsRunning;
        private ECServerLocatorServer mLocatorServer;


        // =======================================================================================
        // : Events & Actions
        // =======================================================================================

        /// <summary>
        /// Action performed when there is an error while connecting client.
        /// </summary>
        public Action onServerStartError;

        /// <summary>
        /// Action performed when a client successfully connects with handshake.
        /// </summary>
        public ECNetworkClientEvent onClientConnected;

        /// <summary>
        /// Fired when client has timed out.
        /// See <see cref="ECNetworkClient.onConnectionTimeout"/> for more details.
        /// </summary>
        public ECNetworkClientEvent onClientTimeout;

        /// <summary>
        /// Invoked when the other end of this connection has requested to disconnect.
        /// See <see cref="ECNetworkClient.onRequestedDisconnect"/> for more details.
        /// </summary>
        public ECNetworkClientEvent onClientRequestDisconnect;

        /// <summary>
        /// Invoked when client on this end gracefully disconnects.
        /// See <see cref="ECNetworkClient.onGracefulDisconnect"/> for more details.
        /// </summary>
        public ECNetworkClientEvent onClientGracefulDisconnect;

        /// <summary>
        /// Invoked when a client received data.
        /// See <see cref="ECNetworkClient.onDataReceived"/> for more details.
        /// </summary>
        public ECServerReceivedClientDataEvent onClientReceivedData;

        /// <summary>
        /// Construct a new ECNetworkServer instance.
        /// </summary>
        /// <param name="_port">Port to listen on. (0 for a random port)</param>
        /// <param name="_backlogSize">Size of server backlog. (Max number of connections)</param>
        public ECNetworkServer(int _port = 0, int _backlogSize = 50)
        {
            mListener = new TcpListener(IPAddress.Any, _port);
            mClients = new List<ECNetworkClient>();
            mIsRunning = false;
            mBacklogSize = _backlogSize;
           // mLocatorServer = new ECServerLocatorServer();
        }

        // =======================================================================================
        // : public methods.
        // =======================================================================================

        /// <summary>
        /// Start listening for this server.
        /// </summary>
        /// <param name="onStartError">Function to execute on start error.</param>
        public void start()
        {
            try
            {
                mListener.Start(mBacklogSize);
                //mLocatorServer.TcpServerPort = ((IPEndPoint)mListener.LocalEndpoint).Port;
                //mLocatorServer.start();
                mIsRunning = true;
                mListenerThread = new Thread(listenLoop);
                mListenerThread.Start();
            }
            catch
            {
                if (onServerStartError != null)
                    onServerStartError();
            }
        }

        /// <summary>
        /// Stop the server and disconnect all clients.
        /// </summary>
        public void stop()
        {
            if (mIsRunning)
            {
                mIsRunning = false;
                Console.WriteLine("Server on port " + mListener.LocalEndpoint + " closing client connections...");
                foreach (ECNetworkClient client in mClients)
                {
                    client.disconnect();
                }
                mListener.Stop();
                mListenerThread.Join();
                //mLocatorServer.stop();
            }
        }


        // =======================================================================================
        // : Threading methods.
        // =======================================================================================

        /// <summary>
        /// Loop for listening for connecting clients.
        /// </summary>
        private void listenLoop()
        {
            while (mIsRunning)
            {
                Thread.Sleep(50);
                try
                {
                    // checking if there are connections pending
                    if (mListener.Pending())
                    {
                        TcpClient cli = mListener.AcceptTcpClient();
                        mClients.Add(new ECNetworkClient(cli));
                        ECNetworkClient client = mClients[mClients.Count - 1];

                        // setting up passthrough delegates
                        client.onConnectSuccessful += () =>
                        {
                            lock (mMutexObj)
                            {
                                mClients.Add(client);
                            }
                            if (onClientConnected != null)
                                onClientConnected(client);
                        };
                        client.onConnectionTimeout += () =>
                        {
                            Console.WriteLine("Client timed out: " + client.getClientName());
                            if (onClientTimeout != null)
                                onClientTimeout(client);
                            lock (mMutexObj)
                            {
                                mClients.Remove(client);
                            }
                        };
                        client.onRequestedDisconnect += () =>
                        {
                            Console.WriteLine("Client " + client.getClientName() + " has requested graceful disconnect.");
                            if (onClientRequestDisconnect != null)
                                onClientRequestDisconnect(client);
                            lock (mMutexObj)
                            {
                                mClients.Remove(client);
                            }
                        };
                        client.onGracefulDisconnect += () =>
                        {
                            Console.WriteLine("Client " + client.getClientName() + " has accepted our shutdown request.");
                            if (onClientGracefulDisconnect != null)
                                onClientGracefulDisconnect(client);
                            lock (mMutexObj)
                            {
                                mClients.Remove(client);
                            }
                        };
                        client.onDataReceived += (ECNetworkRequestType _type, ref byte[] _data) =>
                        {
                            Console.WriteLine("Client " + client.getClientName() + " has received message of type: " + _type + " with " + _data.Length + " byte data size.");
                            if (onClientReceivedData != null)
                                onClientReceivedData(client, _type, ref _data);
                        };
                        client.send(ECNetworkRequestType.HANDSHAKE_REQUEST_USERNAME); // starting handshake...
                    }
                }
                catch { }
            }
        }
    }
}
