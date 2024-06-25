
using ServerConsole.Core;
using System.Text;

namespace ServerConsole
{
    internal class EchoMessageParser : IMessageParser
    {
        public const int MessageType = 1;

        public IMessage Parse(ReadOnlySpan<byte> buffer)
        {
            return new EchoMessage(Encoding.UTF8.GetString(buffer));
        }
    }
}