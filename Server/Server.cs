using System.Net;
using System.Net.Sockets;

namespace ServerProtocol.Server
{
    public class Server
    {
        public static Dictionary<int, Client> clients = new Dictionary<int, Client>();
        public static int Port = 26950;
        private static TcpListener tcpListener = new TcpListener(IPAddress.Any, Port);

        public static void Start()
        {

            Console.WriteLine("Starting server...");
            tcpListener.Start();
            tcpListener.BeginAcceptTcpClient(TCPConnectCallback, null);

            Console.WriteLine($"Server started on port {Port}.");
        }

        private static void TCPConnectCallback(IAsyncResult _result)
        {
            TcpClient _client = tcpListener.EndAcceptTcpClient(_result);
            tcpListener.BeginAcceptTcpClient(TCPConnectCallback, null);
            Console.WriteLine($"Incoming connection from {_client.Client.RemoteEndPoint}...");

            int cIndex = 1;

            while (clients.ContainsKey(cIndex))
                cIndex++;

            Client newClient = new Client(cIndex, _client);
            newClient.tcp.Connect();

            clients.Add(cIndex, newClient);
        }
    }
}