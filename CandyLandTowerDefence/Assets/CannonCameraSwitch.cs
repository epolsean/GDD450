using UnityEngine;
using System.Collections;

public class CannonCameraSwitch : MonoBehaviour {

    public bool isFPSCannon;

    public GameObject Gun;

    public GameObject TileSelected;

    public double shootTimer = 0.1;

	// Use this for initialization
	void Start () {
        isFPSCannon = false; 
	}
	
	// Update is called once per frame
	void Update () {

        shootTimer -= Time.deltaTime;
        
        if(Input.GetKeyDown(KeyCode.F)&& isFPSCannon == false && Gun != null)
        {
            if (Gun.tag == "GunBall" || Gun.tag == "PumpSeed")
            {
                Debug.Log("Switching To Cannon!");
                SwitchToCannonView();
            }
            else if(Gun.tag == "Catapult")
            {
                Debug.Log("Switching to Catapult");
                SwitchToCatView();
            }
            
        }
        else if(Input.GetKeyDown(KeyCode.F)&& isFPSCannon)
        {
            if (Gun.tag == "GunBall" || Gun.tag == "PumpSeed")
            {
                SwitchCameraViewBack();
            }
            else if(Gun.tag == "Catapult")
            {
                SwitchCatViewBack();
            }
        }
        if (isFPSCannon && Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Fire!");
            if (Gun.tag == "GunBall")
            {
                Gun.GetComponentInChildren<BasicGunFire>().FireGun();
                if (Gun.GetComponentInChildren<FollowEnemyTest>().EndOfBarrel2 != null)
                {
                    Gun.GetComponentInChildren<FollowEnemyTest>().EndOfBarrel2.GetComponent<BasicGunFire>().FireGun();
                }
            }
            else if (Gun.tag == "Catapult")
            {
                Gun.GetComponentInChildren<CatFire>().FireGun();
            }
        }
        else if (isFPSCannon && Gun.tag == "PumpSeed" && Input.GetKey(KeyCode.Space))
        {
            if (shootTimer <= 0)
            {
                Debug.Log("RailGun!!!");
                Gun.GetComponentInChildren<RollPumpSeedUpBarrel>().isFire = true;
                Gun.GetComponentInChildren<BasicGunFire>().FireGun();
                shootTimer = 0.1;
            }
        }
        if(Input.GetKeyUp(KeyCode.Space) && Gun.tag == "PumpSeed")
        {
            Gun.GetComponentInChildren<RollPumpSeedUpBarrel>().isFire = false; 
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

    public void SwitchToCatView()
    {
        Gun.GetComponent<FollowEnemyTestCatapult>().isFPS = true;
        Gun.GetComponent<FollowEnemyTestCatapult>().hasTarget = false;
        isFPSCannon = true;
        Gun.GetComponent<MouseLook>().enabled = true;
        Gun.GetComponent<MoveTarget>().enabled = true;
        Gun.GetComponentInChildren<Camera>().depth = 1;
    }

    public void SwitchCatViewBack()
    {
        Gun.GetComponent<FollowEnemyTestCatapult>().isFPS = false;
        Gun.GetComponent<FollowEnemyTestCatapult>().hasTarget = true;
        isFPSCannon = false;
        Gun.GetComponent<MouseLook>().enabled = false;
        Gun.GetComponent<MoveTarget>().enabled = false;
        Gun.GetComponentInChildren<Camera>().depth = -1;
    }
}
