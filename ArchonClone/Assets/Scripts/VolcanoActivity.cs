using UnityEngine;
using System.Collections;

public class VolcanoActivity : MonoBehaviour {

    int phase = 0;
	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Accelerator.countdownTimer <= 30 && phase == 0)
        {
            GetComponent<ParticleSystem>().Play();
            GetComponent<ParticleSystem>().emissionRate += 15;
            phase++;
        }
        else if (Accelerator.countdownTimer <= 20 && phase == 1)
        {
            GetComponent<ParticleSystem>().emissionRate += 15;
            phase++;
        }
        else if (Accelerator.countdownTimer <= 10 && phase == 2)
        {
            GetComponent<ParticleSystem>().emissionRate += 15;
            phase++;
        }
        else if (Accelerator.countdownTimer <= 0 && phase == 3)
        {
            GetComponent<ParticleSystem>().emissionRate += 15;
            phase++;
        }
	}
}
