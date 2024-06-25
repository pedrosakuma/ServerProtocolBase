namespace ServerConsole.Core
{
    public interface IMessageParser
    {
        IMessage Parse(ReadOnlySpan<byte> buffer);
    }
}