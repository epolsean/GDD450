using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TutorialTextHints : MonoBehaviour 
{
    public static int curTutorialStep = 0;

    public GameObject forcefield1;
    public GameObject forcefield2;

    public GameObject continueText;

	// Update is called once per frame
	void Update () 
    {
        if (Input.GetJoystickNames().Length != 0)
        {
            continueText.GetComponent<Text>().text = "Press the A button to continue";
        }
        else
        {
            continueText.GetComponent<Text>().text = "Press the Space Bar to continue";
        }
        if (curTutorialStep == 0)
        {
            if (Input.GetJoystickNames().Length != 0)
            {
                GetComponent<Text>().text = "Use the left joystick to move and right joystick to look around";
            }
            else
            {
                GetComponent<Text>().text = "Use the WASD keys to move and look around";
            }
        }
        else if (curTutorialStep == 1)
        {
            if (Input.GetJoystickNames().Length != 0)
            {
                GetComponent<Text>().text = "Press the right trigger to fire a bullet";
            }
            else
            {
                GetComponent<Text>().text = "Press the E key to fire a bullet";
            }
        }
        else if (curTutorialStep == 2)
        {
            GetComponent<Text>().text = "The enemy is somewhere on the map and it is up to you to find and destroy them";
        }
        else if (curTutorialStep == 3)
        {
            GetComponent<Text>().text = "";
            Destroy(forcefield1);
            Destroy(forcefield2);
            transform.parent.gameObject.SetActive(false);
        }
        else if (curTutorialStep == 4)
        {
            GetComponent<Text>().text = "In the settings you can change your preference of camera to either fight battles in top down or third person";
        }
        else if (curTutorialStep == 5)
        {
            transform.parent.gameObject.SetActive(false);
        }

        
	}
}
