using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WaveSetup : MonoBehaviour {

    public Button start;
    public float wave = 1;
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
                totalEnemies = totalEnemies * 2 - (int)wave;
                buildMode = true;
                start.enabled = true;
            }
        }
	}
}
