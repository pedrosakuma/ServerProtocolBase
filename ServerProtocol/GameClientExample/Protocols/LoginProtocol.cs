using System;
using System.Collections.Generic;

public enum LoginProtocolRequest : ushort
{
    connect,
}
public class LoginProtocol : Protocol
{
    private static Dictionary<ushort, ProtocolHandler> protocolHandlers = new Dictionary<ushort, ProtocolHandler>();
    public LoginProtocol() : base(ProtocolID.Login)
    {
    }

    public static LoginProtocol GetProtocol()
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
                {(ushort)LoginProtocolRequest.connect, Connect}
            };
    }

    public static void Connect(int id, ProtocolReader packet)
    {
        Console.WriteLine("Chegou no Init");
    }
}