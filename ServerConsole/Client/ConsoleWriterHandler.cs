using ServerConsole.Core;
using System.Diagnostics.CodeAnalysis;
using System.Net.Sockets;

namespace ServerConsole.Client
{
    internal class ConsoleWriterHandler : AbstractClientSessionHandler, IMessageResolver
    {

        public override IMessageResolver CreateMessageResolver()
        {
            return this;
        }

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

        protected override void ClientTooSlow()
        {
        }

        protected override void MessageTypeParserNotFound(int messageType)
        {
        }
    }
}
