using System.Net.Sockets;

namespace ServerConsole.Core
{
    public abstract class AbstractServerSessionHandler : AbstractSessionHandler, IServerSessionHandler
    {
        private IServerSessionOwner? owner;
        public IServerSessionOwner? Owner => owner;

        public virtual void Start(IServerSessionOwner owner, Guid id, Socket socket)
        {
            this.owner = owner;
            base.Start(id, socket);
        }
    }
}
