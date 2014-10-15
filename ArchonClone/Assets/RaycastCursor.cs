using UnityEngine;
using System.Collections;

public class RaycastCursor : MonoBehaviour {

    private Vector3 rayHitPoint;
    public GameObject image;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        RaycastHit hit;
        Ray rayPos = Camera.main.ScreenPointToRay(image.transform.position);


        if (Physics.Raycast(rayPos, out hit, Mathf.Infinity))
        {
            Debug.DrawLine(Camera.main.transform.position, hit.point, Color.red);
            rayHitPoint = hit.point;
            hit.transform.gameObject.GetComponent<TileProperties>().OnMouseOver();
        }
	
	}
}
