using UnityEngine;
using System.Collections;

public class LaserController : MonoBehaviour 
{
    float shootTimer = 5.0f;
    public bool shooting = false;

	// Use this for initialization
	void Start () 
    {
        shootTimer = Random.Range(4.6f, 6.2f);
	}
	
	// Update is called once per frame
	void Update () 
    {
        shootTimer -= Time.deltaTime;
        if (shootTimer <= 0)
        {
            if (shooting)
            {
                GetComponent<ParticleSystem>().Stop();
                shooting = false;
            }
            else
            {
                GetComponent<ParticleSystem>().Play();
                shooting = true;
            }
            shootTimer = 5.0f;
        }
	}
}
