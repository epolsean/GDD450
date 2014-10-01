using UnityEngine;
using System.Collections;

public class CatFire : MonoBehaviour {

    public GameObject CatBase;
    public GameObject CatPivot;
    public Rigidbody datProj;
    public int projSpeed = 9;
    public double shootTimer = 0;
    public double time2Shoot = 2;
    //public bool hasTarget = false; 

	// Use this for initialization
	void Start () {
	    //FollowEnemyTest followEnemyTest = this.GetComponent<FollowEnemyTest>();
	}
	
	// Update is called once per frame
	void Update () {
        FollowEnemyTestCatapult followEnemyTestCatapult = CatBase.GetComponent<FollowEnemyTestCatapult>();
        shootTimer += Time.deltaTime;
        if(followEnemyTestCatapult.hasTarget)
        {
            if(shootTimer >= time2Shoot)
            {
                shootTimer = 0;
                //Transform originalTrans = CatPivot.transform;
                //CatPivot.transform.rotation = Quaternion.Euler(150, 0, 0);
                Debug.Log("Boom!!!");
                FireGun();
                //CatPivot.transform.rotation = originalTrans.rotation;
                //CatPivot.transform.rotation = Quaternion.Euler(75, 0, 0);
            }
        }
	}

    public void FireGun()
    {
        Rigidbody bulletClone = Instantiate(datProj, transform.position, transform.rotation) as Rigidbody;
        //bulletClone.rigidbody.useGravity = false; 
        bulletClone.velocity = transform.TransformDirection(Vector3.forward*projSpeed);
        Destroy(bulletClone.gameObject, 2);
    }

    /*public void setHasTarget(bool setInput)
    {
        this.GetComponent<BasicGunFire>().hasTarget = setInput;
    }*/
}
