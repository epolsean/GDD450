using UnityEngine;
using System.Collections;

public class NetworkLevelLoader : MonoBehaviour {
    
    bool player1Created = false;
    bool player2Created = false;
    string loadedLevel = "TestingTileMovement_Ryan";
    static bool player1Connected = false;
    static bool player2Connected = false;
    bool bothConnected = false;
    bool onLine = false;

    void Start()
    {
        if (!Network.isClient && !Network.isServer)
        {
            
        }
        else
        {
            onLine = true;
            if (Network.isClient && !Network.isServer)
                networkView.RPC("playerConnect", RPCMode.AllBuffered, 2, true);
            else if (!Network.isClient && Network.isServer)
                networkView.RPC("playerConnect", RPCMode.AllBuffered, 1, true);
        }
    }

    void Update () 
    {
        if (onLine)
        {
            if (bothConnected != true)
                networkView.RPC("checkClientLevels", RPCMode.AllBuffered);
        }
    }

    [RPC]
    void checkClientLevels()
    {
        if (Network.isServer && Application.loadedLevelName == loadedLevel)
            networkView.RPC("playerConnect", RPCMode.AllBuffered, 1, true);
        if (Network.isClient && Application.loadedLevelName == loadedLevel)
            networkView.RPC("playerConnect", RPCMode.AllBuffered, 2, true);
        if (player1Connected == true && player2Connected == true)
            bothConnected = true;
    }

    [RPC]
    void playerConnect(int player, bool isConnected)
    {
        if (player == 1)
            player1Connected = isConnected;
        else if (player == 2)
            player2Connected = isConnected;
    }
}
