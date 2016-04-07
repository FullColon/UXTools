namespace EC.Networking
{
    /// <summary>
    /// Delegate method to be executed when a specific client event is fired.
    /// </summary>
    /// <param name="_client"></param>
    public delegate void ECNetworkClientEvent(ECNetworkClient _client);

    /// <summary>
    /// Delegate method to be invoked when a client receives data.
    /// </summary>
    /// <param name="_type">Type of request that was made.</param>
    /// <param name="_data"></param>
    public delegate void ECClientReceivedDataEvent(ECNetworkRequestType _type, ref byte[] _data);

    /// <summary>
    /// A client that is connected to the server received data. This exists to solve the problem
    /// of not knowing which client received data on server-side.
    /// </summary>
    /// <param name="_client">Client that received data</param>
    /// <param name="_type">Type of message being received</param>
    /// <param name="_data">Decrypted data that was received</param>
    public delegate void ECServerReceivedClientDataEvent(ECNetworkClient _client, ECNetworkRequestType _type, ref byte[] _data);

    /// <summary>
    /// Different request types over network.
    /// This will tell us which response to use.
    /// </summary>
    public enum ECNetworkRequestType
    {
        /// <summary>
        /// This request type is attached when the server is requesting the username of the client.
        /// </summary>
        HANDSHAKE_REQUEST_USERNAME = 1,

        /// <summary>
        /// This request type is attached when the client is responding to the username request in handshake.
        /// </summary>
        HANDSHAKE_RESPONSE_USERNAME,

        /// <summary>
        /// This request type is attached when the server tells the client that the handshake is complete.
        /// </summary>
        HANDSHAKE_REQUEST_COMPLETE,

        /// <summary>
        /// Server sends its encryption key.
        /// </summary>
        HANDSHAKE_SV_AES_KEY,

        /// <summary>
        /// Server sends its encryption initialization vector.
        /// </summary>
        HANDSHAKE_SV_AES_IV,

        /// <summary>
        /// Client sends its encryption key.
        /// </summary>
        HANDSHAKE_CL_AES_KEY,

        /// <summary>
        /// Client sends its encryption initialization vector.
        /// </summary>
        HANDSHAKE_CL_AES_IV,

        /// <summary>
        /// Tell the server that we are disconnecting.
        /// </summary>
        REQUEST_DISCONNECT,

        /// <summary>
        /// Server is accepting a client disconnect.
        /// </summary>
        ACCEPT_DISCONNECT,

        /// <summary>
        /// Requests with the request type of "OTHER" will be passed off 
        /// to a delegate implementation to be handled there.
        /// </summary>
        OTHER
    }

    /// <summary>
    /// Options used for server/client communication.
    /// </summary>
    public static class ECNetworkOptions
    {
        /// <summary>
        /// Send a ping request to each client every _____ millis.
        /// </summary>
        public const double SERVER_PING_EVERY = 200;

        /// <summary>
        /// Client timeout after ____ millis.
        /// </summary>
        public const double CONNECTION_TIMEOUT_MILLIS = 350;

        /// <summary>
        /// Wait this long for successful handshake.
        /// </summary>
        public const double HANDSHAKE_TIMEOUT_MILLIS = 3000;
    }
}
