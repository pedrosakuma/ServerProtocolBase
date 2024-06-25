using System.Net.Sockets;
using ServerConsole.Core;

namespace ServerConsole.Server
{
    public abstract class AbstractExecutorSessionHandler : AbstractServerSessionHandler
    {
        private Task? executor;

        public override void Start(IServerSessionOwner owner, Guid id, Socket socket)
        {
            base.Start(owner, id, socket);
            executor = Task.Run(Execute);
        }

        protected abstract Task ReceivedAsync(IMessage message);

        private async Task Execute()
        {
            while (await WaitToReadAsync())
            {
                if (TryRead(out var message))
                    await ReceivedAsync(message);
            }
        }
    }
}
