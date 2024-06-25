using System.Net.Sockets;

namespace ServerConsole.Core
{
    public interface IClientSessionHandler : ISessionHandler
    {
        void Start(IClientSessionOwner owner, Guid id, Socket socket);

    }
}
