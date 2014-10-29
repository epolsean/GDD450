using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class CursorController : MonoBehaviour 
{
    Vector3 moveDirection = Vector3.zero;
    float speed = 5.0F;

    public GameObject cursor;

	// Use this for initialization
	void Start () 
    {
        Screen.showCursor = false;
        
        cursor.transform.position = new Vector3(20, 20, 0);
	}
	
 
    void Update()
    {
        Screen.showCursor = false;
        if (TurnStateMachine.state == TurnStateMachine.State.playerTurn)
        {
            if (Input.GetJoystickNames().Length != 0)
            {
                if (Input.GetAxis("360_XButton1") == 1)
                {
                    speed = 12.0f;
                }
                else
                {
                    speed = 5.0f;
                }
                moveDirection = new Vector3(Input.GetAxis("360_HorizontalLeftStick1"), Input.GetAxis("360_VerticalLeftStick1"), 0);
                Vector3.Normalize(moveDirection);
                cursor.transform.position += speed * moveDirection;
                Input.mousePosition.Set(cursor.transform.position.x + 6, cursor.transform.position.y - 6, cursor.transform.position.z);
            }
            else
            {
                cursor.transform.position = new Vector3(Input.mousePosition.x + 6, Input.mousePosition.y - 6, Input.mousePosition.z);
            }
        }
        else
        {
            if (Input.GetJoystickNames().Length >= 1)
            {
                if (Input.GetAxis("360_XButton2") == 1)
                {
                    speed = 12.0f;
                }
                else
                {
                    speed = 5.0f;
                }
                moveDirection = new Vector3(Input.GetAxis("360_HorizontalLeftStick2"), Input.GetAxis("360_VerticalLeftStick2"), 0);
                Vector3.Normalize(moveDirection);
                cursor.transform.position += speed * moveDirection;
                Input.mousePosition.Set(cursor.transform.position.x + 6, cursor.transform.position.y - 6, cursor.transform.position.z);
            }
            else
            {
                cursor.transform.position = new Vector3(Input.mousePosition.x + 6, Input.mousePosition.y - 6, Input.mousePosition.z);
            }
        }
    }
}


