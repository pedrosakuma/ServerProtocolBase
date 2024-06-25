namespace ServerConsole.Core
{
    public interface IServerSessionOwner
    {
        void Disconnected(Guid key);
        void SendTo(Guid id, IFormattableMessage message);
        void SendToAll(IFormattableMessage message);
        void SendToAllExcept(Guid id, IFormattableMessage message);
    }
}
