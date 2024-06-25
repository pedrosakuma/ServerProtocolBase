using System.Diagnostics.CodeAnalysis;

namespace ServerConsole.Core
{
    public interface IMessageResolver
    {
        bool TryGetMessageParser(int messageTypeId, [NotNullWhen(true)] out IMessageParser? parser);
    }
}