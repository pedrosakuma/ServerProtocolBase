using System.Net.Sockets;

namespace ServerConsole.Core
{
    public abstract class AbstractExecutorSessionHandler : AbstractSessionHandler
    {
        private Task? executor;

        public override void Start(Guid id, Socket client)
        {
            base.Start(id, client);
            this.executor = Task.Run(Execute);
        }

        protected abstract Task ReceivedAsync(IMessage message);

        private async Task Execute()
        {
            var reader = ReceiverChannel;
            while (await reader.WaitToReadAsync())
            {
                if (reader.TryRead(out var message))
                {
                    await ReceivedAsync(message);
                }
            }
        }
    }
}
