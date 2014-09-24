using UnityEngine;
using System.Collections;

public class WaveSetup : MonoBehaviour {

    public int wave = 0;
    public Transform[] spawnPoints;
    public GameObject[] enemyTypes;
    public Vector2 spawnTime = new Vector2(2, 8);
    public int totalEnemies = 4;
    public float spawnNumber;
    public int selectedEnemy;
    public int spawns = 0;
    public float spawnRate = 0;
    public bool timeSet = false;
    public bool buildMode = true;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (buildMode == false)
        {
            if (spawns <= totalEnemies)
            {
                if (timeSet == false)
                {
                    spawnRate = Random.Range(spawnTime.x, spawnTime.y) + Time.time;
                    spawnNumber = Random.Range(0, 100);
                    timeSet = true;
                }
                else if (timeSet == true && Time.time >= spawnRate)
                {

                    if (spawnNumber >= 40 && spawnNumber <= 100)
                    {
                        selectedEnemy = 0;
                    }
                    else if (spawnNumber >= 10 && spawnNumber < 40)
                    {
                        selectedEnemy = 1;
                    }
                    else if (spawnNumber >= 0 && spawnNumber < 10)
                    {
                        selectedEnemy = 2;
                    }

                    GameObject newEnemy = (GameObject)Instantiate(enemyTypes[selectedEnemy], spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position, Quaternion.identity);
                    spawns++;
                    timeSet = false;
                }
            }
        }
	}
}
