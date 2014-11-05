using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DisplayTimer : MonoBehaviour 
{
    public bool volcano = false;
	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (volcano)
        {
            if (Accelerator.countdownTimer <= 0)
            {
                GetComponent<Text>().text = "";
            }
            else
            {
                GetComponent<Text>().text = "Time Until Volcano Eruption: " + Accelerator.countdownTimer.ToString("0.00");
            }
        }
        else
        {
            if (Accelerator.countdownTimer <= 0)
            {
                GetComponent<Text>().text = "";
            }
            else
            {
                GetComponent<Text>().text = "Time Until Reactor Malfunction: " + Accelerator.countdownTimer.ToString("0.00");
            }
        }

	}
}
