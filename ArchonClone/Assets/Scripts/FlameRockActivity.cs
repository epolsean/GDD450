using UnityEngine;
using System.Collections;

public class FlameRockActivity : MonoBehaviour {

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
            phase++;
        }
        else if (Accelerator.countdownTimer <= 10 && phase == 1)
        {
            GetComponent<ParticleSystem>().maxParticles += 1;
            phase++;
        }
        else if (Accelerator.countdownTimer <= 0 && phase == 2)
        {
            GetComponent<ParticleSystem>().maxParticles += 1;
            phase++;
        }
	}
}
