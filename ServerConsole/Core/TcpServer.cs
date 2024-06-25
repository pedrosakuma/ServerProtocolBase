using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;

namespace ServerConsole.Core
{
    internal class TcpServer<SessionHandler> : IDisposable, IServerSessionOwner
        where SessionHandler : IServerSessionHandler, new()
    {
        private readonly Socket socket;
        private readonly IPAddress address;
        private readonly int port;
        private readonly int listenerBacklog;
        private readonly ConcurrentDictionary<Guid, ISessionHandler> sessions;
        private Task acceptorTask;

        public TcpServer(IPAddress address, int port, int listenerBacklog)
        {
            socket = new Socket(SocketType.Stream, ProtocolType.Tcp);
            sessions = new ConcurrentDictionary<Guid, ISessionHandler>();
            this.address = address;
            this.port = port;
            this.listenerBacklog = listenerBacklog;
        }

        public void Dispose()
        {
            socket.Shutdown(SocketShutdown.Both);
            foreach (var session in sessions.Values)
                session.Dispose();
            sessions.Clear();
            acceptorTask.Dispose();
            socket.Dispose();
        }

        public void Start(CancellationToken token)
        {
            socket.Bind(new IPEndPoint(address, port));
            socket.Listen(listenerBacklog);
            acceptorTask = Task.Run(AcceptConnections, token);
        }

        public void SendToAll(IFormattableMessage message)
        {
            foreach (var session in sessions.Values)
            {
                session.Send(message);
            }
        }

        public void SendTo(Guid id, IFormattableMessage message)
        {
            if (sessions.TryGetValue(id, out var session))
                session.Send(message);
        }

        public void SendToAllExcept(Guid id, IFormattableMessage message)
        {
            foreach (var session in sessions.Values)
            {
                if (session.Id != id)
                    session.Send(message);
            }
        }

        private async Task AcceptConnections()
        {
            while (!acceptorTask.IsCanceled)
            {
                var client = await socket.AcceptAsync();
                var key = Guid.NewGuid();
                var session = new SessionHandler();
                sessions.TryAdd(key, session);
                session.Start(this, key, client);
            }
        }

        public void Disconnected(Guid key)
        {
            if (sessions.TryRemove(key, out var handler))
            {
                handler.Dispose();
            }
        }
    }
}
