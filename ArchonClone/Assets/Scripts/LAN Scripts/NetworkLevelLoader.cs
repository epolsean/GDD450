using UnityEngine;
using System.Collections;

public class NetworkLevelLoader : MonoBehaviour {

    public GameObject spawn1;
    public GameObject spawn2;
    public GameObject player1;
    public GameObject player2;

    bool player1Created = false;
    bool player2Created = false;
    string loadedLevel = "AlienBattleSmall";
    static bool player1Connected = false;
    static bool player2Connected = false;
    bool bothConnected = false;
    bool onLine = false;

    void Start()
    {
        if (!Network.isClient && !Network.isServer)
        {
            GameObject p1 = (GameObject)Instantiate(player1, spawn1.transform.position, spawn1.transform.rotation);
            GameObject p2 = (GameObject)Instantiate(player2, spawn2.transform.position, spawn2.transform.rotation);
            p1.transform.parent = GameObject.Find("BattleSceneAdditive").transform;
            p2.transform.parent = GameObject.Find("BattleSceneAdditive").transform;
        }
        else
        {
            onLine = true;
        }
    }

    void Update () 
    {
        if (onLine)
        {
            if (bothConnected != true)
                networkView.RPC("checkClientLevels", RPCMode.AllBuffered);
            else
                networkView.RPC("loadPlayers", RPCMode.AllBuffered);
        }
    }

    [RPC]
	void loadPlayers () {
        if (Network.isServer && player1Created == false)
        {
            GameObject newPlayer1 = (GameObject)Network.Instantiate(player1, spawn1.transform.position, player1.transform.rotation, 1);
            newPlayer1.transform.parent = GameObject.Find("BattleSceneAdditive").transform;
            player1Created = true;
        }
        else if (Network.isClient && player2Created == false)
        {
            GameObject newPlayer2 = (GameObject)Network.Instantiate(player2, spawn2.transform.position, player2.transform.rotation, 1);
            newPlayer2.transform.parent = GameObject.Find("BattleSceneAdditive").transform;
            player2Created = true;
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
