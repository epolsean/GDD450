using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class NetworkManagerScript : MonoBehaviour {

    public GameObject Canvas;
    public Button serverButton;
    public Button stopServer;
    public Text playerPanelDetectorText;
    public Text playerPanelLoaderText;
    public Text serversListText;
    public GameObject playerPanel;
    public GameObject inputPanel;
    public GameObject serverPanel;
    public InputField serverNameInput;
    public InputField serverDesInput;
    public EventSystem eventSystem;

    float startTime = 0;
    int count = 3;
    string gameName = "GDD450_Test_LAN_Game";
    string serverName = "Default Server Name";
    string serverDes = "This is a 'Insert-Final-Game-Name-Here' LAN game";
    bool refreshing;
    bool secondPlayerConnected;
    HostData[] hostData;


    void Start()
    {
        serversListText.text = "Page 1 of 1 / Total Servers: 0";
        if (Application.loadedLevelName == "LANLobby")
        {
            inputPanel.SetActive(false);
            playerPanel.SetActive(false);
            stopServer.gameObject.SetActive(false);
        }
        MasterServer.RequestHostList(gameName);
        refreshing = true;
    }

    void Update()
    {
        if (Application.loadedLevelName == "LANLobby")
        {
            if (refreshing)
            {
                if (GameObject.FindGameObjectsWithTag("server").Length > 0)
                {
                    GameObject[] oldButtons = GameObject.FindGameObjectsWithTag("server");
                    foreach (GameObject i in oldButtons)
                        GameObject.Destroy(i);
                }
                else
                {
                    if (MasterServer.PollHostList().Length > 0)
                    {
                        refreshing = false;
                        //Debug.Log(MasterServer.PollHostList().Length);
                        int page = MasterServer.PollHostList().Length % 1 + 1;
                        serversListText.text = "Page 1 of " + page + " / Total Servers: " + MasterServer.PollHostList().Length;
                        hostData = MasterServer.PollHostList();
                        for (int i = 0; i < hostData.Length; i++)
                        {
                            Button newServer = (Button)Instantiate(serverButton);
                            newServer.transform.parent = serverPanel.transform;
                            newServer.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);
                            newServer.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
                            newServer.GetComponent<RectTransform>().anchorMin = new Vector2(0.0125f, 0.9f - 0.9f * i);
                            newServer.GetComponent<RectTransform>().anchorMax = new Vector2(0.9875f, 0.985f - 0.9f * i);
                            newServer.GetComponent<serverButtonScript>().serverName.text = hostData[i].gameName;
                            newServer.GetComponent<serverButtonScript>().serverDes.text = hostData[i].comment;
                            newServer.GetComponent<serverButtonScript>().hostData = hostData[i];
                        }
                    }
                }
            }
            if (playerPanel.activeInHierarchy == true)
            {
                if (!secondPlayerConnected)
                {
                    if (startTime == 0)
                    {
                        startTime = Time.time + .5f;
                    }
                    else if (startTime <= Time.time)
                    {
                        if (playerPanelDetectorText.text == "Both Players are connected")
                            networkView.RPC("setPlayerPanelText", RPCMode.AllBuffered, "Waiting for other players");
                        if (playerPanelDetectorText.text != "Waiting for other players......")
                            networkView.RPC("setPlayerPanelText", RPCMode.AllBuffered, (playerPanelDetectorText.text + "."));
                        else
                            networkView.RPC("setPlayerPanelText", RPCMode.AllBuffered, "Waiting for other players");
                        startTime = Time.time + .5f;
                    }
                }
                else
                {
                    networkView.RPC("setPlayerPanelText", RPCMode.AllBuffered, "Both Players are connected");
                    if (startTime == 0)
                    {
                        startTime = Time.time + 1;
                    }
                    else if (startTime <= Time.time)
                    {
                        if (playerPanelLoaderText.text != "Loading level in: 0")
                        {
                            networkView.RPC("countdownTimer", RPCMode.AllBuffered, "Loading level in: ", count);
                            count--;
                        }
                        else
                            Application.LoadLevel("TestingHexTiles");
                        startTime = Time.time + 1f;
                    }
                }
            }
        }
    }

    [RPC]
    void setPlayerPanelText(string pPT)
    {
        playerPanelDetectorText.text = pPT;
    }

    [RPC]
    void setSecondPlayerCheck(bool sPC)
    {
        secondPlayerConnected = sPC;
        playerPanelLoaderText.gameObject.SetActive(true);
        startTime = 0;
    }

    [RPC]
    void countdownTimer(string phrase, int counter)
    {
        playerPanelLoaderText.text = phrase + counter;
    }

    public void onStartServerButtonClick()
    {
        inputPanel.SetActive(true);
    }

    public void startServer()
    {
        if (serverNameInput.text.text != "Input")
            serverName = serverNameInput.text.text;
        if (serverDesInput.text.text != "Input")
            serverDes = serverDesInput.text.text;
        Network.InitializeServer(2, 25000, !Network.HavePublicAddress());
        MasterServer.RegisterHost(gameName, serverName, serverDes);
        inputPanel.SetActive(false);
        playerPanel.SetActive(true);
        stopServer.gameObject.SetActive(true);
    }

    public void refreshHostList()
    {
        if (Network.isClient || Network.isServer)
        {
            Network.Disconnect();
            secondPlayerConnected = false;
        }
        MasterServer.RequestHostList(gameName);
        inputPanel.SetActive(false);
        playerPanel.SetActive(false);
        playerPanelLoaderText.gameObject.SetActive(true);
        refreshing = true;
    }

    //Messages
    void OnServerInitialized()
    {
        Debug.Log("Server Initialized");
    }

    void OnConnectedToServer()
    {
        if (GameObject.FindGameObjectsWithTag("server").Length > 0)
        {
            GameObject[] oldButtons = GameObject.FindGameObjectsWithTag("server");
            foreach (GameObject i in oldButtons)
                GameObject.Destroy(i);
        }
        if(Network.connections.Length >= 1)
            networkView.RPC("setSecondPlayerCheck", RPCMode.AllBuffered, true);
        playerPanel.SetActive(true);
    }

    void OnDisconnectedFromServer()
    {
        if(Application.loadedLevelName != "LANLobby")
            Application.LoadLevel("LANLobby");
    }

    void OnMasterServerEvent(MasterServerEvent mse)
    {
        if (mse == MasterServerEvent.RegistrationSucceeded)
        {
            Debug.Log("Registered Server");
            MasterServer.RequestHostList(gameName);
            refreshing = true;
        }
    }

    public void StopServer()
    {
        Network.Disconnect();
        playerPanel.SetActive(false);
        StartCoroutine(disconnectRefresh());
    }

    IEnumerator disconnectRefresh()
    {
        MasterServer.RequestHostList(gameName);
        //refreshing = true;
        yield return new WaitForSeconds(.35f);
        stopServer.gameObject.SetActive(false);
        MasterServer.RequestHostList(gameName);
        refreshing = true;
    }
}
