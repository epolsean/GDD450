using UnityEngine;
using System.Collections;

public class MoveTarget : MonoBehaviour {

    float firingFrom;
    public GameObject Target;
    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        float targetDist = Vector3.Distance(this.transform.position, Target.transform.position);
        Debug.Log("TargetDist = " + targetDist);
	    if(Input.GetAxis("Mouse Y") > 0 && targetDist < 20)
        {
            firingFrom = Vector3.Distance(this.transform.position, Target.transform.position);
            Target.transform.Translate(0, 0, (Time.deltaTime * (6)));
        }
        if(Input.GetAxis("Mouse Y") < 0 && targetDist > 10)
        {
            firingFrom = Vector3.Distance(this.transform.position, Target.transform.position);
            Target.transform.Translate(0, 0, (Time.deltaTime * (6) * (-1)));
        }
	}


}
