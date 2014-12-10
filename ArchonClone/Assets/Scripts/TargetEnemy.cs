using UnityEngine;
using System.Collections;

public class TargetEnemy : MonoBehaviour 
{
    public GameObject target;
	
	// Update is called once per frame
	void Update () 
    {
        if (target != null)
        {
            GetComponent<Rigidbody>().velocity = (10 * Vector3.Normalize(target.transform.position - transform.position));
        }
        else
        {
            Destroy(this.gameObject);
        }
	}
}
