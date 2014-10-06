using UnityEngine;
using System.Collections;

public class RollPumpSeedUpBarrel : MonoBehaviour {

    public bool isFire = false; 
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if(isFire)//reference hasTarget in parent
        {
            this.transform.Rotate(new Vector3(0, 0, 10));
        }
	}
}
