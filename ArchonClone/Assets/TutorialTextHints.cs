using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TutorialTextHints : MonoBehaviour 
{
    int curTutorialStep = 0;

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (curTutorialStep == 0)
        {
            GetComponent<Text>().text = "Use the WASD keys to move and look around";
        }
        else if (curTutorialStep == 1)
        {
            GetComponent<Text>().text = "Press the E key to fire a bullet";
        }
        else if (curTutorialStep == 2)
        {
            GetComponent<Text>().text = "The enemy is somewhere on the map and it is up to you to find and destroy them";
        }
        else if (curTutorialStep == 3)
        {
            GetComponent<Text>().text = "";
        }
        else if (curTutorialStep == 4)
        {
            GetComponent<Text>().text = "";
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            curTutorialStep++;
        }

	}
}
