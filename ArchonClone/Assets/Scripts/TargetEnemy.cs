using UnityEngine;
using System.Collections;

public class TargetEnemy : MonoBehaviour 
{
    public GameObject target;
	
	// Update is called once per frame
	void Update () 
    {
        GetComponent<Rigidbody>().velocity = (10 * Vector3.Normalize(target.transform.position - transform.position));
	}
}
