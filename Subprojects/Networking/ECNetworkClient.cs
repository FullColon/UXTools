using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Threading;
using System.Timers;

namespace EC.Networking
{
    /// <summary>
    /// Tells us if this client is on the server-side or client-side.
    /// </summary>
    enum ECNetworkClientConnectType
    {
        CLIENT,
        SERVER
    }

    public class ECNetworkClient
    {
        private ECNetworkClientConnectType mConnectionType;
        private System.Timers.Timer mConnectTimeoutTimer;
        private volatile TcpClient mClient;
        private Thread mConnectThread;
        private Thread mReceiveThread;
        private Thread mSendThread;
        private volatile Queue<ECNetworkSendQueueObject> mSendQueue;
        private volatile bool mIsRunning;
        private long mLastUpdateTime = 0;
        private string mClientName;
        private volatile AesManaged mAesLocal;
        private volatile AesManaged mAesRemote;
        private volatile bool mKeysWereExchanged;
        //private ECServerLocatorClient mServerLocator;
        
        // ========================================================================================
        // Events
        // ========================================================================================

        /// <summary>
        /// Executed when there is an issue connecting to an ECNetworkServer.
        /// </summary>
        public Action onClientConnectError;
        /// <summary>
        /// Invoked when the _client connection is successful. (Executed after handshake)
        /// </summary>
        public Action onConnectSuccessful;
        /// <summary>
        /// Invoked when a timeout has occurred.
        /// </summary>
        public Action onConnectionTimeout;
        /// <summary>
        /// Invoked when a graceful disconnect has been accepted from other end.
        /// </summary>
        public Action onGracefulDisconnect;
        /// <summary>
        /// Client or server has requested graceful disconnect on the other end.
        /// </summary>
        public Action onRequestedDisconnect;
        /// <summary>
        /// Event for data being received.
        ///     Note: Excludes events from
        /// </summary>
        public ECClientReceivedDataEvent onDataReceived;


        // ========================================================================================
        // Init functions.
        // ========================================================================================

        private void baseInit()
        {
            mClient.SendTimeout = 300;
            mClient.ReceiveTimeout = 300;
            mSendQueue = new Queue<ECNetworkSendQueueObject>();
            mConnectTimeoutTimer = new System.Timers.Timer(ECNetworkOptions.CONNECTION_TIMEOUT_MILLIS);
            mConnectTimeoutTimer.Elapsed += timeoutTimerTick;
            mConnectTimeoutTimer.AutoReset = true;
            mAesLocal = ECCryptoHelper.InitializeAesManaged();
            mAesRemote = new AesManaged();
            mKeysWereExchanged = false;
        }

        /// <summary>
        /// Instantiate a new instance of ECNetworkClient
        /// </summary>
        /// <param name="_clientName">Name of client.</param>
        public ECNetworkClient(string _clientName)
        {
            mConnectionType = ECNetworkClientConnectType.CLIENT;
            mClient = new TcpClient();
            baseInit();
            setClientName(_clientName);
            //mServerLocator = new ECServerLocatorClient();
            //mServerLocator.onServerLocated += (string _ipAddr, int _port) =>
            //{
            //    Console.WriteLine("Server was located at " + _ipAddr + ":" + _port);
            //    this.connect(_ipAddr, _port);
            //};
            //mServerLocator.onServerNotLocated += () =>
            //{
            //    Console.WriteLine("Server was not located.");
            //};
        }

        /// <summary>
        /// Instantiate an instance of ECNetwork client with an instance of a TcpClient.
        /// Note: This is implimented on server side due to its accept policies.
        /// </summary>
        /// <param name="_client">Accepted TcpClient</param>
        public ECNetworkClient(TcpClient _client)
        {
            mConnectionType = ECNetworkClientConnectType.SERVER;
            mClient = _client;
            baseInit();
            mIsRunning = true;

            // starting threads
            mReceiveThread = new Thread(receiveLoop);
            mSendThread = new Thread(sendLoop);

            mReceiveThread.Start();
            mSendThread.Start();
            mConnectTimeoutTimer.Start();
        }

        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        // Accessors / Mutators
        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        /// <summary>
        /// Get the name of this client.
        /// </summary>
        /// <returns>Name of client.</returns>
        public string getClientName()
        {
            return mClientName;
        }

        /// <summary>
        /// Get the last time this object was updated.
        /// </summary>
        /// <returns>Last time this client received an update message from the server.</returns>
        public long getLastUpdateTime()
        {
            return mLastUpdateTime;
        }

        /// <summary>
        /// Set the name of this client.
        /// </summary>
        /// <param name="_clientName">New name of client</param>
        /// <exception cref="ArgumentException">Client name is not valid.</exception>
        public void setClientName(string _clientName)
        {
            if(_clientName == null || _clientName.Length == 0)
            {
                throw new ArgumentException("Argument is not valid", "_clientName");
            }
            mClientName = _clientName;
        }

