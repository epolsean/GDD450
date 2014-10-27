using UnityEngine;
using System.Collections;

public class SetupTutorial : MonoBehaviour 
{
    public GameObject player1;

    public GameObject p1TopDownCamera;
    public GameObject p1ThirdCamera;

    // Use this for initialization
    void Awake()
    {
        if (BattleStats.player1Pref == "third")
        {
            p1TopDownCamera.SetActive(false);
            player1.GetComponent<PlayerControllerTutorial>().topDownView = false;
        }
        else
        {
            p1TopDownCamera.SetActive(true);
            p1ThirdCamera.SetActive(false);
            player1.GetComponent<PlayerControllerTutorial>().topDownView = true;
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}

