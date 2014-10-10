using UnityEngine;
using System.Collections;


public class CursorController : MonoBehaviour {

    Vector3 moveDirection = Vector3.zero;
    float speed = 5.0F;

    public GameObject cursor;

	// Use this for initialization
	void Start () {
        Screen.showCursor = false;
        cursor.transform.position = new Vector3(1, 1, 0);
	}
	
 
    void Update() 
    {
        if (Input.GetAxis("360_AButton1")==1)
        {
            speed = 12.0f;
            Debug.Log("A Button down");
        }
        else
        {
           speed = 5.0f;
           Debug.Log("A Button up");
        }
        moveDirection = new Vector3(Input.GetAxis("360_HorizontalLeftStick1"), Input.GetAxis("360_VerticalLeftStick1"), 0);
        Vector3.Normalize(moveDirection);
        cursor.transform.position += speed*moveDirection;
        Input.mousePosition.Set(cursor.transform.position.x, cursor.transform.position.y, cursor.transform.position.z);
 
    }


}