        /// <summary>
        /// Set the last time the server was updated.
        /// </summary>
        /// <param name="_time"></param>
        private void setLastUpdateTime(long _time)
        {
            mLastUpdateTime = _time;
        }

        /// <summary>
        /// Attempt to find the server and connect asynchronously.
        /// </summary>
        public void locateAndConnectToServer()
        {
            //mServerLocator.locateServerAsync();
        }
        
        /// <summary>
        /// Connect to an ECNetworkServer Asynchronously.
        /// Note: This method should only be called from the _client-side. Server should never call
        ///       this as it starts unnecessary threads.
        /// </summary>
        /// <param name="_host">Host name.</param>
        /// <param name="_port">Host listening port.</param>
        public void connect(string _host, int _port)
        {
            mConnectThread = new Thread(() =>
            {
                try
                {
                    mClient.Connect(_host, _port);
                    if(mSendThread == null)
                        mSendThread = new Thread(sendLoop);
                    if(mReceiveThread == null)
                        mReceiveThread = new Thread(receiveLoop);
                    mIsRunning = true;
                    mSendThread.Start();
                    mReceiveThread.Start();
                    mConnectTimeoutTimer.Start();
                }
                catch
                {
                    if (onClientConnectError != null)
                        onClientConnectError();
                }
            });
            mConnectThread.Start();
        }

        /// <summary>
        /// Disconnect from server.
        /// Note: This is not a graceful disconnect. 
        /// See <see cref="requestDisconnect"/> for graceful disconnect.
        /// </summary>
        public void disconnect()
        {
            if (mIsRunning)
            {
                mIsRunning = false;
                mConnectTimeoutTimer.Stop();
                mClient.Close();
                mSendThread.Join();
                mReceiveThread.Join();
                mSendThread = null;
                mReceiveThread = null;
            }
        }

        /// <summary>
        /// Request a graceful disconnect. You must <see cref="on"/>
        /// See <see cref="onRequestedDisconnect" /> and <see cref="onGracefulDisconnect"/> for more details on events.
        /// </summary>
        [Obsolete("Not yet perfected. Please use the \"disconnect\" method to disconnect.")]
        public void requestDisconnect()
        {
            this.send(ECNetworkRequestType.REQUEST_DISCONNECT);
        }

        /// <summary>
        /// Send a message to the server.
        /// </summary>
        /// <param name="_type">Type of message being sent</param>
        /// <param name="_buffer">Data to be sent (optional)</param>
        public void send(ECNetworkRequestType _type, byte[] _buffer = null)
        {
            ECNetworkSendQueueObject nsq;
            nsq.requestType = _type;
            nsq.buffer = _buffer;
            mSendQueue.Enqueue(nsq);
        }
        /// <summary>
        /// Send a single byte to server.
        /// </summary>
        /// <param name="_data"></param>
        /// <returns>Success</returns>
        private bool send(byte _data)
        {
            if (mClient.Connected)
            {
                try
                {
                    mClient.GetStream().WriteByte(_data);
                    return true;
                }
                catch { }
            }
            return false;
        }

