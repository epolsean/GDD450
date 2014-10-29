using UnityEngine;
using System.Collections;

public class RaycastCursor : MonoBehaviour {

    Vector3 rayHitPoint;
    GameObject cursor;
    GameObject currentHex;
    GameObject lastHex;

    public GameObject alienCursor;
    public GameObject robotCursor;

    public LayerMask mask;
	// Use this for initialization
	void Start () {
        cursor = GameObject.Find("Cursor");
	}
	
	// Update is called once per frame
	void Update () {

        if (TurnStateMachine.state == TurnStateMachine.State.playerTurn)
        {
            robotCursor.SetActive(true);
            alienCursor.SetActive(false);
        }
        else if(TurnStateMachine.state == TurnStateMachine.State.otherTurn)
        {
            robotCursor.SetActive(false);
            alienCursor.SetActive(true);
        }

        RaycastHit hit;
        Ray rayPos = Camera.main.ScreenPointToRay(cursor.transform.position);

        if (Physics.Raycast(rayPos, out hit, Mathf.Infinity,mask))
        {
            Debug.DrawLine(Camera.main.transform.position, hit.point, Color.red);
            rayHitPoint = hit.point;
            if (currentHex != hit.transform.gameObject && lastHex != null)
            {
                lastHex = currentHex;
                lastHex.GetComponent<TileProperties>().OnMouseExit();
                currentHex = hit.transform.gameObject;
                currentHex.GetComponent<TileProperties>().OnMouseOver();
            }
            else if (currentHex != hit.transform.gameObject && lastHex == null)
            {
                currentHex = hit.transform.gameObject;
                currentHex.GetComponent<TileProperties>().OnMouseOver();
                lastHex = currentHex;
                
            }
            else if (currentHex == hit.transform.gameObject)
            {
                if (Input.GetButtonUp("360_AButton1"))
                {
                    Debug.Log("A button pressed");
                    currentHex.GetComponent<TileProperties>().OnMouseDown();
                }
            }
        }
	
	}
}
