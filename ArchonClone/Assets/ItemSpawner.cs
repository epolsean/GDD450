using UnityEngine;
using System.Collections;

public class ItemSpawner : MonoBehaviour 
{
    public GameObject explodingBarrel;
    public GameObject regularBarrel;
    public GameObject powerUp;

    public static int numExplodingBarrels;
    public static int numRegularBarrels;
    public static int numPowerUps;

    public bool empty;
    float reSpawnTimer = 5.0f;
	// Use this for initialization
	void Start () 
    {
        float spawnNum = Random.Range(0, 100);
        if (spawnNum < 25)
        {
            if (ItemSpawner.numExplodingBarrels < 3)
            {
                explodingBarrel.SetActive(true);
                empty = false;
                ItemSpawner.numExplodingBarrels++;
            }
        }
        else if (spawnNum > 80)
        {
            if (ItemSpawner.numRegularBarrels < 3)
            {
                regularBarrel.SetActive(true);
                empty = false;
                ItemSpawner.numRegularBarrels++;
            }
        }
        else if (spawnNum < 50)
        {
            if (ItemSpawner.numPowerUps < 3)
            {
                powerUp.SetActive(true);
                empty = false;
                ItemSpawner.numPowerUps++;
            }
        }
        else
        {
            empty = true;
        }
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (empty)
        {
            if (reSpawnTimer < 0)
            {
                float spawnNum = Random.Range(0, 1000);
                if (spawnNum < 25)
                {
                    if (ItemSpawner.numExplodingBarrels < 3)
                    {
                        explodingBarrel.SetActive(true);
                        empty = false;
                        ItemSpawner.numExplodingBarrels++;
                    }
                }
                else if (spawnNum < 50)
                {
                    if (ItemSpawner.numPowerUps < 3)
                    {
                        powerUp.SetActive(true);
                        empty = false;
                        ItemSpawner.numPowerUps++;
                    }
                }
                else
                {
                    reSpawnTimer = 5.0f;
                }
            }
            else
            {
                reSpawnTimer -= Time.deltaTime;
            }
        }
	}
}
