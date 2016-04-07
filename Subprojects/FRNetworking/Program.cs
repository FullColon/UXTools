using System;
using System.Collections.Generic;
using EC.Networking;

/// <summary>
/// Client/server testing application.
/// </summary>

namespace FRNetworking
{
    class Program
    {
        private static List<ECNetworkClient> _clients = new List<ECNetworkClient>();
        private static ECNetworkServer _server;
        static void Main(string[] args)
        {
            string type = Console.ReadLine();
            if (type.Equals("client"))
            {
                startClient();
            }
            else if (type.Equals("server"))
            {
                startServer();
            }
            Console.ReadKey();
            if (type.Equals("client"))
            {
                foreach(ECNetworkClient cli in _clients)
                {
                    cli.disconnect();
                    Console.WriteLine("Client disconnected: " + cli.getClientName());
                }
            }
            else if (type.Equals("server"))
            {
                _server.stop();
            }
        }

        private static void startClient()
        {
            for(int i = 0; i < 15; ++i)
            {
                ECNetworkClient cli = new ECNetworkClient("Eddie_" + i);
                _clients.Add(cli);
                cli.onDataReceived += (ECNetworkRequestType type, ref byte[] data) =>
                {
                    Console.WriteLine("Client " + cli.getClientName() + " received data: " + ECNetworkHelper.NetworkBufferToASCIIString(data));
                };
                cli.connect("127.0.0.1", 4414);
            }
        }
        private static void startServer()
        {
            _server = new ECNetworkServer(4414);
            _server.onServerStartError += () =>
            {
                Console.WriteLine("Error starting server.");
            };
            _server.onClientConnected += (ECNetworkClient client) =>
            {
                Console.WriteLine("Client connected with name: " + client.getClientName());
                client.send(ECNetworkRequestType.OTHER, ECNetworkHelper.ASCIIStringToNetworkBuffer(Guid.NewGuid().ToString()));
            };
            Console.WriteLine("Starting server...");
            _server.start();
        }
    }
}
