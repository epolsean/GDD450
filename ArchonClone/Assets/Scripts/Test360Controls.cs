using UnityEngine;
using System.Collections;

public class Test360Controls : MonoBehaviour 
{
	// These are used to modify the player movement speed, and rotation speed.
	public float PlayerMovementSpeed = 30;
	public float PlayerRotationSpeed = 180;
    public bool thirdPersonSplit;
	
	// I seperated Movement and Button inputs into seperate functions, it makes for easier debugging
	void Update ()
	{
		//Movement1();
        //Movement2();
		UserInputs1();
        UserInputs2();
	}
	

	void Movement1()
	{
		// This line is for vertical movement, right now its on the Z AXIS.
        transform.Translate(0, 0, Input.GetAxis("360_VerticalLeftStick1") * Time.deltaTime * PlayerMovementSpeed);
		
		// This line is for horizontal movement, right now its on the X AXIS. When combined with vertical movement it can be used for Strafing.
        transform.Translate(Input.GetAxis("360_HorizontalLeftStick1") * Time.deltaTime * PlayerMovementSpeed, 0, 0);

        if (thirdPersonSplit)
        {
            // This line is for vertical rotation, right now its on the Y AXIS. 
            transform.Rotate(0, Input.GetAxis("360_VerticalRightStick1") * Time.deltaTime * PlayerRotationSpeed, 0);
            // This line is for hhorizontal rotation
            //transform.Rotate(0, Input.GetAxis("360_HorizontalRightStick1") * Time.deltaTime * PlayerRotationSpeed, 0);
        }
	}

    void Movement2()
    {
        // This line is for vertical movement, right now its on the Z AXIS.
        transform.Translate(0, 0, Input.GetAxis("360_VerticalLeftStick2") * Time.deltaTime * PlayerMovementSpeed);

        // This line is for horizontal movement, right now its on the X AXIS. When combined with vertical movement it can be used for Strafing.
        transform.Translate(Input.GetAxis("360_HorizontalLeftStick2") * Time.deltaTime * PlayerMovementSpeed, 0, 0);

        if(thirdPersonSplit)
        {
            // This line is for vertical rotation, right now its on the Y AXIS. 
            transform.Rotate(0, Input.GetAxis("360_VerticalRightStick2") * Time.deltaTime * PlayerRotationSpeed, 0);
            // This line is for horizontal rotation, right now its on the Y AXIS. 
            transform.Rotate(0, Input.GetAxis("360_HorizontalRightStick2") * Time.deltaTime * PlayerRotationSpeed, 0);
        }

        // This line is for horizontal rotation, right now its on the Y AXIS. 
        //transform.Rotate(Input.GetAxis("360_VerticalRightStick") * Time.deltaTime * PlayerRotationSpeed, 0, 0);
    }
	
	// This function handles the Inputs from the buttons on the controller 1
	void UserInputs1()
	{
		// A Button is read from Input Positive Button "joystick button 0"
		if(Input.GetButtonDown ("360_AButton1"))
		{
			Debug.Log("A Button1!");
		}
		
		// B Button is read from Input Positive Button "joystick button 1"
		if(Input.GetButtonDown ("360_BButton1"))
		{
			Debug.Log("B Button1!");
		}
		
		// X Button is read from Input Positive Button "joystick button 2"
		if(Input.GetButtonDown ("360_XButton1"))
		{
			Debug.Log("X Button1!");
		}
		
		// Y Button is read from Input Positive Button "joystick button 3"
		if(Input.GetButtonDown ("360_YButton1"))
		{
			Debug.Log("Y Button1!");
		}
				
		// Left Bumper is read from Input Positive Button "joystick button 4"
		if(Input.GetButtonDown ("360_LBButton1"))
		{
			Debug.Log("Left Bumper1!");
		}
		
		// Right Bumper is read from Input Positive Button "joystick button 5"
		if(Input.GetButtonDown ("360_RBButton1"))
		{
			Debug.Log("Right Bumper1!");
		}
		
		// Back Button is read from Input Positive Button "joystick button 6"
		if(Input.GetButtonDown ("360_BackButton1"))
		{
			Debug.Log("Back Button1!");
		}
		
		// Start Button is read from Input Positive Button "joystick button 7"
		if(Input.GetButtonDown ("360_StartButton1"))
		{
			Debug.Log("Start Button1!");
		}
		
		// Left Thumbstick Button is read from Input Positive Button "joystick button 8"
		if(Input.GetButtonDown ("360_LeftThumbstickButton1"))
		{
			Debug.Log("Left Thumbstick Button1!");
		}
		
		// Right Thumbstick Button is read from Input Positive Button "joystick button 9"
		if(Input.GetButtonDown ("360_RightThumbstickButton1"))
		{
			Debug.Log("Right Thumbstick Button1!");
		}
		
		// Triggers are read from the 3rd Joystick Axis and read from a Sensitivity rating from -1 to 1
		//
		// Right Trigger is activated when pressure is above 0, or the dead zone.
		if(Input.GetAxis("360_Triggers1")>0.001)
		{
			Debug.Log ("Right Trigger1!");
		}
		
		// Right Trigger is activated when pressure is under 0, or the dead zone.
		if(Input.GetAxis("360_Triggers1")<0)
		{
			Debug.Log("Left Trigger1!");
		}
		
		// The D-PAD is read from the 6th(Horizontal) and 7th(Vertical) Joystick Axes and read from a Sensitivity rating from -1 to 1, similar to the Triggers.
		//
		// Right D-PAD Button is activated when pressure is above 0, or the dead zone.
		if(Input.GetAxis("360_HorizontalDPAD1")>0.001)
		{
			Debug.Log ("Right D-PAD Button1!");
		}
		
		// Left D-PAD Button is activated when pressure is under 0, or the dead zone.
		if(Input.GetAxis("360_HorizontalDPAD1")<0)
		{
			Debug.Log("Left D-PAD Button1!");
		}
		
		// Up D-PAD Button is activated when pressure is above 0, or the dead zone.
		if(Input.GetAxis("360_VerticalDPAD1")>0.001)
		{
			Debug.Log ("Up D-PAD Button1!");
		}
		
		// Down D-PAD Button is activated when pressure is under 0, or the dead zone.
		if(Input.GetAxis("360_VerticalDPAD1")<0)
		{
			Debug.Log("Down D-PAD Button1!");
		}
	}

