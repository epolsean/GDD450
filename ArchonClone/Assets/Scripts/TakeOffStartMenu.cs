using UnityEngine;
using System.Collections;

public class TakeOffStartMenu : MonoBehaviour {

	// Use this for initialization
	void Start () 
    {

	}
	
	// Update is called once per frame
	void Update () 
    {
        if (GetComponent<ParticleSystem>().startSize <= 25)
        {
            GetComponent<ParticleSystem>().startSize += 2 * Time.deltaTime;
        }
	
	}
}
