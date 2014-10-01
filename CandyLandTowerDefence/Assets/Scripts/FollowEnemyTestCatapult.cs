using UnityEngine;
using System.Collections;

public class FollowEnemyTestCatapult : MonoBehaviour {

    //public Object datEnemy;
    public GameObject datEnemy;
    public GameObject EndOfBarrel;
    public int maxDist = 10;
    public bool hasTarget;
    public bool isFPS;

    public int damp = 1;
    //BasicGunFire BasicGunFire;

    // Use this for initialization
	void Start () {
        hasTarget = false;
        isFPS = false; 
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
        //GameObject thisGun = GameObject.Find("BasicTestGun");
        //BasicGunFire basicGunFire = EndOfBarrel.GetComponent<BasicGunFire>();
        datEnemy = GetClosestEnemy();
        float enemyDist = Vector3.Distance(this.transform.position, datEnemy.transform.position);
        Debug.Log(enemyDist);

        if(isFPS == false)
        {
            if(enemyDist <= maxDist)
            {
                hasTarget = true;
                var rotationAngle = Quaternion.LookRotation(datEnemy.transform.position - transform.position); 
                transform.rotation = Quaternion.Slerp(transform.rotation, rotationAngle, Time.deltaTime * damp);
                transform.rotation = new Quaternion(0, this.transform.rotation.y, 0, this.transform.rotation.w); 
            }
            if(enemyDist >= maxDist)
            {
                hasTarget = false; 
            }
        }
	}


    
}
