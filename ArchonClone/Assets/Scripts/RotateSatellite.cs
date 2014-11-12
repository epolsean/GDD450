using UnityEngine;
using System.Collections;

public class RotateSatellite : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        transform.Rotate(transform.up, Time.deltaTime * 10, Space.Self);
	}
}
