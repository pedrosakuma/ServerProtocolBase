using System.Diagnostics;
using ServerProtocol.Server.Protocols.Base;

namespace ServerProtocol.Server.Protocols
{
    public class ProtocolManager
    {
        private static Dictionary<Protocol.ProtocolID, Protocol> protocols = new Dictionary<Protocol.ProtocolID, Protocol>();

        public static void InitProtocols()
        {

            protocols = new Dictionary<Protocol.ProtocolID, Protocol>();
            RegisterProtocol(new Protocol[] {
               new LoginProtocol(),
            });

        }

        public static void HandleNetworkInformation(int index, byte[] packet)
        {
            using (ProtocolReader reader = new ProtocolReader(packet))
            {
                byte protocolID = reader.ReadByte();
                GetProtocol((Protocol.ProtocolID)protocolID)?.ParseMessage(index, reader);
            }
        }

        public static void HandleNetworkInformation(int index, ProtocolReader packet)
        {
            byte protocolID = packet.ReadByte();
            GetProtocol((Protocol.ProtocolID)protocolID)?.ParseMessage(index, packet);
        }

        public static Protocol? GetProtocol(Protocol.ProtocolID protoID)
        {
            if (!protocols.ContainsKey(protoID))
            {
                return null;
            }
            return protocols[protoID];
        }

        private static void RegisterProtocol(params Protocol[] passedProtocols)
        {
            foreach (Protocol current in passedProtocols)
            {
                protocols.Add(current.protocolID, current);
                current.InitProtocolHandler();
            }
        }
    }
}