    void UserInputs2()
    {
        // A Button is read from Input Positive Button "joystick button 0"
        if (Input.GetButtonDown("360_AButton2"))
        {
            Debug.Log("A Button2!");
        }

        // B Button is read from Input Positive Button "joystick button 1"
        if (Input.GetButtonDown("360_BButton2"))
        {
            Debug.Log("B Button2!");
        }

        // X Button is read from Input Positive Button "joystick button 2"
        if (Input.GetButtonDown("360_XButton2"))
        {
            Debug.Log("X Button2!");
        }

        // Y Button is read from Input Positive Button "joystick button 3"
        if (Input.GetButtonDown("360_YButton2"))
        {
            Debug.Log("Y Button2!");
        }

        // Left Bumper is read from Input Positive Button "joystick button 4"
        if (Input.GetButtonDown("360_LBButton2"))
        {
            Debug.Log("Left Bumper2!");
        }

        // Right Bumper is read from Input Positive Button "joystick button 5"
        if (Input.GetButtonDown("360_RBButton2"))
        {
            Debug.Log("Right Bumper2!");
        }

        // Back Button is read from Input Positive Button "joystick button 6"
        if (Input.GetButtonDown("360_BackButton2"))
        {
            Debug.Log("Back Button2!");
        }

        // Start Button is read from Input Positive Button "joystick button 7"
        if (Input.GetButtonDown("360_StartButton2"))
        {
            Debug.Log("Start Button2!");
        }

        // Left Thumbstick Button is read from Input Positive Button "joystick button 8"
        if (Input.GetButtonDown("360_LeftThumbstickButton2"))
        {
            Debug.Log("Left Thumbstick Button2!");
        }

        // Right Thumbstick Button is read from Input Positive Button "joystick button 9"
        if (Input.GetButtonDown("360_RightThumbstickButton2"))
        {
            Debug.Log("Right Thumbstick Button2!");
        }

        // Triggers are read from the 3rd Joystick Axis and read from a Sensitivity rating from -1 to 1
        //
        // Right Trigger is activated when pressure is above 0, or the dead zone.
        if (Input.GetAxis("360_Triggers2") > 0.001)
        {
            Debug.Log("Right Trigger2!");
        }

        // Left Trigger is activated when pressure is under 0, or the dead zone.
        if (Input.GetAxis("360_Triggers2") < 0)
        {
            Debug.Log("Left Trigger2!");
        }

        // The D-PAD is read from the 6th(Horizontal) and 7th(Vertical) Joystick Axes and read from a Sensitivity rating from -1 to 1, similar to the Triggers.
        //
        // Right D-PAD Button is activated when pressure is above 0, or the dead zone.
        if (Input.GetAxis("360_HorizontalDPAD2") > 0.001)
        {
            Debug.Log("Right D-PAD Button2!");
        }

        // Left D-PAD Button is activated when pressure is under 0, or the dead zone.
        if (Input.GetAxis("360_HorizontalDPAD2") < 0)
        {
            Debug.Log("Left D-PAD Button2!");
        }

        // Up D-PAD Button is activated when pressure is above 0, or the dead zone.
        if (Input.GetAxis("360_VerticalDPAD2") > 0.001)
        {
            Debug.Log("Up D-PAD Button2!");
        }

        // Down D-PAD Button is activated when pressure is under 0, or the dead zone.
        if (Input.GetAxis("360_VerticalDPAD2") < 0)
        {
            Debug.Log("Down D-PAD Button2!");
        }
    }
}