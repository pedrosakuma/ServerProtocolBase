using System.Net;
using System.Net.Sockets;

namespace ServerConsole.Core
{
    internal class TcpClient<SessionHandler> : IDisposable, IClientSessionOwner
        where SessionHandler : IClientSessionHandler, new()
    {
        private readonly Socket socket;
        private readonly IPAddress address;
        private readonly int port;
        private SessionHandler session;

        public SessionHandler Session => session;

        public TcpClient(IPAddress address, int port)
        {
            socket = new Socket(SocketType.Stream, ProtocolType.Tcp);
            this.address = address;
            this.port = port;
        }

        public void Dispose()
        {
            socket.Shutdown(SocketShutdown.Both);
            socket.Dispose();
        }

        public async Task ConnectAsync()
        {
            await socket.ConnectAsync(address, port);
            session = new SessionHandler();
            session.Start(this, Guid.Empty, socket);
        }

        public void Connect()
        {
            socket.Connect(address, port);
            session = new SessionHandler();
            session.Start(this, Guid.Empty, socket);
        }

        public void Disconnected(Guid key)
        {
            // Reconnection logic
        }
    }
}
