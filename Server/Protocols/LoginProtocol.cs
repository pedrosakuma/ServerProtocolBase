using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;

using ServerProtocol.Server.Protocols.Base;
using ServerProtocol.Server.Protocols.Others;

namespace ServerProtocol.Server.Protocols
{
    public enum LoginProtocolRequest : ushort
    {
        init,
        test
    }
    public class LoginProtocol : Protocol
    {
        private static Dictionary<ushort, ProtocolHandler> protocolHandlers = new Dictionary<ushort, ProtocolHandler>();
        public LoginProtocol() : base(ProtocolID.Login) { }

        public static LoginProtocol? GetProtocol()
        {
            return ProtocolManager.GetProtocol(ProtocolID.Login) as LoginProtocol;
        }

        public override void ParseMessage(int index, ProtocolReader packet)
        {
            ushort protocolType = packet.ReadUShort();
            protocolHandlers[protocolType](index, packet);
        }

        public override void InitProtocolHandler()
        {
            protocolHandlers = new Dictionary<ushort, ProtocolHandler>()
            {
                {(ushort)LoginProtocolRequest.init, Init},
                {(ushort)LoginProtocolRequest.test, Test}
            };
        }
        public static void Test(int id, ProtocolReader packet)
        {
            Console.WriteLine("Chegou no Teste");
        }
        public static void Init(int id, ProtocolReader packet)
        {
            Console.WriteLine("Chegou no Init");
        }
    }
}