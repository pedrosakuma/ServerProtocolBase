
using System.Collections.Generic;
using UnityEngine;

public class ProtocolManager
{
    private readonly Connection connection;
    private static Dictionary<Protocol.ProtocolID, Protocol> protocols = new Dictionary<Protocol.ProtocolID, Protocol>();

    public ProtocolManager()
    {
        connection = new Connection();
        connection.OnConnectionDropped += OnConnectionDropped;
        connection.Connect("", 29650);

        protocols = new Dictionary<Protocol.ProtocolID, Protocol>();
        RegisterProtocol(new Protocol[] {
               new LoginProtocol(),
        });
    }

    public static Protocol GetProtocol(Protocol.ProtocolID protoID)
    {
        if (!protocols.ContainsKey(protoID))
        {
            return null;
        }
        return protocols[protoID];
    }

    private void OnConnectionDropped(string message)
    {
        Debug.Log($"OnConnectionDropped: {message}");
        Game.instance.LoadScene();
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
