using System.Net.Sockets;
using System.Threading.Channels;

namespace ServerConsole.Core
{
    public abstract class AbstractClientSessionHandler : AbstractSessionHandler, IClientSessionHandler
    {
        private IClientSessionOwner? owner;
        public IClientSessionOwner? Owner => owner;

        public void Start(IClientSessionOwner owner, Guid id, Socket socket)
        {
            this.owner = owner;
            base.Start(id, socket);
        }

    }
}