        /// <summary>
        /// Loop for receive thread.
        /// </summary>
        private void receiveLoop()
        {
            while (mIsRunning)
            {
                Thread.Sleep(50);
                try
                {
                    int firstByte = mClient.GetStream().ReadByte();
                    if (!Enum.IsDefined(typeof(ECNetworkRequestType), firstByte))
                        continue;
                    ECNetworkRequestType type = (ECNetworkRequestType)firstByte;

                    // checking for requests that require no body.
                    if (type == ECNetworkRequestType.HANDSHAKE_REQUEST_COMPLETE)
                    {
                        // ending parsing here.
                        if (onConnectSuccessful != null)
                            onConnectSuccessful();
                        continue;
                    }
                    else if (type == ECNetworkRequestType.HANDSHAKE_REQUEST_USERNAME)
                    {
                        send(ECNetworkRequestType.HANDSHAKE_RESPONSE_USERNAME, ECNetworkHelper.ASCIIStringToNetworkBuffer(mClientName));
                        continue;
                    }
                    else if (type == ECNetworkRequestType.REQUEST_DISCONNECT)
                    {
                        ECNetworkSendQueueObject obj;
                        obj.requestType = ECNetworkRequestType.ACCEPT_DISCONNECT;
                        obj.buffer = null;
                        sendNetworkSendQueueObjectSync(obj);
                        disconnect();
                        if (onRequestedDisconnect != null)
                            onRequestedDisconnect();
                        continue;
                    }
                    else if(type == ECNetworkRequestType.ACCEPT_DISCONNECT)
                    {
                        disconnect();
                        if (onGracefulDisconnect != null)
                            onGracefulDisconnect();
                        continue;
                    }

                    // getting request body
                    byte[] recSizeBuffer = new byte[sizeof(int)];
                    mClient.GetStream().Read(recSizeBuffer, 0, recSizeBuffer.Length);
                    int recSize = BitConverter.ToInt32(recSizeBuffer, 0);
                    Console.WriteLine("Detected size: " + recSize + " from message: " + type);

                    byte[] buffer = new byte[recSize]; // creating buffer
                    mClient.GetStream().Read(buffer, 0, recSize);
                    if (mKeysWereExchanged)
                        buffer = ECCryptoHelper.DecryptBytes(ref buffer, mAesRemote);

                    // checking request type
                    switch (type)
                    {
                        case ECNetworkRequestType.HANDSHAKE_RESPONSE_USERNAME:
                            mClientName = ECNetworkHelper.NetworkBufferToASCIIString(buffer);
                            send(ECNetworkRequestType.HANDSHAKE_SV_AES_KEY, mAesLocal.Key);
                            break;

                        // encryption handshakes
                        case ECNetworkRequestType.HANDSHAKE_SV_AES_KEY:
                            mAesRemote.Key = buffer;
                            send(ECNetworkRequestType.HANDSHAKE_CL_AES_KEY, mAesLocal.Key);
                            break;
                        case ECNetworkRequestType.HANDSHAKE_CL_AES_KEY:
                            mAesRemote.Key = buffer;
                            send(ECNetworkRequestType.HANDSHAKE_SV_AES_IV, mAesLocal.IV);
                            break;
                        case ECNetworkRequestType.HANDSHAKE_SV_AES_IV:
                            mAesRemote.IV = buffer;
                            send(ECNetworkRequestType.HANDSHAKE_CL_AES_IV, mAesLocal.IV);
                            mKeysWereExchanged = true;
                            break;
                        case ECNetworkRequestType.HANDSHAKE_CL_AES_IV:
                            mAesRemote.IV = buffer;
                            send(ECNetworkRequestType.HANDSHAKE_REQUEST_COMPLETE); // done with handshake.
                            mKeysWereExchanged = true;
                            // telling server this _client is done connecting.
                            if (onConnectSuccessful != null)
                                onConnectSuccessful();
                            break;

                        // other types of messages.
                        default:
                            if (onDataReceived != null)
                                onDataReceived(type, ref buffer);
                            break;
                    }
                }
                catch(Exception ex)
                {
                    break;
                }
            }
        }

        /// <summary>
        /// Send a NetworkSendQueueObject synchronously.
        /// </summary>
        /// <param name="nsq">Object to send</param>
        private void sendNetworkSendQueueObjectSync(ECNetworkSendQueueObject nsq)
        {
            if (!send((byte)nsq.requestType))
                return;
            if (nsq.buffer != null && nsq.buffer.Length > 0)
            {
                try
                {
                    byte[] buffer;
                    if (mKeysWereExchanged && nsq.requestType != ECNetworkRequestType.HANDSHAKE_CL_AES_IV)
                        buffer = ECCryptoHelper.EncryptBytes(ref nsq.buffer, mAesLocal);
                    else
                        buffer = nsq.buffer;
                    mClient.GetStream().Write(BitConverter.GetBytes(buffer.Length), 0, sizeof(int));
                    mClient.GetStream().Write(buffer, 0, buffer.Length);
                }
                catch { }
            }
        }
        
        /// <summary>
        /// Loop to send messages.
        /// </summary>
        private void sendLoop()
        {
            while (mIsRunning)
            {
                Thread.Sleep(50);
                while(mSendQueue.Count > 0)
                {
                    ECNetworkSendQueueObject nsq = mSendQueue.Dequeue();
                    sendNetworkSendQueueObjectSync(nsq);
                }
            }
        }

        /// <summary>
        /// Fired when a timeout occurrs.
        /// </summary>
        private void timeoutTimerTick(object sender, ElapsedEventArgs e)
        {
            try
            {
                if (!ECNetworkHelper.IsSocketConnected(mClient.Client))
                {
                    string msg = "Client " +
                        (mClientName == null ? "unknown" : mClientName) +
                        " disconnected from " +
                        (mClient.Client != null ? mClient.Client.RemoteEndPoint.ToString() : "unknown");
                    Console.WriteLine(msg);
                    disconnect();
                }
            }
            catch
            {
                string msg = "Client " +
                    (mClientName == null ? "unknown" : mClientName) +
                    " disconnected from " +
                    (mClient.Client != null ? mClient.Client.RemoteEndPoint.ToString() : "unknown");
                Console.WriteLine(msg);
                disconnect();
            }
        }
    }
}
