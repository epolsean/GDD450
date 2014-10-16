using UnityEngine;
using System.Collections;

public class RaycastCursor : MonoBehaviour {

    Vector3 rayHitPoint;
    GameObject cursor;
    GameObject currentHex;
    GameObject lastHex;
	// Use this for initialization
	void Start () {
        cursor = GameObject.Find("Cursor");
	}
	
	// Update is called once per frame
	void Update () {

        RaycastHit hit;
        Ray rayPos = Camera.main.ScreenPointToRay(cursor.transform.position);

        if (Physics.Raycast(rayPos, out hit, Mathf.Infinity))
        {
            Debug.DrawLine(Camera.main.transform.position, hit.point, Color.red);
            rayHitPoint = hit.point;
            if (currentHex != hit.transform.gameObject && lastHex != null)
            {
                lastHex = currentHex;
                lastHex.GetComponent<TileProperties>().OnMouseExit();
                currentHex = hit.transform.gameObject;
                currentHex.GetComponent<TileProperties>().OnMouseOver();
                Debug.Log("if");
            }
            else if (currentHex != hit.transform.gameObject && lastHex == null)
            {
                currentHex = hit.transform.gameObject;
                currentHex.GetComponent<TileProperties>().OnMouseOver();
                lastHex = currentHex;
                Debug.Log("else if");
            }
        }
	
	}
}
