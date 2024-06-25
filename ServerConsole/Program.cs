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
            using TcpServer<EchoSessionHandler> server = new TcpServer<EchoSessionHandler>(IPAddress.Loopback, 8080, 100);
            server.Start(CancellationToken.None);

            using TcpClient<ConsoleWriterHandler> client = new TcpClient<ConsoleWriterHandler>(IPAddress.Loopback, 8080);
            await client.ConnectAsync();
            Timer timer = new Timer(o =>
            {
                client.Session.SenderChannel.WriteAsync(new EchoMessage("Hello!")).AsTask().Wait();
            }, null, 1000, 1000);

            while (await client.Session.ReceiverChannel.WaitToReadAsync())
            {
                if (client.Session.ReceiverChannel.TryRead(out IMessage? message))
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
            Console.ReadLine();
        }
    }
}
