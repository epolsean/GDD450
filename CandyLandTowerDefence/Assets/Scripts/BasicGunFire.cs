using UnityEngine;
using System.Collections;

public class BasicGunFire : MonoBehaviour {

    public Rigidbody datBullet;
    public int bulletSpeed;
    public double shootTimer = 0;
    public double time2Shoot = 2;
    public static bool hasTarget = false; 

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        shootTimer += Time.deltaTime;
        if(hasTarget)
        {
            if(shootTimer >= time2Shoot)
            {
                shootTimer = 0;
                Debug.Log("Boom!!!");
            }
        }
	}

    public void FireGun()
    {
        Object bulletClone = Instantiate(datBullet, this.transform.position, this.transform.rotation);
        //bulletClone.Velocity = transform.TransformDirection(Vector3(0, 0, bulletSpeed));
    }

    public static void setHasTarget(bool setInput)
    {
        hasTarget = setInput;
    }
}
