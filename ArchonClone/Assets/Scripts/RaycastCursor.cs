using UnityEngine;
using System.Collections;

public class RaycastCursor : MonoBehaviour {

    GameObject currentHex;
    GameObject lastHex;

    public GameObject alienCursor;
    public GameObject robotCursor;

    public LayerMask mask;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

        if (GameObject.Find("BattleSceneAdditive") || GameObject.Find("TutorialTestBattle"))
        {
            gameObject.GetComponent<Canvas>().enabled = false;
        }
        else
        {
            gameObject.GetComponent<Canvas>().enabled = true;
        }
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

        if (TurnStateMachine.state == TurnStateMachine.State.playerTurn)
        {
            Ray rayPos = Camera.main.ScreenPointToRay(new Vector3(robotCursor.transform.position.x - 6,robotCursor.transform.position.y + 6,robotCursor.transform.position.z));
            RaycastHit hit;

            if (Physics.Raycast(rayPos, out hit, Mathf.Infinity, mask))
            {
                Debug.DrawLine(Camera.main.transform.position, hit.point, Color.red);
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
                    if (Input.GetJoystickNames().Length != 0)
                    {
                        if (Input.GetButtonUp("360_AButton1"))
                        {
                            Debug.Log("A button pressed");
                            currentHex.GetComponent<TileProperties>().MouseDownCall();
                        }
                    }
                    /*else
                    {
                        if (Input.GetMouseButtonDown(0))
                        {
                            Debug.Log("Mouse Clicked");
                            currentHex.GetComponent<TileProperties>().OnMouseDown();
                        }
                    }*/
                }
            }
        }
        else if (TurnStateMachine.state == TurnStateMachine.State.otherTurn)
        {
            Ray rayPos = Camera.main.ScreenPointToRay(new Vector3(alienCursor.transform.position.x - 6, alienCursor.transform.position.y + 6, alienCursor.transform.position.z));
            RaycastHit hit;

            if (Physics.Raycast(rayPos, out hit, Mathf.Infinity, mask))
            {
                Debug.DrawLine(Camera.main.transform.position, hit.point, Color.red);
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
                    if (Input.GetJoystickNames().Length > 1)
                    {
                        if (Input.GetButtonUp("360_AButton2"))
                        {
                            Debug.Log("A button pressed");
                            currentHex.GetComponent<TileProperties>().OnMouseDown();
                        }
                    }
                    /*else
                    {
                        if (Input.GetMouseButtonDown(0))
                        {
                            Debug.Log("Mouse Clicked");
                            currentHex.GetComponent<TileProperties>().OnMouseDown();
                        }
                    }*/
                }
            }
        }
	}
}
