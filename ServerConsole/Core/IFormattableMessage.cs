namespace ServerConsole.Core
{
    public interface IFormattableMessage : IMessage
    {
        int FormatMessage(Span<byte> bytes);
    }
}