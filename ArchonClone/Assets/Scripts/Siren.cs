using UnityEngine;
using System.Collections;

public class Siren : MonoBehaviour {

    bool increasing = true;
	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Accelerator.countdownTimer <= 5)
        {
            if (GetComponent<Light>().intensity <= 1)
            {
                increasing = true;
            }
            else if (GetComponent<Light>().intensity >= 5)
            {
                increasing = false;
            }

            if (increasing)
            {
                GetComponent<Light>().intensity += 4 * Time.deltaTime;
            }
            else
            {
                GetComponent<Light>().intensity -= 4 * Time.deltaTime;
            }
        }
        
	}
}
