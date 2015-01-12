using UnityEngine;
using System.Collections;

public class Flicker : MonoBehaviour 
{
    public static int brokenLights = 0;
    bool broken = false;
    bool flashing = false;
    float timer = 0f;
	
	// Update is called once per frame
	void Update () 
    {
        if (Flicker.brokenLights == 0)
        {
            if (Random.Range(0, 100) < 10)
            {
                Flicker.brokenLights++;
                broken = true;
            }
        }
        if (broken && timer < 0)
        {
            
            if (flashing)
            {
                flashing = false;
                GetComponent<Light>().intensity = 8f;
                timer = Random.Range(0.2f, 1.5f);
            }
            else
            {
                flashing = true;
                timer = Random.Range(0.1f, 0.5f);
            }
        }
        else
        {
            if(flashing)
            {
                GetComponent<Light>().intensity = Random.Range(1f, 8f);
            }
            timer -= Time.deltaTime;
        }
	
	}
}
