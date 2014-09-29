using UnityEngine;
using System.Collections;

public class FollowEnemyTest : MonoBehaviour {

    //public Object datEnemy;
    public GameObject datEnemy;
    public GameObject EndOfBarrel;
    public int maxDist = 10;
    public bool hasTarget;
    //BasicGunFire BasicGunFire;

    // Use this for initialization
	void Start () {
        hasTarget = false;
	}

    public GameObject GetClosestEnemy()
    {
        GameObject[] demEnemies;
        demEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closestEnemy = null;
        float distance = Mathf.Infinity;
        Vector3 disPosition = transform.position;
        foreach (GameObject Enemy in demEnemies)
        {
            Vector3 diff = Enemy.transform.position - disPosition;
            float curDustance = diff.sqrMagnitude;
            if (curDustance < distance)
            {
                closestEnemy = Enemy;
                distance = curDustance;
            }
        }
        return closestEnemy;
    }
	
	// Update is called once per frame
	void Update () {
        GameObject thisGun = GameObject.Find("BasicTestGun");
        BasicGunFire basicGunFire = EndOfBarrel.GetComponent<BasicGunFire>();
        datEnemy = GetClosestEnemy();
        //Debug.Log(FindClosestEnemy().name);
        //datEnemy = FindClosestEnemy();
        //Debug.Log(GetClosestEnemy().name);
        float enemyDist = Vector3.Distance(this.transform.position, datEnemy.transform.position);
        Debug.Log(enemyDist);
        //BasicGunFire = this.GetComponent<BasicGunFire>();
        if(enemyDist <= maxDist)
        {
            //this.gameObject.GetComponent<BasicGunFire>().setHasTarget(true);
            //basicGunFire.hasTarget = true; 
            //this.GetComponent<BasicGunFire>().hasTarget = true; 
            //basicGunFire.setHasTarget(true);
            hasTarget = true;
            this.transform.LookAt(datEnemy.transform);
        }
        if(enemyDist >= maxDist)
        {
            //BasicGunFire.setHasTarget(false);
            //this.GetComponent<BasicGunFire>().hasTarget = false; 
            //basicGunFire.hasTarget = false; 
            //basicGunFire.setHasTarget(false);
            hasTarget = false; 
        }
	}


    
}
