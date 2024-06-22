using UnityEngine;

public class Game : MonoBehaviour
{
    public static Game instance;
    private ProtocolManager protocolManager;
    private ProtocolManager ProtocolManager => protocolManager;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        protocolManager = new ProtocolManager();
    }

    public void LoadScene()
    {

    }
}
