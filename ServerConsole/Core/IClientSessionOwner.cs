namespace ServerConsole.Core
{
    public interface IClientSessionOwner
    {
        void Disconnected(Guid key);
    }
}
