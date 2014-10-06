using UnityEngine;
using System.Collections;

public class PumpSeedFire : MonoBehaviour {

    public GameObject GunTop;
    public Rigidbody datBullet;
    public int bulletSpeed;
    public double shootTimer = 0;
    public double time2Shoot = 0.5;
    //public bool hasTarget = false; 

	// Use this for initialization
	void Start () {
	    //FollowEnemyTest followEnemyTest = this.GetComponent<FollowEnemyTest>();
	}
	
	// Update is called once per frame
	void Update () {
        FollowEnemyTest followEnemyTest = GunTop.GetComponent<FollowEnemyTest>();
        shootTimer += Time.deltaTime;
        if(followEnemyTest.hasTarget)
        {
            if(shootTimer >= time2Shoot)
            {
                shootTimer = 0;
                Debug.Log("Boom!!!");
                FireGun();
            }
        }
	}

    public void FireGun()
    {
        Rigidbody bulletClone = Instantiate(datBullet, transform.position, transform.rotation) as Rigidbody;
        bulletClone.rigidbody.useGravity = false; 
        bulletClone.velocity = transform.TransformDirection(Vector3.forward*bulletSpeed);
        Destroy(bulletClone.gameObject, 2);
    }

    /*public void setHasTarget(bool setInput)
    {
        this.GetComponent<BasicGunFire>().hasTarget = setInput;
    }*/
}
