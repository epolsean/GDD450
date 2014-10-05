using UnityEngine;
using System.Collections;

public class CatHitEnemy : MonoBehaviour {

    public GameObject datCat;
    public Transform enemyTrans;
    public int projSpeed = 1;

	// Use this for initialization
	void Start () {
        enemyTrans = datCat.GetComponent<FollowEnemyTestCatapult>().datEnemy.transform;
	}
	
	// Update is called once per frame
	void Update () {
        //transform.LookAt(enemyTrans);
        //this.transform.position += transform.forward * Time.deltaTime*2;
	}
}
