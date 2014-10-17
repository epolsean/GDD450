using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class CursorController : MonoBehaviour {

    Vector3 moveDirection = Vector3.zero;
    float speed = 5.0F;

    public GameObject cursor;

	// Use this for initialization
	void Start () {
        Screen.showCursor = false;
        cursor.transform.position = new Vector3(20, 20, 0);
        if (Input.GetJoystickNames().Length == 0)
        {
            GetComponent<Image>().enabled = false;
        }
	}
	
 
    void Update() 
    {
        if (BattleStats.player1sTurn)
        {
            if (Input.GetAxis("360_AButton1") == 1)
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
            Input.mousePosition.Set(cursor.transform.position.x, cursor.transform.position.y, cursor.transform.position.z);
        }
        else
        {
            if (Input.GetAxis("360_AButton2") == 1)
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
            Input.mousePosition.Set(cursor.transform.position.x, cursor.transform.position.y, cursor.transform.position.z);
        }
 
    }


}


