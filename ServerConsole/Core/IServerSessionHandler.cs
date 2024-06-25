using System.Net.Sockets;

namespace ServerConsole.Core
{
    public interface IServerSessionHandler : ISessionHandler
    {
        void Start(IServerSessionOwner owner, Guid id, Socket socket);
    }
}
