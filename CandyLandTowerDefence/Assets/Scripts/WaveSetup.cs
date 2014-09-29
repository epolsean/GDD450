using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WaveSetup : MonoBehaviour {

    public Button start;
    public float wave = 1;
    public Transform[] spawnPoints;
    public GameObject[] enemyTypes;
    Vector2 spawnTime = new Vector2(.5f, 2f);
    int totalEnemies = 4;
    float spawnNumber;
    int selectedEnemy;
    int spawns = 0;
    float spawnRate = 0;
    bool timeSet = false;
    bool buildMode = true;
    public float resource1 = 50;
    public float resource2 = 50;
    public float resource3 = 50;
    public float numBuilding1 = 0;
    public float numBuilding2 = 0;
    public float numBuilding3 = 0;

	// Use this for initialization
	void Start () {
        start.onClick.AddListener(() => {buildMode = false;});
	}
	
	// Update is called once per frame
	void Update () {
        if (buildMode == false)
        {
            start.enabled = false;
            if (spawns < totalEnemies)
            {
                if (timeSet == false)
                {
                    spawnRate = Random.Range(spawnTime.x, spawnTime.y) + Time.time;
                    spawnNumber = Random.Range(0, 100);
                    timeSet = true;
                }
                else if (timeSet == true && Time.time >= spawnRate)
                {

                    if (spawnNumber >= 30 && spawnNumber <= 100)
                    {
                        selectedEnemy = 0;
                    }
                    else if (spawnNumber >= 5 && spawnNumber < 30)
                    {
                        selectedEnemy = 1;
                    }
                    else if (spawnNumber >= 0 && spawnNumber < 5)
                    {
                        selectedEnemy = 2;
                    }

                    GameObject newEnemy = (GameObject)Instantiate(enemyTypes[selectedEnemy], spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position, Quaternion.identity);
                    spawns++;
                    timeSet = false;
                }
            }
            else
            {
                spawns = 0;
                wave++;
                resource1 += numBuilding1 * 10;
                resource2 += numBuilding2 * 10;
                resource3 += numBuilding3 * 10;
                totalEnemies = totalEnemies * 2 - (int)wave;
                buildMode = true;
                start.enabled = true;
            }
        }
	}
}
