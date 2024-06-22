
public abstract class Protocol
{
    public enum ProtocolID : byte
    { Login }
    public readonly ProtocolID protocolID;
    private readonly bool enabled;

    protected Protocol(ProtocolID protocolID)
    {
        this.protocolID = protocolID;
        enabled = true;
    }
    public abstract void ParseMessage(int index, ProtocolReader packet);
    public abstract void InitProtocolHandler();
    protected delegate void ProtocolHandler(int _fromClient, ProtocolReader packet);
}