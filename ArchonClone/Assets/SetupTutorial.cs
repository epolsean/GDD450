using UnityEngine;
using System.Collections;

public class SetupTutorial : MonoBehaviour 
{
    public GameObject player1;

    public GameObject p1TopDownCamera;
    public GameObject p1ThirdCamera;

    public GameObject tutText;

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
        if (TutorialTextHints.curTutorialStep == 4)
        {
            tutText.SetActive(true);
            if (BattleStats.player1Pref == "third")
            {
                p1TopDownCamera.SetActive(true);
                p1ThirdCamera.SetActive(false);
                player1.GetComponent<PlayerControllerTutorial>().topDownView = true;
            }
            else
            {
                p1TopDownCamera.SetActive(false);
                player1.GetComponent<PlayerControllerTutorial>().topDownView = false;
            }
        }
        if (Input.GetKeyUp(KeyCode.Space) && (TutorialTextHints.curTutorialStep <= 2 || TutorialTextHints.curTutorialStep == 4))
        {
            TutorialTextHints.curTutorialStep++;
        }
    }
}

