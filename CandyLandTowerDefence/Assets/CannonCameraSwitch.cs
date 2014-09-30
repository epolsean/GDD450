using UnityEngine;
using System.Collections;

public class CannonCameraSwitch : MonoBehaviour {

    public bool isFPSCannon;

    public GameObject Gun;

	// Use this for initialization
	void Start () {
        isFPSCannon = false; 
	}
	
	// Update is called once per frame
	void Update () {
        
        if(Input.GetKeyDown(KeyCode.F)&& isFPSCannon == false)
        {
            Debug.Log("Switching To Cannon!");
            SwitchToCannonView();
        }
        else if(Input.GetKeyDown(KeyCode.F)&& isFPSCannon)
        {
            SwitchCameraViewBack();
        }
        if (isFPSCannon && Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Fire!");
            Gun.GetComponentInChildren<BasicGunFire>().FireGun();
        }
	}

    public void SwitchToCannonView()
    {
        Gun.GetComponentInChildren<FollowEnemyTest>().isFPS = true;
        Gun.GetComponentInChildren<FollowEnemyTest>().hasTarget = false;
        isFPSCannon = true; 
        Gun.GetComponentInChildren<MouseLook>().enabled = true;
        Gun.GetComponentInChildren<Camera>().depth = 1;
    }

    public void SwitchCameraViewBack()
    {
        Gun.GetComponentInChildren<FollowEnemyTest>().isFPS = false;
        isFPSCannon = false;
        Gun.GetComponentInChildren<MouseLook>().enabled = false;
        Gun.GetComponentInChildren<Camera>().depth = -1;
    }
}
