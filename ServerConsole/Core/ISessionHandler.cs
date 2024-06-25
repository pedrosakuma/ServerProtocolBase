namespace ServerConsole.Core
{
    public interface ISessionHandler : IDisposable
    {
        Guid Id { get; }
        void Send(IFormattableMessage message);
    }
}