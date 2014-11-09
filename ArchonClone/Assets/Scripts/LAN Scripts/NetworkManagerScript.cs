using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class NetworkManagerScript : MonoBehaviour {

    public GameObject Canvas;
    public Button serverButton;
    public Text serverButtonText;
    public GameObject playerPanel;
    public Text playerPanelDetectorText;
    public Text playerPanelLoaderText;
    public GameObject inputPanel;
    public InputField serverNameInput;
    public EventSystem eventSystem;

    float startTime = 0;
    int count = 3;
    string serverName = "Default Server Name";
    string gameName = "GDD450_Test_LAN_Game";
    bool refreshing;
    bool secondPlayerConnected;
    HostData[] hostData;


    void Start()
    {
        if (Application.loadedLevelName == "LANLobby")
        {
            inputPanel.SetActive(false);
            playerPanel.SetActive(false);
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
                        Debug.Log(MasterServer.PollHostList().Length);
                        hostData = MasterServer.PollHostList();
                        for (int i = 0; i < hostData.Length; i++)
                        {
                            Button newServer = (Button)Instantiate(serverButton);
                            newServer.transform.parent = Canvas.transform;
                            newServer.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);
                            newServer.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
                            newServer.GetComponent<RectTransform>().anchorMin = new Vector2(0.25f, 0.8f - 0.125f * i);
                            newServer.GetComponent<RectTransform>().anchorMax = new Vector2(0.45f, 0.9f - 0.125f * i);
                            newServer.GetComponent<serverButtonScript>().buttonText.text = hostData[i].gameName;
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
        if (GameObject.FindGameObjectsWithTag("server").Length > 0)
        {
            GameObject[] oldButtons = GameObject.FindGameObjectsWithTag("server");
            foreach (GameObject i in oldButtons)
                GameObject.Destroy(i);
        }
        inputPanel.SetActive(true);
    }

    public void startServer()
    {
        if (serverNameInput.text.text != "Input")
            serverName = serverNameInput.text.text;
        Network.InitializeServer(2, 25000, !Network.HavePublicAddress());
        MasterServer.RegisterHost(gameName, serverName, "This is a 'Insert-Final-Game-Name-Here' LAN game");
        inputPanel.SetActive(false);
        playerPanel.SetActive(true);
    }

    public void refreshHostList()
    {
        if (Network.isClient || Network.isServer)
        {
            Network.Disconnect();
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
        networkView.RPC("setSecondPlayerCheck", RPCMode.AllBuffered, true);
        playerPanel.SetActive(true);
    }

    void OnDisconnectedFromServer()
    {
        Application.LoadLevel("LANLobby");
    }

    void OnMasterServerEvent(MasterServerEvent mse)
    {
        if (mse == MasterServerEvent.RegistrationSucceeded)
        {
            Debug.Log("Registered Server");
        }
    }
}
