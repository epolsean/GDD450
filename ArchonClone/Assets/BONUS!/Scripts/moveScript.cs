using UnityEngine;
using System.Collections;

public class moveScript : MonoBehaviour {

    public Animator WalkingStates;
    public Animator FlashlightStates;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.W))
        {
            WalkingStates.SetBool("North",true);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            WalkingStates.SetBool("West", true);
            FlashlightStates.SetBool("West", true);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            WalkingStates.SetBool("South", true);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            WalkingStates.SetBool("East", true);
            FlashlightStates.SetBool("East", true);
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            WalkingStates.SetBool("North", false);
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            WalkingStates.SetBool("West", false);
            FlashlightStates.SetBool("West", false);
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            WalkingStates.SetBool("South", false);
            FlashlightStates.SetBool("South", false);
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            WalkingStates.SetBool("East", false);
            FlashlightStates.SetBool("East", false);
        }
	}
}
