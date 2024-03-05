using ServerProtocol.Server.Protocols.Others;

namespace ServerProtocol.Server.Protocols.Base
{
    public abstract class Protocol
    {
        public enum ProtocolID : byte
        { Login }
        public readonly ProtocolID protocolID;
        protected Protocol(ProtocolID protocolID)
        {
            this.protocolID = protocolID;
        }
        public abstract void ParseMessage(int index, ProtocolReader packet);
        public abstract void InitProtocolHandler();
        protected delegate void ProtocolHandler(int _fromClient, ProtocolReader packet);
        protected ProtocolWriter NewMessage()
        {
            ProtocolWriter protocolWriter = new ProtocolWriter();
            protocolWriter.WriteByte((byte)protocolID);
            return protocolWriter;
        }
    }
}