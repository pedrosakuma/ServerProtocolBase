using ServerConsole.Client;
using ServerConsole.Core;
using ServerConsole.Server;
using System.Net;

namespace ServerConsole
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            CancellationTokenSource source = new CancellationTokenSource();
            var token = source.Token;
            using TcpServer<EchoSessionHandler> server = new TcpServer<EchoSessionHandler>(IPAddress.Loopback, 8080, 100);
            server.Start(token);

            using TcpClient<ConsoleWriterHandler> client = new TcpClient<ConsoleWriterHandler>(IPAddress.Loopback, 8080);
            await client.ConnectAsync();
            Timer timer = new Timer(o =>
            {
                client.Session.Send(new EchoMessage("Hello!"));
            }, null, 1000, 1000);

            var clientTask = Task.Run(() =>
            {
                while (!token.IsCancellationRequested)
                    Update(client);
            });
            Console.ReadLine();
            source.Cancel();
            await clientTask;
        }

        private static void Update(TcpClient<ConsoleWriterHandler> client)
        {
            if (client.Session.TryRead(out var message))
            {
                switch (message)
                {
                    case EchoMessage echoMessage:
                        Console.WriteLine($"Received message EchoMessage {{ Text={echoMessage.Text} }}");
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
