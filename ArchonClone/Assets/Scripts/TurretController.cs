using UnityEngine;
using System.Collections;

public class TurretController : MonoBehaviour {

    enum state {shooting,stationary, rotating };
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {

        RaycastHit hit;
        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.renderer.material.mainTextureOffset = new Vector2(Time.time,0);
        //lineRenderer.useWorldSpace = false; 
        //lineRenderer.SetVertexCount(2);
        //Physics.Raycast(transform.position,transform.forward,hit);
        
        
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            if (hit.collider)
            {
                lineRenderer.SetPosition(1, new Vector3(0, 0, hit.distance));
            }
            else
            {
                lineRenderer.SetPosition(1, new Vector3(0, 0, 5000));
            }
            //Debug.DrawLine(transform.position, hit.point, Color.red);
            //if (hit.transform.gameObject == enemy)
            {
                
            }
        }
	}
}
