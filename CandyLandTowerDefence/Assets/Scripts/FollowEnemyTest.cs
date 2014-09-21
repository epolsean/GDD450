using UnityEngine;
using System.Collections;

public class FollowEnemyTest : MonoBehaviour {

    public GameObject datEnemy;
    int maxDist = 10;
    //BasicGunFire BasicGunFire;

    // Use this for initialization
	void Start () {
        //datEnemy = GameObject.FindGameObjectsWithTag("Enemy");
       // BasicGunFire = this.GetComponent<BasicGunFire>();
	}
	
	// Update is called once per frame
	void Update () {
        float enemyDist = Vector3.Distance(this.transform.position, datEnemy.transform.position);
        //BasicGunFire = this.GetComponent<BasicGunFire>();
        if(enemyDist <= maxDist)
        {
            BasicGunFire.setHasTarget(true);
            this.transform.LookAt(datEnemy.transform);
        }
        if(enemyDist >= maxDist)
        {
            BasicGunFire.setHasTarget(false);
        }
	}
}
