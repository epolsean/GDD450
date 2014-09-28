using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIController : MonoBehaviour
{
    public GameObject WavCon;
    public GameObject GoldUI;
    public GameObject GumballUI;
    public GameObject CandyCaneUI;

    WaveSetup WavSet;
    int currentWave = 0;
    int numGumballs = 0;
    int numCandyCanes = 0;
    int numGoldCoins = 0;

    public float timeTaken = 0;
    public int enemyKills = 0;

    // Use this for initialization
    void Start()
    {
        WavSet = WavCon.GetComponent<WaveSetup>();
    }

    // Update is called once per frame
    void Update()
    {
        numGoldCoins = (int)WavSet.resource1;
        numCandyCanes = (int)WavSet.resource2;
        numGumballs = (int)WavSet.resource3;
        GoldUI.GetComponent<Text>().text = "Gold: " + numGoldCoins;
        CandyCaneUI.GetComponent<Text>().text = "Candy Canes: " + numCandyCanes;
        GumballUI.GetComponent<Text>().text = "Gumballs: " + numGumballs;
    }
}
