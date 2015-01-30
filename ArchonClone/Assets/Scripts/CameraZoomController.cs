using UnityEngine;
using System.Collections;

public class CameraZoomController : MonoBehaviour {

    Transform startTrans;
    bool following = false;

    //Transform focalPoint;

	// Use this for initialization
	void Start () 
    {
        startTrans = transform;
        //focalPoint = GameObject.Find("FocalPoint").transform;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (!following)
        {
            if (Input.GetAxis("Vertical") != 0)
            {
                transform.Translate(Input.GetAxis("Vertical") * transform.forward, Space.World);
            }

            if (Input.GetAxis("Horizontal") != 0)
            {
                transform.Translate(Input.GetAxis("Horizontal") * transform.right, Space.World);
            }
        }
	}

    public void FollowTarget(GameObject target)
    {
        following = true;
        transform.position = startTrans.position;
        transform.rotation = startTrans.rotation;
        transform.position = new Vector3(transform.position.x, transform.position.y, target.transform.position.z);
        transform.forward = Vector3.Normalize(target.transform.position - transform.position);
        transform.Translate(30 * transform.forward,Space.World);
        transform.SetParent(target.transform);
    }

    public void ResetTransform()
    {
        following = false;
        transform.SetParent(null);
        transform.position = startTrans.position;
        transform.rotation = startTrans.rotation;
    }
}
