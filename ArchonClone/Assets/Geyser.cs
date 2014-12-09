using UnityEngine;
using System.Collections;

public class Geyser : MonoBehaviour 
{
    public static int curEruptions = 0;
    public bool erupting = false;
    bool goingToErupt = false;
    float eruptionTimer = 0f;
    bool inBetween = false;
	// Use this for initialization
	void Start () 
    {
        Geyser.curEruptions = 0;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (!erupting && !goingToErupt && !inBetween && Geyser.curEruptions < 5)
        {
            if (Random.Range(0.0f, 10000.0f) < 10)
            {
                Geyser.curEruptions++;
                goingToErupt = true;
            }
        }

        if (goingToErupt)
        {
            eruptionTimer = Random.Range(5.0f, 15.0f);
            GetComponent<ParticleSystem>().Play();
            GetComponent<ParticleSystem>().startSpeed = 1;
            goingToErupt = false;
            inBetween = true;
            Invoke("Erupt",Random.Range(4.0f,8.0f));
        }

        if (erupting)
        {
            if (eruptionTimer < 0)
            {
                if (GetComponent<ParticleSystem>().startSpeed >= 1)
                {
                    GetComponent<ParticleSystem>().startSpeed -= Random.Range(5.0f, 15.0f) * Time.deltaTime;
                }
                else
                {
                    GetComponent<ParticleSystem>().Stop();
                    Geyser.curEruptions--;
                    erupting = false;
                }
            }
            else
            {
                if (GetComponent<ParticleSystem>().startSpeed < 20)
                {
                    GetComponent<ParticleSystem>().startSpeed += Random.Range(5.0f,15.0f)*Time.deltaTime;
                }
                eruptionTimer -= Time.deltaTime;
            }
        }
	}

    void Erupt()
    {
        erupting = true;
        inBetween = false;
    }
}
