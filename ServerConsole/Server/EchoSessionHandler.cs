
using ServerConsole.Core;
using System.Diagnostics.CodeAnalysis;
using System.Net.Sockets;

namespace ServerConsole.Server
{
    public class EchoSessionHandler : AbstractExecutorSessionHandler, IMessageResolver, IServerSessionHandler
    {
        private IServerSessionOwner? owner;
        public bool TryGetMessageParser(int messageTypeId, [NotNullWhen(true)] out IMessageParser? parser)
        {
            switch (messageTypeId)
            {
                case EchoMessageParser.MessageType:
                    parser = new EchoMessageParser();
                    return true;
                default:
                    parser = null;
                    return false;
            }
        }

        public override IMessageResolver CreateMessageResolver()
        {
            return this;
        }

        protected override void ClientTooSlow()
        {
        }

        protected override void MessageTypeParserNotFound(int messageType)
        {
        }

        protected override async Task ReceivedAsync(IMessage message)
        {
            switch (message)
            {
                case EchoMessage echoMessage:
                    await HandleEchoMessage(echoMessage);
                    break;
            }
        }

        private async Task HandleEchoMessage(EchoMessage echoMessage)
        {
            owner.SendToAll(echoMessage);
            //await SendAsync(echoMessage);
        }

        public void Start(IServerSessionOwner owner, Guid id, Socket socket)
        {
            this.owner = owner;
            base.Start(id, socket);
        }
    }
}
