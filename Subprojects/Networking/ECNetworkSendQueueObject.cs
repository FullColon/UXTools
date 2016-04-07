namespace EC.Networking
{
    /// <summary>
    /// Object containing data necessary for sending data between client/server.
    /// </summary>
    public struct ECNetworkSendQueueObject
    {
        public ECNetworkRequestType requestType;
        public byte[] buffer;
    }
}
