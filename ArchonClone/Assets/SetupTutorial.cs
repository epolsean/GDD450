using UnityEngine;
using System.Collections;

public class SetupTutorial : MonoBehaviour 
{
    public GameObject player1;

    public GameObject p1Health3rd;
    public GameObject p1HealthTopDown1;
    public GameObject p1HealthTopDown2;
    public GameObject p2Health3rd;
    public GameObject p2HealthTopDown1;
    public GameObject p2HealthTopDown2;
    public GameObject p2Text;
    public GameObject p1Text;

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

        if (player1.GetComponent<PlayerControllerTutorial>().topDownView == false)
        {
            p1HealthTopDown1.SetActive(false);
            p1HealthTopDown2.SetActive(false);
            p2HealthTopDown1.SetActive(false);
            p2HealthTopDown2.SetActive(false);

            p1Health3rd.SetActive(true);
            p2Health3rd.SetActive(true);
            p2Text.SetActive(true);
            p1Text.SetActive(true);
        }
        else
        {
            p1HealthTopDown1.SetActive(true);
            p1HealthTopDown2.SetActive(true);
            p2HealthTopDown1.SetActive(true);
            p2HealthTopDown2.SetActive(true);

            p1Health3rd.SetActive(false);
            p2Health3rd.SetActive(false);
            p2Text.SetActive(false);
            p1Text.SetActive(false);
        }
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
                p1ThirdCamera.SetActive(true);
                p1TopDownCamera.SetActive(false);
                player1.GetComponent<PlayerControllerTutorial>().topDownView = false;
            }
        }
        if (Input.GetJoystickNames().Length != 0)
        {
            if (Input.GetButtonUp("360_AButton1") && (TutorialTextHints.curTutorialStep <= 2 || TutorialTextHints.curTutorialStep == 4))
            {
                TutorialTextHints.curTutorialStep++;
            }
        }
        else
        {
            if (Input.GetKeyUp(KeyCode.Space) && (TutorialTextHints.curTutorialStep <= 2 || TutorialTextHints.curTutorialStep == 4))
            {
                TutorialTextHints.curTutorialStep++;
            }
        }
    }
}

