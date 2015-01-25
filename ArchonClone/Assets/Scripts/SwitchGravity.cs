using UnityEngine;
using System.Collections;

public class SwitchGravity : MonoBehaviour 
{

    public Vector3 gravity;
    Vector3 startGravity;
	// Use this for initialization
	void Start () 
    {
        startGravity = Physics.gravity;
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player1(Clone)" && gravity!=Vector3.zero)
        {
            Debug.Log("player collided with cube");
            Physics.gravity = gravity;
            StartCoroutine("ReturnGravity");
        }
    }

    IEnumerator ReturnGravity()
    {
        yield return new WaitForSeconds(1f);
        Physics.gravity = startGravity;
    }
}
