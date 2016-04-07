using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.Net;

namespace EC.Networking
{
    /// <summary>
    /// Executed when our client has located the ip address and port of the tcp listen server.
    /// </summary>
    /// <param name="_ipAddr">IP Address of the tcp server.</param>
    /// <param name="_port">Port of the tcp server.</param>
    public delegate void ECServerLocatedEvent(string _ipAddr, int _port);

    /// <summary>
    /// Properties for server locator.
    /// </summary>
    public class ECServerLocatorProperties
    {
        public static int ListenPort
        {
            get
            {
                return 12566;
            }
        }
        public static byte[] MessageBytes = ECNetworkHelper.ASCIIStringToNetworkBuffer("IS_THIS_OUR_SERVER?");
    }

    /// <summary>
    /// Listen server for locators. Keep it running in background to listen
    /// for clients.
    /// </summary>
    public class ECServerLocatorServer
    {
        private UdpClient mServer;
        private volatile bool mIsRunning;
        private Thread mRunThread;

        /// <summary>
        /// Port to return to the active clients.
        /// </summary>
        public int TcpServerPort { get; set; }

        /// <summary>
        /// Instantiate a ECServerLocatorServer object.
        /// </summary>
        public ECServerLocatorServer()
        {
            mServer = new UdpClient(ECServerLocatorProperties.ListenPort);
            mIsRunning = false;
        }

        /// <summary>
        /// Start listening for connecting clients.
        /// </summary>
        public void start()
        {
            mIsRunning = true;
            mRunThread = new Thread(listenLoop);
            mRunThread.Start();
        }

        /// <summary>
        /// Stop listening and shutdown server.
        /// </summary>
        public void stop()
        {
            mIsRunning = false;
            mRunThread.Join();
            mServer.Close();
        }

        private void listenLoop()
        {
            while (mIsRunning)
            {
                IPEndPoint clientEp = new IPEndPoint(IPAddress.Any, 0);
                try
                {
                    byte[] reqData = mServer.Receive(ref clientEp);
                    if (ECServerLocatorProperties.MessageBytes.SequenceEqual(reqData))
                    {
                        // valid request... returning response.
                        byte[] portBytes = BitConverter.GetBytes(TcpServerPort);
                        mServer.Send(portBytes, portBytes.Length);
                    }
                    Thread.Sleep(50);
                }
                catch
                {
                    
                }
            }
        }
    }

    /// <summary>
    /// Class that will help with locating a server on the local network.
    /// Note: This will be ONLY used as a locator, not as a client.
    /// </summary>
    public class ECServerLocatorClient
    {
        private UdpClient mClient;
        private IPEndPoint mServerAddr = new IPEndPoint(IPAddress.Any, 0);
        private Thread mLocateThread;

        /// <summary>
        /// Event to be fired when server is located.
        /// </summary>
        public ECServerLocatedEvent onServerLocated;

        /// <summary>
        /// Event to be fired when server is not located.
        /// </summary>
        public Action onServerNotLocated;

        /// <summary>
        /// Instantiate a new server locator.
        /// </summary>
        public ECServerLocatorClient()
        {
            mClient = new UdpClient();
            mClient.EnableBroadcast = true;
        }

        /// <summary>
        /// Start the process of looking for available servers.
        /// </summary>
        public void locateServerAsync()
        {
            mLocateThread = new Thread(locateServerProcess);
            mLocateThread.Start();
        }

        /// <summary>
        /// Process for locating server.
        /// </summary>
        private void locateServerProcess()
        {
            try
            {
                mClient.Send(ECServerLocatorProperties.MessageBytes, ECServerLocatorProperties.MessageBytes.Length, new IPEndPoint(IPAddress.Broadcast, ECServerLocatorProperties.ListenPort));
                byte[] response = mClient.Receive(ref mServerAddr);
                int port = BitConverter.ToInt32(response, 0);
                mClient.Close();
                if (onServerLocated != null)
                    onServerLocated(mServerAddr.Address.ToString(), port);
            }
            catch
            {
                if (onServerNotLocated != null)
                    onServerNotLocated();
            }
        }
    }
}
